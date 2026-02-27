using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;


public class script_sess_self_rating : MonoBehaviour
{
	public GameObject object_button;
	public GameObject object_txt;
	GameObject m_object_txt;
	GameObject m_object_button;
	public static int ith_rating_trial = 0;
	public static string cur_clickbait_idx;
	public static float cur_time_show_rating; 
	public static float cur_time_show_fixation; 
	public static string cur_clickbait; 

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
		cur_time_show_fixation = script_main.exp_time.ElapsedMilliseconds;

		//
		System.Random rnd_tp = new System.Random();
		Invoke("show_rating", rnd_tp.Next(1000, 3001) * 0.001f);
	}

	public void show_clickbait(string cur_txt)
	{
		m_object_txt = Instantiate(object_txt, Vector3.zero, Quaternion.identity);
		m_object_txt.transform.SetParent(script_main.canvasUI.transform, false);
		m_object_txt.transform.localEulerAngles = Vector3.zero;
		m_object_txt.transform.localScale = Vector3.one;
		m_object_txt.name = "txt";

		var rt = m_object_txt.GetComponent<RectTransform>();
		rt.anchorMin = new Vector2(0.05f, 0.65f);
		rt.anchorMax = new Vector2(0.95f, 0.65f);
		rt.pivot = new Vector2(0.5f, 0.5f);
		rt.anchoredPosition = Vector2.zero;

		TMP_Text txt_message = m_object_txt.GetComponent<TMP_Text>();
		TMP_FontAsset fontAsset = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
		txt_message.font = fontAsset;
		txt_message.enableAutoSizing = true;
		txt_message.fontSizeMin = 40;
		txt_message.fontSizeMax = 120;
		txt_message.text = cur_txt;
		txt_message.alignment = TextAlignmentOptions.Center;
		txt_message.color = Color.black;

		// 2️⃣ 强制文本刷新布局（这样才能获得正确的文本尺寸）
		Canvas.ForceUpdateCanvases();

		// 3️⃣ 创建黄色背景并匹配文字大小
		GameObject bg = new GameObject("txt_bg", typeof(Image));
		bg.transform.SetParent(m_object_txt.transform.parent, false);  // 放在同一层级
		bg.transform.SetAsFirstSibling(); // 保证在文字后面

		Image bgImage = bg.GetComponent<Image>();
		bgImage.color = new Color(1f, 1f, 1f, 1f); // 半透明黄色

		RectTransform bgRt = bg.GetComponent<RectTransform>();
		Vector2 textSize = txt_message.GetPreferredValues(txt_message.text);

		// 根据文字尺寸 + 一点 padding
		float paddingX = 20f;
		float paddingY = 20f;
		bgRt.sizeDelta = new Vector2(textSize.x + paddingX, textSize.y + paddingY);

		// 对齐背景和文字中心
		bgRt.anchorMin = rt.anchorMin;
		bgRt.anchorMax = rt.anchorMax;
		bgRt.pivot = rt.pivot;
		bgRt.anchoredPosition = rt.anchoredPosition;
	}

	void show_rating()
    {
		//
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();

		// file_output
		GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_self_fix_"+script_main.cur_sub_id+".txt", ith_rating_trial.ToString(), script_main.cur_time_trigger_sent.ToString(), cur_time_show_fixation.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());

		// cursor
		GameObject.Find("Directional Light").GetComponent<script_utility>().ResetCursorToCenter("bottom");
		script_main.cursor_image.enabled = true;
		
		// title 
		cur_clickbait_idx = script_main.list_condition_trivia_idx[script_main.list_random_idx[ith_rating_trial]];
		cur_clickbait = script_main.list_clickbait[int.Parse(cur_clickbait_idx)];
		show_clickbait(cur_clickbait);

		// rating bar 
		GameObject.Find("Directional Light").GetComponent<script_utility>().show_rating_bar("How interesting do you find this phrase?");

		// file_output
		cur_time_show_rating = script_main.exp_time.ElapsedMilliseconds;

		//
		// GameObject.Find("Directional Light").GetComponent<script_utility>().rating_btn_clicked(1);

    }

	public void go_to_next_trial()
	{
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();

		ith_rating_trial = ith_rating_trial + 1;		

		// if (ith_rating_trial < 2) 
		if (ith_rating_trial < script_main.list_condition_trivia_idx.Count) 
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
			rt.anchorMin = new Vector2(0.08f, 0.3f);
			rt.anchorMax = new Vector2(0.92f, 0.9f);
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
		txt_message.color = Color.black;
	}
	





}