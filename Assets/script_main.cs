using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using TMPro;
using RainbowArt.CleanFlatUI;
using Unity.VisualScripting;
using System.Net.Sockets;
using System.Text;
using SREYELINKLib;
using System.Threading;


public class script_main : MonoBehaviour
{
	public static elconnect.ELmain eyelink;
	public GameObject object_ball;
	public GameObject object_circle;
	public GameObject object_ground_origin;
	public GameObject object_txt;
	public GameObject object_button;
	public GameObject cursorPrefab;
	GameObject m_cursorPrefab;
	public static bool allow_eyelink = true;


	public GameObject controller;
	public GameObject object_subID;
	public Camera MainCamera;
	public static Camera m_MainCamera;
	public static GameObject m_circle1;
	public static GameObject m_circle2;
	public static GameObject m_object_subID;
	public static string selected_loc;
	public static float size_win_width = 1.2f;
	public static float size_win_height = 2.2f;
	public static float size_btn_width = 6f;
	public static float size_btn_height = 18f;
	public static float size_icon = 8f;
	public static float ground_size;

	public static int sector_number = 4;
	public static int sector_angle_in_game = 360 / sector_number;
	public static int sector_angle = 360 / sector_number;
		
	public static List<Color32> fear_color_list_in_game = new List<Color32>();
	public static List<Color32> curiosity_color_list = new List<Color32>();
	public static float min_fontsize = 30f;
	public static float max_fontsize = 100f;
	public static bool bool_is_postexp_scale1;
	public static bool bool_is_postexp_scale2;
	public static bool bool_is_postexp_scale3;
	public static bool bool_is_postexp_scale4;
	static GameObject m_object_ground_origin;
	public static GameObject m_object_button;
	public static GameObject object_fix;
	public static GameObject object_canvas_background;
	public static GameObject clickbait_label;
	public static Image image_fix;
	public static Image backgroundScreen;
	public static Image backgroundScreen2;
	public static Image circle1_face;
	public static Image circle2_face;
	
	public static bool bool_is_in_menu;
	public static bool bool_is_self_ratingSession = false;
	public static bool bool_is_risk_ratingSession = false;
	public static bool bool_is_decisionSession = false;
	public static bool bool_is_QuestionnaireSession = false;

	public static Canvas canvasUI;  

	public static List<string> list_trivia_facts;
	public static List<string> list_clickbait;
	public static List<string> list_imageName;
	public static List<string> list_cursor_pos_x;
	public static List<string> list_cursor_pos_y;
	public static List<string> list_trivia_word_sequence;
	public static List<string> list_trivia_time_sequence;
	public static bool bool_allow_record_cursor_pos=false;

	public static List<string> list_condition_risk_int;
	public static List<string> list_condition_trivia_idx;
	public static List<string> list_trivia_rate_bin4;
	public static List<string> list_condition_risk_labels;
	public static List<string> list_condition_risk_sess;
	public static List<int> list_random_idx;
	public static List<float> list_fearProbability = new List<float>();
	public static int ith_trial = 0;
	public static float fear_prob_1;
	public static float fear_prob_2;
	public static float button_wait_time = 0.3f;
	public static float angle_ori1;
	public static float angle_ori2;
	public static float size_ima_height = 4f;
	float refHeight = 1080f;
	public static float scaleFactor;
	int clickbait_idx;
	float temp_space_ori;
	public static string cur_trivia = "";
	public static string cur_image_name;
	public static string subject_id = "";
	public static List<string> list_5DC_question;
	public static List<string> list_5DC_options;
	public static List<string> list_IUS_question;
	public static List<string> list_IUS_options;
	public static List<string> list_STICSA_question;
	public static List<string> list_STICSA_options;
	public static List<string> list_DASS21_question;
	public static List<string> list_DASS21_options;

	public static List<string> cur_list_question;
	public static List<string> cur_list_options;


	// output
	public static List<string> output_scale_ius;
	public static List<string> output_scale_5dc;
	public static List<string> output_scale_STICSA;
	public static List<string> output_scale_DASS21;
	public static float cur_time_show_trivia_win;
	public static float cur_time_show_rating;
	public static float cur_time_trigger_sent;
	public static string cur_shock_status;
	public static float cur_time_shockPeriod;
	public static float cur_time_decisionPeriod;
	public static float cur_time_show_fixation;
	

