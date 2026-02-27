using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace elconnect
{
    public class ELmain
    {
        private SREYELINKLib.EyeLink el;
        private SREYELINKLib.EyeLinkUtil elutil;
        private EyelinkWindow elW;

        private Thread eyeThread;
        private BlockingCollection<Action> commandQueue;
        private bool running = false;

        private string currentEDF;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_RESTORE = 9;

        // ==================================================
        // START EYELINK THREAD
        // ==================================================

        public void StartCalibration(string eyelink_fileName)
        {
            if (running) return;

            running = true;
            commandQueue = new BlockingCollection<Action>();
            currentEDF = FormatEDF(eyelink_fileName);

            eyeThread = new Thread(EyeThreadLoop);
            eyeThread.SetApartmentState(ApartmentState.STA);
            eyeThread.IsBackground = true;
            eyeThread.Start();
        }

        private void EyeThreadLoop()
        {
            Application.EnableVisualStyles();

            el = new SREYELINKLib.EyeLink();
            elutil = new SREYELINKLib.EyeLinkUtil();

            el.open("100.1.1.1", 0);
            el.openDataFile(currentEDF);

            // Session-level configuration
            el.sendCommand("file_event_filter = LEFT,RIGHT,FIXATION,SACCADE,BLINK,MESSAGE,BUTTON");
            el.sendCommand("file_sample_data = LEFT,RIGHT,GAZE,AREA,GAZERES,STATUS");
            el.sendCommand("link_event_filter = LEFT,RIGHT,FIXATION,SACCADE,BLINK,BUTTON");
            el.sendCommand("link_sample_data = LEFT,RIGHT,GAZE,GAZERES,AREA,STATUS");

            Application.Run(new HiddenMessageLoop(this));
        }

        // ==================================================
        // HIDDEN MESSAGE LOOP
        // ==================================================

        private class HiddenMessageLoop : Form
        {
            private ELmain parent;

            public HiddenMessageLoop(ELmain p)
            {
                parent = p;
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }

            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);

                new Thread(() =>
                {
                    foreach (var action in parent.commandQueue.GetConsumingEnumerable())
                    {
                        if (!this.IsHandleCreated) continue;
                        this.Invoke(action);
                    }
                })
                { IsBackground = true }.Start();
            }

            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                base.OnFormClosing(e);

                parent.running = false;
                parent.commandQueue?.CompleteAdding();
                Application.ExitThread();
            }
        }

        // ==================================================
        // CALIBRATION (ONE TIME PER SESSION)
        // ==================================================

        public void Calibrate()
        {
            commandQueue.Add(() =>
            {
                elW = new EyelinkWindow();

                elW.Show();
                elW.Activate();
                elW.Focus();

                el.sendCommand($"screen_pixel_coords=0,0,{elW.ClientSize.Width - 1},{elW.ClientSize.Height - 1}");

                el.setOfflineMode();
                Thread.Sleep(100);

                var cal = elutil.getGDICal();
                cal.setCalibrationWindow(elW.Handle.ToInt32());
                cal.enableKeyCollection(true);

                // ESC exits setup
                el.doTrackerSetup();

                cal.enableKeyCollection(false);
                el.setOfflineMode();

                elW.Close();

                script_main.eyelink_is_ready = true;

                BringUnityToFront();
            });
        }

        // ==================================================
        // START RECORDING
        // ==================================================

        public void startRecord()
        {
            commandQueue.Add(() =>
            {
                el.setOfflineMode();
                Thread.Sleep(100);

                el.startRecording(true, true, true, true);
                Thread.Sleep(100);
            });
        }

        // ==================================================
        // STOP RECORDING (KEEP CONNECTION)
        // ==================================================

        public void stopRecord()
        {
            commandQueue.Add(() =>
            {
                el.stopRecording();
                Thread.Sleep(100);
                el.setOfflineMode();
            });
        }

        // ==================================================
        // TRIAL LABEL
        // ==================================================

        public void trial_ith(int ith)
        {
            commandQueue.Add(() =>
            {
                el.sendCommand($"record_status_message 'TRIAL {ith}'");
                el.sendMessage($"TRIALID {ith}");
            });
        }

        public void add_label(string label)
        {
            commandQueue.Add(() =>
            {
                el.sendMessage(label);
            });
        }

        // ==================================================
        // DOWNLOAD EDF
        // ==================================================

        public void DownloadEDF()
        {
            commandQueue.Add(() =>
            {
                try
                {
                    el.receiveDataFile(currentEDF, currentEDF);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("EDF download error: " + ex.Message);
                }
            });
        }

        // ==================================================
        // STOP RECORD + SAVE
        // ==================================================

        public void stopRecordAndSave()
        {
            if (!running) return;

            commandQueue.Add(() =>
            {
                try
                {
                    el.stopRecording();
                    Thread.Sleep(100);

                    el.setOfflineMode();
                    Thread.Sleep(100);

                    el.sendCommand("close_data_file");
                    Thread.Sleep(100);

                    el.close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("EDF save error: " + ex.Message);
                }

                Application.ExitThread();
            });

            commandQueue.CompleteAdding();

            if (eyeThread != null && eyeThread.IsAlive)
                eyeThread.Join(2000);

            running = false;
        }

        // ==================================================
        // FULL SHUTDOWN
        // ==================================================

        public void Shutdown()
        {
            if (!running) return;

            commandQueue.Add(() =>
            {
                try
                {
                    el.stopRecording();
                    Thread.Sleep(100);

                    el.setOfflineMode();
                    Thread.Sleep(100);

                    el.close();
                }
                catch { }

                Application.ExitThread();
            });

            commandQueue.CompleteAdding();

            if (eyeThread != null && eyeThread.IsAlive)
                eyeThread.Join(2000);

            running = false;
        }

        // ==================================================
        // HELPERS
        // ==================================================

        private void BringUnityToFront()
        {
            try
            {
                IntPtr h = Process.GetCurrentProcess().MainWindowHandle;
                if (h != IntPtr.Zero)
                {
                    ShowWindow(h, SW_RESTORE);
                    SetForegroundWindow(h);
                }
            }
            catch { }
        }

        private string FormatEDF(string name)
        {
            string clean = name.ToUpper()
                .Replace(".EDF", "")
                .Replace("_", "")
                .Replace("-", "");

            if (clean.Length > 8)
                clean = clean.Substring(0, 8);

            return clean;
        }
    }
}