using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net.Sockets;
using System.Text;
using System.ComponentModel;

public class script_sess_risk_rating : MonoBehaviour
{
	public GameObject object_ground_origin;
	public GameObject object_button;
	public GameObject object_txt;
	GameObject m_object_txt;
	GameObject m_object_button;
	public static int ith_rating_trial = 0;
	public static string cur_clickbait_idx;
	static GameObject m_object_ground_origin;
	string cur_fear_prob;
	float cur_fear_prob_float;
	int cur_risk_color_idx;
	public string pythonIP = "127.0.0.1";
	public int pythonPort = 5005;
	public static float cur_time_show_risk_fix; 
	public static float cur_time_show_risk_color; 
	public static float cur_time_show_rating; 
	public static float cur_time_show_blank; 
	public static float cur_time_show_shockPeriod; 
	public static string cur_shock_status; 


	public void rating_session_config()
	{
		// cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		script_main.cursor_image.enabled = false;
		script_main.allowCursorMove = true;
		//
		ith_rating_trial = 0;
		show_fixation();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			//
			CancelInvoke();
			StopAllCoroutines();
			GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
			GameObject.Find("Directional Light").GetComponent<script_utility>().reset_all_state();
			GameObject.Find("Directional Light").GetComponent<script_menu>().show_menu();
		}
	}

	public void show_fixation()
	{
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();

		// cursor 
		script_main.cursor_image.enabled = false;

		script_main.object_fix = new GameObject("Canvas");
		script_main.object_fix.AddComponent<Canvas>();
		script_main.object_fix.name = "object_fix";
		script_main.image_fix = script_main.object_fix.AddComponent<Image>();
		script_main.image_fix.transform.SetParent(script_main.canvasUI.transform);
		script_main.image_fix.rectTransform.sizeDelta = new Vector2(Screen.width / 20, Screen.width / 20);
		script_main.image_fix.rectTransform.anchoredPosition = new Vector2(0, 0);
		script_main.object_fix.transform.localEulerAngles = new Vector3(0, 0, 0);
		Texture2D fix_ima = (Texture2D)Resources.Load("ima_fix_on");
		script_main.image_fix.sprite = Sprite.Create(fix_ima, new Rect(0, 0, fix_ima.width, fix_ima.height), new Vector2(0.5f, 0.5f));	

		//
		cur_time_show_risk_fix = script_main.exp_time.ElapsedMilliseconds;
		cur_shock_status = "NA";

		//
		System.Random rnd_tp = new System.Random();
		Invoke("show_risk", rnd_tp.Next(1000, 3001) * 0.001f);
	}

	void show_risk()
    {
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		// cursor
		script_main.cursor_image.enabled = false;

		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_risk_timing_fix_"+script_main.cur_sub_id+".txt", ith_rating_trial.ToString(), script_main.cur_time_trigger_sent.ToString(), cur_time_show_risk_fix.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());


		//
		if (script_main.list_condition_risk_sess[script_main.list_random_idx[ith_rating_trial]] == "Risk L4")
		{
			cur_fear_prob = "24%";
			cur_fear_prob_float = 0.24f;
			cur_risk_color_idx=3;
		}
		if (script_main.list_condition_risk_sess[script_main.list_random_idx[ith_rating_trial]] == "Risk L3")
		{
			cur_fear_prob = "18%";
			cur_fear_prob_float = 0.18f;
			cur_risk_color_idx=2;
		}
		if (script_main.list_condition_risk_sess[script_main.list_random_idx[ith_rating_trial]] == "Risk L2")
		{
			cur_fear_prob = "12%";
			cur_fear_prob_float = 0.12f;
			cur_risk_color_idx=1;
		}
		if (script_main.list_condition_risk_sess[script_main.list_random_idx[ith_rating_trial]] == "Risk L1")
		{
			cur_fear_prob = "6%";
			cur_fear_prob_float = 0.06f;
			cur_risk_color_idx=0;
		}
		show_text("At the end of this trial\nA shock will be delivered with a " + cur_fear_prob + " probability.", "large_top", "center");

		//
		float temp_angle_ref = 0f;
		script_main.world_space = new GameObject("space");
		script_main.world_space.transform.position = new Vector3(0, 0, -Screen.height/8);
		for (int ii = 0; ii < script_main.sector_number; ii++)
		{
			m_object_ground_origin = Instantiate(object_ground_origin, new Vector3(0, 1, 0), Quaternion.identity);
			m_object_ground_origin.name = "sector" + ii.ToString();
			m_object_ground_origin.transform.SetParent(script_main.world_space.transform, false);
			script_draw_sector modifier = m_object_ground_origin.GetComponent<script_draw_sector>();
			modifier.ref_angle = temp_angle_ref;
			modifier.idx_sector_color = cur_risk_color_idx;
			modifier.outerR_ratio = 2.5f;
			modifier.UpdateMesh();
			temp_angle_ref = temp_angle_ref + script_main.sector_angle_in_game;  
		}
		
		cur_time_show_risk_color = script_main.exp_time.ElapsedMilliseconds;

		//
		System.Random rnd_tp = new System.Random();
		Invoke("show_blank_screen", rnd_tp.Next(2000, 4001) * 0.001f);
    }

	void show_blank_screen()
	{
		// cursor
		script_main.cursor_image.enabled = false;

		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_risk_riskLevel_"+script_main.cur_sub_id+".txt", ith_rating_trial.ToString(), cur_risk_color_idx.ToString(), script_main.list_random_idx[ith_rating_trial].ToString(), script_main.cur_time_trigger_sent.ToString(), cur_time_show_risk_color.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());

		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		cur_time_show_blank = script_main.exp_time.ElapsedMilliseconds;

		//
		System.Random rnd_tp = new System.Random();
		Invoke("show_rating", rnd_tp.Next(500, 1501) / 1000f);
	}

	void show_rating()
	{
		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_risk_blank_"+script_main.cur_sub_id+".txt", ith_rating_trial.ToString(), script_main.cur_time_trigger_sent.ToString(), cur_time_show_blank.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());
		
		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		// cursor
		GameObject.Find("Directional Light").GetComponent<script_utility>().ResetCursorToCenter("bottom");
		script_main.cursor_image.enabled = true;

		// 
		cur_time_show_rating = script_main.exp_time.ElapsedMilliseconds;
		//
		GameObject.Find("Directional Light").GetComponent<script_utility>().show_rating_bar("How anxious do you feel when you see this color?");
	}

	public void show_shock_screen()
	{
		// cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		script_main.cursor_image.enabled = false;
		//
		cur_time_show_shockPeriod = script_main.exp_time.ElapsedMilliseconds;
		//
		System.Random rnd_number = new System.Random();
		float temp = rnd_number.Next(0, 1000) / 1000f;
		if (temp < cur_fear_prob_float)
		{
			GameObject.Find("Directional Light").GetComponent<script_utility>().show_shock_icon("shock_yes");
			try
			{
				using (TcpClient client = new TcpClient(pythonIP, pythonPort))
				{
					NetworkStream stream = client.GetStream();
					byte[] data = Encoding.ASCII.GetBytes("SHOCK");
					stream.Write(data, 0, data.Length);
				}
				cur_shock_status = "Yes";
				UnityEngine.Debug.Log("SHOCK DELIVERED");
			}
			catch 
			{
				UnityEngine.Debug.Log("Shock triggered, It is not sent");
			}
		}
		else
		{
			cur_shock_status = "No";
			GameObject.Find("Directional Light").GetComponent<script_utility>().show_shock_icon("shock_no");
			UnityEngine.Debug.Log("SHOCK did not trigger");
		}
		//
		System.Random rnd_tp = new System.Random();
		Invoke("go_to_next_trial", rnd_tp.Next(2000, 4001) / 1000f);
	}



	public void go_to_next_trial()
	{
		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_risk_shock_"+script_main.cur_sub_id+".txt", ith_rating_trial.ToString(), cur_shock_status, script_main.cur_time_trigger_sent.ToString(), cur_time_show_shockPeriod.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());

		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();

		ith_rating_trial = ith_rating_trial + 1;		

		// if (ith_rating_trial < 2) 
		if (ith_rating_trial < script_main.list_condition_risk_sess.Count) 
		{
			show_fixation();
		}
		else
		{
			script_main.cursor_image.enabled = false;
			GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
			GameObject.Find("Directional Light").GetComponent<script_utility>().show_text("Please wait for further instructions from the experimenter.");
		}
	}
	void show_text(string cur_txt, string txt_type, string alignment)
	{
		m_object_txt = Instantiate(object_txt, Vector3.zero, Quaternion.identity);
		m_object_txt.transform.SetParent(script_main.canvasUI.transform);
		m_object_txt.transform.localEulerAngles = Vector3.zero;
		m_object_txt.transform.localScale = Vector3.one;   
		m_object_txt.name = "txt";

		var rt = m_object_txt.GetComponent<RectTransform>();
		if (txt_type == "large_top")
		{
			rt.anchorMin = new Vector2(0.03f, 0.65f);
			rt.anchorMax = new Vector2(0.97f, 0.9f);
		}
		else if (txt_type == "large_bottom")
		{
			rt.anchorMin = new Vector2(0.08f, 0.15f); // 左右各留 8%，垂直 12%～60%
			rt.anchorMax = new Vector2(0.92f, 0.55f);
		}
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
		rt.pivot = new Vector2(0.5f, 1f);

		TMP_Text txt_message = m_object_txt.GetComponent<TMP_Text>();
		TMP_FontAsset fontAsset = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
		txt_message.font = fontAsset;
		txt_message.enableAutoSizing = true;
		txt_message.fontSizeMin = 40;
		txt_message.fontSizeMax = 200;
		txt_message.text = cur_txt;
		if (alignment == "left")
		{
			txt_message.alignment = TextAlignmentOptions.Left;
		}
		else
		{
			txt_message.alignment = TextAlignmentOptions.Center;
		}
		txt_message.color = Color.white;
	}
	





}