	//
	public static List<int> rating_button_order; 
	public static GameObject world_space;
	public static float virtual_reward = 0;
	public float mouseSensitivity = 0.8f;
	public static bool allowCursorMove;
	public static Vector2 cursorPos;
	public static RectTransform cursorRect;
	public static Image cursor_image;
	public static Stopwatch exp_time;
	// public static bool bool_allow_circle_timer;
	public static GameObject menu_btn_self_rating;
	public static GameObject menu_btn_risk_rating;
	public static GameObject menu_btn_start;
	public static GameObject menu_btn_questionnaire;
	public static int num_dirs = 16;
	public static float time_limit = 1500f;
	//
	public static bool bool_is_on_trigger_screen = false;
	//
	// public static bool bool_circle_timer_start;
	// public static bool bool_clickbait_timeout;
	float cursor_pos_record_Interval = 0.05f;
	public static float cursor_pos_record_timer;

	public string pythonIP = "127.0.0.1";
	public int pythonPort = 5005;
	public static string cur_sub_id = "";

	private float lastEscTime = 0f;
	private float doublePressInterval = 1.5f;   // seconds
	public static bool eyelink_is_ready = false;
	

	void Start()
	{
		//
		object_canvas_background = new GameObject("Canvas_UI_Overlay");
		canvasUI = object_canvas_background.AddComponent<Canvas>();
		canvasUI.AddComponent<GraphicRaycaster>();
		canvasUI.renderMode = RenderMode.ScreenSpaceOverlay;
		canvasUI.sortingOrder = 100;

		var scaler_UI = object_canvas_background.AddComponent<CanvasScaler>();
		scaler_UI.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		scaler_UI.referenceResolution = new Vector2(1920, 1080);
		scaler_UI.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
		scaler_UI.matchWidthOrHeight = 0.5f;
		
		// setup colors
		curiosity_color_list = script_utility.GenerateCuriosityGradient(sector_number);
		fear_color_list_in_game = script_utility.GenerateFearGradient(sector_number);

		// 0.2 0.32 0.44 0.66 0.8
		list_fearProbability = new List<float> { 0.06f, 0.12f, 0.18f, 0.24f};

		// dataset
		list_trivia_facts = script_utility.TextAssetToList((TextAsset)Resources.Load("list_trivia"));
		list_clickbait = script_utility.TextAssetToList((TextAsset)Resources.Load("list_trivia_clickbait"));
		list_imageName = script_utility.TextAssetToList((TextAsset)Resources.Load("list_trivia_image"));
		list_trivia_rate_bin4 = script_utility.TextAssetToList((TextAsset)Resources.Load("list_trivia_rate_4bins"));
		
		// condition formal
		list_condition_trivia_idx = script_utility.TextAssetToList((TextAsset)Resources.Load("list_condition_trivia_idx"));
		list_condition_risk_int = script_utility.TextAssetToList((TextAsset)Resources.Load("list_condition_risk_int"));
		list_condition_risk_labels = script_utility.TextAssetToList((TextAsset)Resources.Load("list_condition_risk_labels"));
		list_condition_risk_sess = script_utility.TextAssetToList((TextAsset)Resources.Load("list_condition_risk_session"));

		
		// scale
		list_5DC_question = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_5DC_question"));
		list_5DC_options = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_5DC_option"));
		list_IUS_question = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_IUS_question"));
		list_IUS_options = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_IUS_option"));
		list_STICSA_question = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_STICSA_question"));
		list_STICSA_options = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_STICSA_option"));
		list_DASS21_question = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_DASS21_question"));
		list_DASS21_options = script_utility.TextAssetToList((TextAsset)Resources.Load("list_scale_DASS21_option"));

		// clean dataset
		list_trivia_facts.RemoveAll(string.IsNullOrWhiteSpace);
		list_clickbait.RemoveAll(string.IsNullOrWhiteSpace);
		list_imageName.RemoveAll(string.IsNullOrWhiteSpace);
		list_trivia_rate_bin4.RemoveAll(string.IsNullOrWhiteSpace);

		list_condition_trivia_idx.RemoveAll(string.IsNullOrWhiteSpace);
		list_condition_risk_int.RemoveAll(string.IsNullOrWhiteSpace);
		list_5DC_question.RemoveAll(string.IsNullOrWhiteSpace);
		list_5DC_options.RemoveAll(string.IsNullOrWhiteSpace);
		list_IUS_question.RemoveAll(string.IsNullOrWhiteSpace);
		list_IUS_options.RemoveAll(string.IsNullOrWhiteSpace);
		list_STICSA_question.RemoveAll(string.IsNullOrWhiteSpace);
		list_STICSA_options.RemoveAll(string.IsNullOrWhiteSpace);
		list_DASS21_options.RemoveAll(string.IsNullOrWhiteSpace);
		list_DASS21_question.RemoveAll(string.IsNullOrWhiteSpace);
		list_condition_risk_labels.RemoveAll(string.IsNullOrWhiteSpace);
		list_condition_risk_sess.RemoveAll(string.IsNullOrWhiteSpace);

		//
		m_MainCamera = Instantiate(MainCamera, new Vector3(0, 75f, 0), Quaternion.Euler(0, 0, 0));
		m_MainCamera.transform.localEulerAngles = new Vector3(90, 0, 0);
		m_MainCamera.transform.position = new Vector3(m_MainCamera.transform.position.x, 100, m_MainCamera.transform.position.z);
		m_MainCamera.orthographicSize = Screen.height / 2;
		m_MainCamera.clearFlags = CameraClearFlags.SolidColor;
		m_MainCamera.backgroundColor = new Color32(140, 140, 140, 255); // 约等于 #8C8C8C
		float orthoSize = m_MainCamera.orthographicSize; // Size = 5
		float visibleHeight = orthoSize * 2f;

		//
		ground_size = visibleHeight * 0.6f;
		scaleFactor = Screen.height / refHeight;
		
		exp_time = new Stopwatch();
		exp_time.Start();

		m_cursorPrefab = Instantiate(cursorPrefab);
		Canvas cursorCanvas = m_cursorPrefab.GetComponentInChildren<Canvas>();
		cursorCanvas.transform.SetParent(canvasUI.transform, false);
		cursorCanvas.overrideSorting = true;
		cursorCanvas.sortingOrder = 1000;
		
		//
		cursor_image = cursorCanvas.GetComponentInChildren<Image>();
		cursorRect = cursor_image.GetComponent<RectTransform>();

		RectTransform cursorCanvas_rect = cursorCanvas.GetComponent<RectTransform>();
		cursorCanvas_rect.anchorMin = new Vector2(0.5f, 0.5f);
		cursorCanvas_rect.anchorMax = new Vector2(0.5f, 0.5f);
		cursorCanvas_rect.sizeDelta = cursorRect.rect.size;
		cursorCanvas_rect.anchoredPosition = new Vector2(0, 0);
		cursorRect.sizeDelta = new Vector2(ground_size / 8, ground_size / 8);

		//
	    List<int> original = new List<int> { 1, 2, 3, 4, 5, 6 };
        System.Random rand = new System.Random();
        int startIndex = rand.Next(original.Count);
        rating_button_order = new List<int>();
        rating_button_order.AddRange(original.GetRange(startIndex, original.Count - startIndex));
        rating_button_order.AddRange(original.GetRange(0, startIndex));

		//
		GameObject.Find("Directional Light").GetComponent<script_utility>().ResetCursorToCenter("top");
		script_main.cursor_image.enabled = true;
		allowCursorMove = true;
		//
		GameObject.Find("Directional Light").GetComponent<script_menu>().show_menu();
	}

	public void trigger_screen()
	{
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		// cursor
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		cursor_image.enabled = false;
		allowCursorMove = false;
		//
		GameObject.Find("Directional Light").GetComponent<script_utility>().show_text("The experiment may start at any moment. \n\nPlease remain prepared.");
		bool_is_on_trigger_screen = true;
	}

	public void present_fixation_taskSession()
	{
		// cursor 
		cursor_image.enabled = false;
		cur_shock_status = "NA";

		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		selected_loc = "";
		
		bool_allow_record_cursor_pos = false;
		// bool_allow_circle_timer = false;
		// bool_circle_timer_start = false;

		//
		object_fix = new GameObject("Canvas");
		object_fix.AddComponent<Canvas>();
		object_fix.name = "object_fix";
		image_fix = object_fix.AddComponent<Image>();
		image_fix.transform.SetParent(canvasUI.transform);
		image_fix.rectTransform.sizeDelta = new Vector2(Screen.width / 20, Screen.width / 20);
		image_fix.rectTransform.anchoredPosition = new Vector2(0, 0);
		object_fix.transform.localEulerAngles = new Vector3(0, 0, 0);
		Texture2D fix_ima = (Texture2D)Resources.Load("ima_fix_on");
		image_fix.sprite = Sprite.Create(fix_ima, new Rect(0, 0, fix_ima.width, fix_ima.height), new Vector2(0.5f, 0.5f));
		cur_time_show_fixation = exp_time.ElapsedMilliseconds;

		//
		if (allow_eyelink & eyelink_is_ready)
		{
			eyelink.trial_ith(ith_trial);
			eyelink.add_label("fixation_onset");
		}


		//
		System.Random rnd_tp = new System.Random();
		Invoke("present_decision_phase", rnd_tp.Next(1000, 3001) * 0.001f);		
	}



	void do_cali()
	{
		eyelink = new elconnect.ELmain();
		cur_sub_id = script_menu.input_subID.text;
		eyelink.StartCalibration("C_"+cur_sub_id.ToString());
		eyelink.Calibrate();
	}
	void OnApplicationQuit()
	{
		if (allow_eyelink & eyelink_is_ready)
		{
			eyelink.Shutdown();
		}
		
	}
	void OnDestroy()
	{
		if (allow_eyelink & eyelink_is_ready)
		{
			eyelink?.Shutdown();
		}
	}


	// update
	//
	//
	// update
	//
	//
	// update
	//
	void Update()
	{

		if (bool_is_on_trigger_screen)
		{
			if (Input.GetKeyDown(KeyCode.E) & allow_eyelink)
			{
				do_cali();
			}

			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				bool_is_on_trigger_screen = false;
				//
				// cursor
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				cursor_image.enabled = true;
				allowCursorMove = true;

				//
				cur_time_trigger_sent = script_main.exp_time.ElapsedMilliseconds;
				//
				if (bool_is_decisionSession & allow_eyelink & eyelink_is_ready)
				{
					eyelink.startRecord();
					UnityEngine.Debug.Log(eyelink_is_ready);
				}
				

				// controller
				if (bool_is_self_ratingSession)
				{
					//
					GameObject.Find("Directional Light").GetComponent<script_sess_self_rating>().rating_session_config();
				}
				if (bool_is_risk_ratingSession)
				{
					//
					GameObject.Find("Directional Light").GetComponent<script_sess_risk_rating>().rating_session_config();
				}
				if (bool_is_decisionSession)
				{
					// idx randomized trials
					list_random_idx = new List<int>();
					list_random_idx = Enumerable.Range(0, list_condition_trivia_idx.Count).ToList();
					list_random_idx = script_utility.ShuffleList(list_random_idx);
					//
					present_fixation_taskSession();
				}
			}
		}

		if (allowCursorMove)
		{
			float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * 0.05f;
			float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * 0.05f;
			cursorPos.x += mouseX;
			cursorPos.y += mouseY;
			cursorPos.x = Mathf.Clamp(cursorPos.x, 0, Screen.width);
			cursorPos.y = Mathf.Clamp(cursorPos.y, 0, Screen.height);
			if (bool_allow_record_cursor_pos)
			{
				cursor_pos_record_timer += Time.deltaTime;
				if (cursor_pos_record_timer >= cursor_pos_record_Interval)
				{
					cursor_pos_record_timer = 0;
					list_cursor_pos_x.Add(cursorPos.x.ToString());
					list_cursor_pos_y.Add(cursorPos.y.ToString());
				}
			}
			cursorRect.position = cursorPos;
			if (Input.GetMouseButtonDown(0))
			{
				GameObject.Find("Directional Light").GetComponent<script_utility>().SimulateUIClick(cursorPos);
			}
		}

		if (bool_is_postexp_scale1)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				ToggleGroup[] toggleGroups = FindObjectsByType<ToggleGroup>(FindObjectsSortMode.None);
				bool allGroupsSelected = toggleGroups.All(group => group.GetComponentsInChildren<Toggle>().Any(toggle => toggle.isOn));
				if (allGroupsSelected)
				{
					//
					bool_is_postexp_scale1 = false;
					GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
					//
					output_scale_ius = new List<string>();
					foreach (ToggleGroup group in toggleGroups)
					{
						var toggles = group.GetComponentsInChildren<Toggle>();
						for (int ith = 0; ith < toggles.Count(); ith++)
						{
							if (toggles[ith].isOn)
							{
								output_scale_ius.Add((ith + 1).ToString());
							}
						}
					}
					//
					GameObject.Find("Directional Light").GetComponent<script_utility>().WriteListToTxt(output_scale_ius, "output_scale_ius_"+cur_sub_id+".txt");
					//
					bool_is_postexp_scale2 = true;
					GameObject.Find("Directional Light").GetComponent<script_utility>().show_scale();
				}
			}
		}
		if (bool_is_postexp_scale2)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				ToggleGroup[] toggleGroups = FindObjectsByType<ToggleGroup>(FindObjectsSortMode.None);
				bool allGroupsSelected = toggleGroups.All(group => group.GetComponentsInChildren<Toggle>().Any(toggle => toggle.isOn));
				if (allGroupsSelected)
				{
					//
					bool_is_postexp_scale2 = false;
					GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
					//
					output_scale_5dc = new List<string>();
					foreach (ToggleGroup group in toggleGroups)
					{
						var toggles = group.GetComponentsInChildren<Toggle>();
						for (int ith = 0; ith < toggles.Count(); ith++)
						{
							if (toggles[ith].isOn)
							{
								output_scale_5dc.Add((ith + 1).ToString());
							}
						}
					}
					//
					GameObject.Find("Directional Light").GetComponent<script_utility>().WriteListToTxt(output_scale_5dc, "output_scale_5dc_"+cur_sub_id+".txt");
					//
					bool_is_postexp_scale3 = true;
					GameObject.Find("Directional Light").GetComponent<script_utility>().show_scale();
				}
			}
		}
		if (bool_is_postexp_scale3)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				ToggleGroup[] toggleGroups = FindObjectsByType<ToggleGroup>(FindObjectsSortMode.None);
				bool allGroupsSelected = toggleGroups.All(group => group.GetComponentsInChildren<Toggle>().Any(toggle => toggle.isOn));
				if (allGroupsSelected)
				{
					//
					bool_is_postexp_scale3 = false;
					GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
					//
					output_scale_STICSA = new List<string>();
					foreach (ToggleGroup group in toggleGroups)
					{
						var toggles = group.GetComponentsInChildren<Toggle>();
						for (int ith = 0; ith < toggles.Count(); ith++)
						{
							if (toggles[ith].isOn)
							{
								output_scale_STICSA.Add((ith + 1).ToString());
							}
						}
					}
					//
					GameObject.Find("Directional Light").GetComponent<script_utility>().WriteListToTxt(output_scale_STICSA, "output_scale_STICSA_"+cur_sub_id+".txt");
					//
					bool_is_postexp_scale4 = true;
					GameObject.Find("Directional Light").GetComponent<script_utility>().show_scale();
				}
			}
		}
		if (bool_is_postexp_scale4)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				ToggleGroup[] toggleGroups = FindObjectsByType<ToggleGroup>(FindObjectsSortMode.None);
				bool allGroupsSelected = toggleGroups.All(group => group.GetComponentsInChildren<Toggle>().Any(toggle => toggle.isOn));
				if (allGroupsSelected)
				{
					//
					bool_is_postexp_scale4 = false;
					GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
					//
					output_scale_DASS21 = new List<string>();
					foreach (ToggleGroup group in toggleGroups)
					{
						var toggles = group.GetComponentsInChildren<Toggle>();
						for (int ith = 0; ith < toggles.Count(); ith++)
						{
							if (toggles[ith].isOn)
							{
								output_scale_DASS21.Add((ith + 1).ToString());
							}
						}
					}
					//
					GameObject.Find("Directional Light").GetComponent<script_utility>().WriteListToTxt(output_scale_DASS21, "output_scale_DASS21_"+cur_sub_id+".txt");
					//
					exp_end_msg();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			//
			CancelInvoke();
			StopAllCoroutines();
			GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
			GameObject.Find("Directional Light").GetComponent<script_utility>().reset_all_state();
			GameObject.Find("Directional Light").GetComponent<script_menu>().show_menu();
			//
			GameObject.Find("Directional Light").GetComponent<script_utility>().ResetCursorToCenter("top");
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (Time.time - lastEscTime < doublePressInterval)
			{
				Application.Quit();
			}
			else
			{
				lastEscTime = Time.time;
			}
		}
	}

	void present_decision_phase()
	{
		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();

		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_fix_"+cur_sub_id+".txt", ith_trial.ToString(), cur_time_trigger_sent.ToString(), cur_time_show_fixation.ToString(), exp_time.ElapsedMilliseconds.ToString());
		
		// cursor
		GameObject.Find("Directional Light").GetComponent<script_utility>().ResetCursorToCenter("center");
		script_main.cursor_image.enabled = true;
		cur_time_decisionPeriod = exp_time.ElapsedMilliseconds;

		// risk levels
		fear_prob_1 = list_fearProbability[int.Parse(list_condition_risk_int[list_random_idx[ith_trial]])-1];
		if (fear_prob_1==0.06f)
		{
			fear_prob_2 = 0.06f;
		}
		if (fear_prob_1==0.12f)
		{
			fear_prob_2 = 0.06f;
		}
		if (fear_prob_1==0.18f)
		{
			fear_prob_2 = 0.12f;
		}
		if (fear_prob_1==0.24f)
		{
			fear_prob_2 = 0.18f;
		}

		// prepare dir indices
		System.Random random = new System.Random();
		int rnd_dir = random.Next(0, 360);
		angle_ori1 = rnd_dir;
		
		if (fear_prob_1==0.06f)
		{
			angle_ori2 = angle_ori1 - 40f;
		} 
		else
		{
			angle_ori2 = angle_ori1 - 90f;
		}

		// add space		
		temp_space_ori = GameObject.Find("Directional Light").GetComponent<script_utility>().get_space_ori(angle_ori1);
		float temp_angle_ref = temp_space_ori;
		world_space = new GameObject("space");
		world_space.transform.position = new Vector3(0, 0, 0);
		for (int ii = 0; ii < sector_number; ii++)
		{
			m_object_ground_origin = Instantiate(object_ground_origin, new Vector3(0, 1, 0), Quaternion.identity);
			m_object_ground_origin.name = "sector" + ii.ToString();
			m_object_ground_origin.transform.SetParent(world_space.transform, false);
			script_draw_sector modifier = m_object_ground_origin.GetComponent<script_draw_sector>();
			modifier.ref_angle = temp_angle_ref;
			modifier.idx_sector_color = ii;
			modifier.UpdateMesh();
			temp_angle_ref = temp_angle_ref + sector_angle_in_game;  
		}

		// add circle
		float loc_ratio1 = 1f;
		Vector2 cur_locs_circle1 = script_utility.get_loc_given_dir(angle_ori1, loc_ratio1);
		Vector2 cur_locs_circle2 = script_utility.get_loc_given_dir(angle_ori2, loc_ratio1);
		m_circle1 = Instantiate(object_circle, new Vector3(cur_locs_circle1[0], 3, cur_locs_circle1[1]), Quaternion.Euler(90, 0, 0));
		m_circle2 = Instantiate(object_circle, new Vector3(cur_locs_circle2[0], 3, cur_locs_circle2[1]), Quaternion.Euler(90, 0, 0));
		Canvas canvas_circle1 = m_circle1.GetComponent<Canvas>();
		Canvas canvas_circle2 = m_circle2.GetComponent<Canvas>();
		canvas_circle1.worldCamera = m_MainCamera;
		canvas_circle2.worldCamera = m_MainCamera;
		m_circle1.transform.SetParent(world_space.transform, false);
		m_circle2.transform.SetParent(world_space.transform, false);
		m_circle1.name = "object_circle1";
		m_circle2.name = "object_circle2";

		//
		clickbait_idx = int.Parse(list_condition_trivia_idx[list_random_idx[ith_trial]]);
	
		// circle color and size
		GameObject circle1_background = m_circle1.transform.Find("circle/ProgressBarCircularAuto_Solid/Background").gameObject;
		GameObject circle2_background = m_circle2.transform.Find("circle/ProgressBarCircularAuto_Solid/Background").gameObject;
		circle1_face = circle1_background.GetComponentInChildren<Image>();
		circle2_face = circle2_background.GetComponentInChildren<Image>();
		circle1_face.color = curiosity_color_list[int.Parse(list_trivia_rate_bin4[clickbait_idx]) - 1];
		circle2_face.color = Color.gray;

		// circle size
		float circle1_ratio = ((int.Parse(list_trivia_rate_bin4[clickbait_idx]) - 1f) / 3f) + 1f;
		float circle2_ratio = 1f;
		float original_circle_size = ground_size / 6f;
		float adjusted_circle1_size = original_circle_size * circle1_ratio;
		float adjusted_circle2_size = original_circle_size * circle2_ratio;

		RectTransform m_circle1_rect = m_circle1.GetComponent<RectTransform>();
		RectTransform m_circle2_rect = m_circle2.GetComponent<RectTransform>();
		m_circle1_rect.sizeDelta = new Vector2(adjusted_circle1_size, adjusted_circle1_size);
		m_circle2_rect.sizeDelta = new Vector2(adjusted_circle2_size, adjusted_circle2_size);

		// gb circle size
		RectTransform circle1_gb_rect = circle1_background.GetComponent<RectTransform>();
		RectTransform circle2_gb_rect = circle2_background.GetComponent<RectTransform>();
		circle1_gb_rect.sizeDelta = new Vector2(adjusted_circle1_size, adjusted_circle1_size);
		circle2_gb_rect.sizeDelta = new Vector2(adjusted_circle2_size, adjusted_circle2_size);

		Button btn_circle1 = m_circle1.GetComponentInChildren<Button>();
		Button btn_circle2 = m_circle2.GetComponentInChildren<Button>();
	
		btn_circle1.onClick.AddListener(circle1_Clicked);
		btn_circle2.onClick.AddListener(circle2_Clicked);

		// add label
		clickbait_label = new GameObject("clickbait_label");
		GameObject.Find("Directional Light").GetComponent<script_utility>().place_label(angle_ori1, list_clickbait[clickbait_idx], curiosity_color_list[int.Parse(list_trivia_rate_bin4[clickbait_idx]) - 1], m_circle1, clickbait_label);

		// add legend
		script_utility.add_legend("legend_c", "colormap_c", 1);
		script_utility.add_legend("legend_f", "colormap_f", 0);

		//
		if (allow_eyelink & eyelink_is_ready)
		{
			eyelink.add_label("clickbait_onset");
		}
		
		//
		// ScreenCapture.CaptureScreenshot("/Users/bo/Desktop/aa.png", 2);

        // 
        if (bool_is_decisionSession)
        {
			list_cursor_pos_x = new List<string>();
			list_cursor_pos_y = new List<string>();
			list_trivia_word_sequence = new List<string>();
			list_trivia_time_sequence = new List<string>();
			bool_allow_record_cursor_pos = true;
			cursor_pos_record_timer = 0;
        }
	}

	void circle1_Clicked()
	{
		// cursor
		cursor_image.enabled = false;
		// bool_circle_timer_start = false;
		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_FLevel_"+cur_sub_id+".txt", ith_trial.ToString(), list_condition_risk_int[list_random_idx[ith_trial]].ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_ori1_"+cur_sub_id+".txt", ith_trial.ToString(), angle_ori1.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_ori2_"+cur_sub_id+".txt", ith_trial.ToString(), angle_ori2.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_oriSpace_"+cur_sub_id+".txt", ith_trial.ToString(), temp_space_ori.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_clickbaitIdx_"+cur_sub_id+".txt", ith_trial.ToString(), clickbait_idx.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_clickbaitLabel_"+cur_sub_id+".txt", ith_trial.ToString(), list_clickbait[clickbait_idx].ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_CLevel_"+cur_sub_id+".txt", ith_trial.ToString(), list_trivia_rate_bin4[clickbait_idx].ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		//
		if(allow_eyelink & eyelink_is_ready){
			eyelink.add_label("clickbaitRes_onset");
		}

		//
		Button btn_circle1 = m_circle1.GetComponentInChildren<Button>();
		Button btn_circle2 = m_circle2.GetComponentInChildren<Button>();
		btn_circle1.interactable = false;
		btn_circle2.interactable = false;

		//
		selected_loc = "1";
		script_scale_animation circle1_ani = m_circle1.GetComponent<script_scale_animation>();
		circle1_ani.play_zoom_animation_for_circle(1.5f);

		Invoke("after_clickbait", 1f);
	}

	void circle2_Clicked()
	{
		// cursor
		cursor_image.enabled = false;
		// bool_circle_timer_start = false;
		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_FLevel_"+cur_sub_id+".txt", ith_trial.ToString(), list_condition_risk_int[list_random_idx[ith_trial]].ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_ori1_"+cur_sub_id+".txt", ith_trial.ToString(), angle_ori1.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_ori2_"+cur_sub_id+".txt", ith_trial.ToString(), angle_ori2.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_oriSpace_"+cur_sub_id+".txt", ith_trial.ToString(), temp_space_ori.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_clickbaitIdx_"+cur_sub_id+".txt", ith_trial.ToString(), clickbait_idx.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_clickbaitLabel_"+cur_sub_id+".txt", ith_trial.ToString(), list_clickbait[clickbait_idx].ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_CLevel_"+cur_sub_id+".txt", ith_trial.ToString(), list_trivia_rate_bin4[clickbait_idx].ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		//
		if (allow_eyelink & eyelink_is_ready)
		{
			eyelink.add_label("clickbaitRes_onset");
		}

		//
		Button btn_circle1 = m_circle1.GetComponentInChildren<Button>();
		Button btn_circle2 = m_circle2.GetComponentInChildren<Button>();
		btn_circle1.interactable = false;
		btn_circle2.interactable = false;

		//
		selected_loc = "2";
		script_scale_animation circle2_ani = m_circle2.GetComponent<script_scale_animation>();
		circle2_ani.play_zoom_animation_for_circle(1.5f);
		//
		Invoke("after_clickbait", 1f);
	}
	
	void after_clickbait()
	{
		if (bool_is_decisionSession)
		{
			GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
			// file_output
			GameObject.Find("Directional Light").GetComponent<script_utility>().WriteMousePos(ith_trial, list_cursor_pos_x, list_cursor_pos_y);
			GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_clickbaitRes_"+cur_sub_id+".txt", ith_trial.ToString(), selected_loc.ToString(), cur_time_trigger_sent.ToString(), cur_time_decisionPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
			//
			bool_allow_record_cursor_pos = false;
			//
			shock_phase();
		}
    }
	void shock_phase()
	{
		// cursor
		cursor_image.enabled = false;
		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		//
		cur_time_shockPeriod = exp_time.ElapsedMilliseconds;
		//
		if (allow_eyelink & eyelink_is_ready)
		{
			eyelink.add_label("ShockPeriod_onset");
		}
		
		//
		if (selected_loc == "1")
		{
			cur_trivia = list_trivia_facts[clickbait_idx];
			cur_image_name = list_imageName[clickbait_idx];

			System.Random rnd_opt1 = new System.Random();
			float temp = rnd_opt1.Next(0, 1000) / 1000f;
			if (temp < fear_prob_1)
			{
				GameObject.Find("Directional Light").GetComponent<script_utility>().show_shock_icon("shock_yes");
				UnityEngine.Debug.Log("SHOCK DELIVERED");
				try
				{
					using (TcpClient client = new TcpClient(pythonIP, pythonPort))
					{
						NetworkStream stream = client.GetStream();
						byte[] data = Encoding.ASCII.GetBytes("SHOCK");
						stream.Write(data, 0, data.Length);
					}
					cur_shock_status="Yes";
				}
				catch 
				{
					UnityEngine.Debug.Log("Shock triggered, It is not sent");
				}
			}
			else
			{
				cur_shock_status="No";
				GameObject.Find("Directional Light").GetComponent<script_utility>().show_shock_icon("shock_no");
				UnityEngine.Debug.Log("SHOCK did not trigger");
			}
			//
			System.Random rnd_tp = new System.Random();
			Invoke("show_trivia_win", rnd_tp.Next(2000, 4001) / 1000f);
		}
		if (selected_loc == "2")
		{
			System.Random rnd_opt2 = new System.Random();
			float temp = rnd_opt2.Next(0, 1000) / 1000f;
			if (temp < fear_prob_2)
			{
				GameObject.Find("Directional Light").GetComponent<script_utility>().show_shock_icon("shock_yes");
				UnityEngine.Debug.Log("SHOCK DELIVERED");
				try
				{
					using (TcpClient client = new TcpClient(pythonIP, pythonPort))
					{
						NetworkStream stream = client.GetStream();
						byte[] data = Encoding.ASCII.GetBytes("SHOCK");
						stream.Write(data, 0, data.Length);
					}
					cur_shock_status="Yes";
				}
				catch 
				{
					UnityEngine.Debug.Log("Shock triggered, It is not sent");
				}
			}
			else
			{
				cur_shock_status="No";
				GameObject.Find("Directional Light").GetComponent<script_utility>().show_shock_icon("shock_no");
				UnityEngine.Debug.Log("SHOCK did not trigger");
			}
			System.Random rnd_tp = new System.Random();

			//
			Invoke("go_to_next_trial", rnd_tp.Next(2000, 4001) / 1000f);
		}
		//
		// ScreenCapture.CaptureScreenshot("/Users/bo/Desktop/aaa.png", 2);
	}

	public void go_to_next_trial()
	{
		if (selected_loc == "2")
		{
			// file_output
			GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_shock_"+cur_sub_id+".txt", ith_trial.ToString(), cur_shock_status, cur_time_trigger_sent.ToString(), cur_time_shockPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		} 
		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		ith_trial = ith_trial + 1;
		
		if (ith_trial < list_condition_trivia_idx.Count) //num_trial list_condition_trivia_idx.Count
		{
			present_fixation_taskSession();
		}
		else
		{
			//
			cursor_image.enabled = false;
			GameObject.Find("Directional Light").GetComponent<script_utility>().show_text("You have completed the scanning session");

			//
			if (allow_eyelink & eyelink_is_ready)
			{
				eyelink.stopRecordAndSave();
				eyelink.DownloadEDF();
			}
		}
	}


	void show_trivia_win()
	{
		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		//
		if (selected_loc == "1")
		{
			// file_output
			GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_shock_"+cur_sub_id+".txt", ith_trial.ToString(), cur_shock_status, cur_time_trigger_sent.ToString(), cur_time_shockPeriod.ToString(), exp_time.ElapsedMilliseconds.ToString());
		} 

		// cursor
		GameObject.Find("Directional Light").GetComponent<script_utility>().ResetCursorToCenter("bottom");
		script_main.cursor_image.enabled = false;

		// time points
		cur_time_show_trivia_win = exp_time.ElapsedMilliseconds;
		//
		if (allow_eyelink & eyelink_is_ready)
		{
			eyelink.add_label("Trivia_onset");
		}

		//
		GameObject.Find("Directional Light").GetComponent<script_utility>().show_trivia_win(cur_trivia, cur_image_name);

		//
		// ScreenCapture.CaptureScreenshot("/Users/bo/Desktop/aaaa.png", 2);

	}

	public static IEnumerator remove_trivia_items()
	{
		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		yield return new WaitForSeconds(button_wait_time);

		// cursor
		GameObject.Find("Directional Light").GetComponent<script_utility>().ResetCursorToCenter("bottom");
		script_main.cursor_image.enabled = true;

		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_triviaWin_"+cur_sub_id+".txt", ith_trial.ToString(), cur_time_trigger_sent.ToString(), cur_time_show_trivia_win.ToString(), exp_time.ElapsedMilliseconds.ToString());
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteWordSequence(ith_trial, list_trivia_word_sequence, list_trivia_time_sequence);

		cur_time_show_rating = exp_time.ElapsedMilliseconds;

		//
		GameObject.Find("Directional Light").GetComponent<script_utility>().show_rating_bar("How interesting do you find this trivia fact?");
		//
		// ScreenCapture.CaptureScreenshot("/Users/bo/Desktop/aaaaa.png", 2);
	}


	void exp_end_msg()
	{
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		GameObject.Find("Directional Light").GetComponent<script_utility>().show_text("Thank you for participating!");
	}
}





