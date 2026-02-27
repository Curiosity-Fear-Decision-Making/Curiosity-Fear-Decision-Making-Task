using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.Contracts;
using System.IO; 

public class script_utility : MonoBehaviour
{
	public GameObject object_scrollview;
	public GameObject object_button;
	public static GameObject m_object_scrollview;
	public GameObject object_message_window;
	public GameObject object_toggle;
	public GameObject object_txt;
	GameObject m_object_toggle;
	GameObject m_object_txt;
	public static int cur_rating_response;
	public static void add_legend(string legend_name, string image_name, int pos)
	{
		Canvas m_legend = new GameObject(legend_name).AddComponent<Canvas>();
		m_legend.renderMode = RenderMode.ScreenSpaceOverlay;
		m_legend.gameObject.AddComponent<CanvasScaler>();
		m_legend.gameObject.AddComponent<GraphicRaycaster>();
		GameObject legend_obj = new GameObject(legend_name);
		legend_obj.transform.SetParent(m_legend.transform);
		Image image = legend_obj.AddComponent<Image>();
		Texture2D texture = (Texture2D)Resources.Load(image_name);
		image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		RectTransform rect_c = legend_obj.GetComponent<RectTransform>();
		rect_c.anchorMin = new Vector2(0.5f, pos);
		rect_c.anchorMax = new Vector2(0.5f, pos);
		rect_c.pivot = new Vector2(0.5f, pos);
		rect_c.anchoredPosition = new Vector2(0, 0); // -Screen.height * 0.01f
		float parentWidth = ((RectTransform)rect_c.parent).rect.width;
		// float target_width = parentWidth * 0.9f;
		float target_width = parentWidth;
		rect_c.sizeDelta = new Vector2(target_width, (target_width / texture.width) * texture.height);
	}

	public static List<E> ShuffleList<E>(List<E> inputList)
	{
		List<E> randomList = new List<E>();
		System.Random r = new System.Random();
		int randomIndex;
		while (inputList.Count > 0)
		{
			randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
			randomList.Add(inputList[randomIndex]); //add it to the new, random list
			inputList.RemoveAt(randomIndex); //remove to avoid duplicates
		}
		return randomList;
	}

	public static List<Color32> GenerateCuriosityGradient(int sectorNumber)
	{
		Color[] colormapColors = new Color[]
		{
			new Color32(0, 255, 255, 255) ,
			new Color32(0, 255, 128, 255),
			new Color32(255, 180, 0, 255),
			new Color32(255, 255, 0, 255)
	    };
		List<Color32> curiosityColorList = new List<Color32>();
		int segmentCount = colormapColors.Length - 1;
		for (int i = 0; i < sectorNumber; i++)
		{
			float t = i / (float)(sectorNumber - 1);
			float scaledT = t * segmentCount;
			int index = Mathf.Min(Mathf.FloorToInt(scaledT), segmentCount - 1);
			float localT = scaledT - index;
			Color interpolatedColor = Color.Lerp(colormapColors[index], colormapColors[index + 1], localT);
			curiosityColorList.Add((Color32)interpolatedColor);
		}
		return curiosityColorList;
	}
	// public static List<Color32> GenerateCuriosityGradient(int sectorNumber)
	// {
	// 	Color[] colormapColors = new Color[]
	// 	{
	// 		new Color32(255, 128, 0, 255),  // 亮红
	// 		new Color32(255, 255, 0, 255)      // 深紫
	// 	};
	// 	List<Color32> curiosityColorList = new List<Color32>();
	// 	int segmentCount = colormapColors.Length - 1;
	// 	for (int i = 0; i < sectorNumber; i++)
	// 	{
	// 		float t = i / (float)(sectorNumber - 1);
	// 		float scaledT = t * segmentCount;
	// 		int index = Mathf.Min(Mathf.FloorToInt(scaledT), segmentCount - 1);
	// 		float localT = scaledT - index;
	// 		Color interpolatedColor = Color.Lerp(colormapColors[index], colormapColors[index + 1], localT);
	// 		
	// 		// 
	// 		curiosityColorList.Add((Color32)interpolatedColor);
	// 	}
	// 	return curiosityColorList;
	// }

	public static List<Color32> GenerateFearGradient(int sectorNumber)
	{
		Color[] colormapColors = new Color[]
		{
			new Color32(255, 204, 229, 255),  // 亮红
			new Color32(102, 0, 51, 255)      // 深紫
		};
		List<Color32> fearColorList = new List<Color32>();
		int segmentCount = colormapColors.Length - 1;

		for (int i = 0; i < sectorNumber; i++)
		{
			float t = i / (float)(sectorNumber - 1);
			float scaledT = t * segmentCount;
			int index = Mathf.Min(Mathf.FloorToInt(scaledT), segmentCount - 1);
			float localT = scaledT - index;
			Color interpolatedColor = Color.Lerp(colormapColors[index], colormapColors[index + 1], localT);
			fearColorList.Add((Color32)interpolatedColor);
		}
		return fearColorList;
	}



	// public static List<Color32> GenerateFearGradient(int sectorNumber)
	// {
	// 	Color[] colormapColors = new Color[]
	// 	{
	// 		new Color32(104, 154, 59, 255), // #00bfff
	//         new Color32(192, 224, 145, 255), // #00ffff
	//         new Color32(217, 147, 189,255), // #00ff80
	//         new Color32(174, 44, 117, 255) // #80ff00
	//     };
	// 	List<Color32> fearColorList = new List<Color32>();
	// 	int segmentCount = colormapColors.Length - 1;

	// 	for (int i = 0; i < sectorNumber; i++)
	// 	{
	// 		float t = i / (float)(sectorNumber - 1);
	// 		float scaledT = t * segmentCount;
	// 		int index = Mathf.Min(Mathf.FloorToInt(scaledT), segmentCount - 1);
	// 		float localT = scaledT - index;
	// 		Color interpolatedColor = Color.Lerp(colormapColors[index], colormapColors[index + 1], localT);
	// 		fearColorList.Add((Color32)interpolatedColor);
	// 	}
	// 	return fearColorList;
	// }

	// public static List<Color32> GenerateFearGradient_in_game(int sectorNumber)
	// {
	// 	Color[] colormapColors = new Color[]
	// 	{
	// 		new Color32(250, 100, 120, 255),  // 亮红
	// 		new Color32(73, 17, 40, 255)      // 深紫
	// 	};
	// 	List<Color32> fearColorList = new List<Color32>();
	// 	int segmentCount = colormapColors.Length - 1;

	// 	for (int i = 0; i < sectorNumber; i++)
	// 	{
	// 		float t = i / (float)(sectorNumber - 1);
	// 		float scaledT = t * segmentCount;
	// 		int index = Mathf.Min(Mathf.FloorToInt(scaledT), segmentCount - 1);
	// 		float localT = scaledT - index;
	// 		Color interpolatedColor = Color.Lerp(colormapColors[index], colormapColors[index + 1], localT);
	// 		fearColorList.Add((Color32)interpolatedColor);
	// 	}
	// 	return fearColorList;
	// }


	public static List<string> TextAssetToList(TextAsset ta)
	{
		return new List<string>(ta.text.Split('\n'));
	}
	public static float GetShortestAngleMidpoint(float a1, float a2)
	{
		float delta = Mathf.DeltaAngle(a1, a2);
		float mid = a1 + delta / 2f;
		if (mid < 0) mid += 360f;
		return mid;
	}

	public static Vector2 get_loc_given_dir(float angle_ori, float loc_ratio)
	{
		float loc_z = Mathf.Sin(angle_ori * Mathf.PI / 180) * (script_main.ground_size / 2 * loc_ratio - script_main.ground_size/10);
		float loc_x = Mathf.Cos(angle_ori * Mathf.PI / 180) * (script_main.ground_size / 2 * loc_ratio - script_main.ground_size/10);
		return new Vector2(loc_x, loc_z);
	}
	public static float AngleDifference(float angle1, float angle2)
	{
		float diff = Mathf.Abs(angle1 - angle2) % 360f;
		return diff > 180f ? 360f - diff : diff;
	}



	void show_txt_for_scale(string txt, Transform parent, float xx, float yy, float width_ratio)
	{
		m_object_txt = Instantiate(object_txt, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
		m_object_txt.transform.SetParent(parent);
		m_object_txt.transform.localEulerAngles = new Vector3(0, 0, 0);
		m_object_txt.name = "txt";
		RectTransform txt_rect = m_object_txt.GetComponent<RectTransform>();
		txt_rect.anchoredPosition = new Vector2(xx, yy);
		txt_rect.sizeDelta = new Vector2(Screen.width * width_ratio, Screen.height / 9);
		TMP_Text txt_message = m_object_txt.GetComponent<TMP_Text>();
		txt_message.enableAutoSizing = true;
		txt_message.fontSizeMin = 10f;
		txt_message.fontSizeMax = 80f;
		txt_message.text = txt;
		txt_message.color = Color.white;
	}
	public void show_scale()
	{
		m_object_scrollview = Instantiate(object_scrollview, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
		m_object_scrollview.name = "scrollview";
		m_object_scrollview.transform.SetParent(script_main.canvasUI.transform);
		m_object_scrollview.transform.localEulerAngles = new Vector3(0, 0, 0);
		GameObject scrollview_content = m_object_scrollview.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
		RectTransform scrollview_rect = m_object_scrollview.GetComponent<RectTransform>();
		scrollview_rect.anchoredPosition = new Vector2(0, -0.05f * Screen.height);
		float visual_width = Screen.width * 0.9f;
		float visual_height = Screen.height * 0.7f;
		float gap_v_size = visual_height / 5f;
		float gap_h_size = visual_width / 10.5f;
		float question_x = -(visual_width / 2) * 0.7f;
		float qwr = 0.2f;
		scrollview_rect.sizeDelta = new Vector2(visual_width, visual_height);
		
		if (script_main.bool_is_postexp_scale1)
		{
			script_main.cur_list_question = script_main.list_IUS_question;
			script_main.cur_list_options = script_main.list_IUS_options;
			gap_h_size = visual_width / 8f;
		}
		if (script_main.bool_is_postexp_scale2)
		{
			script_main.cur_list_question = script_main.list_5DC_question;
			script_main.cur_list_options = script_main.list_5DC_options;
		}
		if (script_main.bool_is_postexp_scale3)
		{
			script_main.cur_list_question = script_main.list_STICSA_question;
			script_main.cur_list_options = script_main.list_STICSA_options;
		}
		if (script_main.bool_is_postexp_scale4)
		{
			script_main.cur_list_question = script_main.list_DASS21_question;
			script_main.cur_list_options = script_main.list_DASS21_options;
		}

		// compute length
		float scrollview_length = 0;
		for (int ith_row = 0; ith_row < script_main.cur_list_question.Count; ith_row++)
		{
			scrollview_length = 2 * (visual_height / 2 * (1f - 0.8f)) + gap_v_size * ith_row;
		}
		scrollview_length = scrollview_length * 1.1f;
		// add title
		if (script_main.bool_is_postexp_scale1)
		{
			show_txt_for_scale("Please use the scale below to describe to what extent each item is characteristic of you. Please circle a number (1 to 5) that describes you best.", scrollview_content.transform, 0, (scrollview_length / 2) * 0.97f, 0.7f);
		}
		if (script_main.bool_is_postexp_scale2)
		{
			show_txt_for_scale("Below are statements people often use to describe themselves. Please use the scale below to indicate the degree to which these statements accurately describe you. There are no right or wrong answers.", scrollview_content.transform, 0, (scrollview_length / 2) * 0.97f, 0.7f);
		}
		if (script_main.bool_is_postexp_scale3)
		{
			show_txt_for_scale("Please use the scale below to indicate how accurately each statement describes your current mood. There are no right or wrong answers.", scrollview_content.transform, 0, (scrollview_length / 2) * 0.97f, 0.7f);
		}
		if (script_main.bool_is_postexp_scale4)
		{
			show_txt_for_scale("Please read each statement and circle a number 0, 1, 2, 3 that indicates how much the statement applied to you over the past week. There are no right or wrong answers.", scrollview_content.transform, 0, (scrollview_length / 2) * 0.97f, 0.7f);
		}
		// add question 
		for (int ith_row = 0; ith_row < script_main.cur_list_question.Count; ith_row++)
		{
			show_txt_for_scale(script_main.cur_list_question[ith_row], scrollview_content.transform, question_x, (scrollview_length / 2) * 0.85f - gap_v_size * ith_row, qwr);
		}
		// change scrollview height
		RectTransform viewport_rect = m_object_scrollview.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
		viewport_rect.sizeDelta = new Vector2(viewport_rect.sizeDelta.x, scrollview_length);

		// add options
		for (int ith_col = 0; ith_col < script_main.cur_list_options.Count; ith_col++)
		{
			string cur_option = script_main.cur_list_options[ith_col].Replace(" ", " " + "\u200B");
			float cur_loc_x = question_x + Screen.width * qwr * 0.8f + gap_h_size * ith_col;
			show_txt_for_scale(cur_option, scrollview_content.transform, cur_loc_x, (scrollview_length / 2) * 0.9f, 0.09f);
		}
		// toggle
		for (int ith_row = 0; ith_row < script_main.cur_list_question.Count; ith_row++)
		{
			GameObject cur_row = new GameObject("group_row" + ith_row.ToString());
			cur_row.transform.SetParent(scrollview_content.transform, false);
			cur_row.AddComponent<ToggleGroup>();
			cur_row.AddComponent<RectTransform>();
			ToggleGroup toggleGroup = cur_row.GetComponent<ToggleGroup>();
			toggleGroup.allowSwitchOff = true;
			for (int ith_col = 0; ith_col < script_main.cur_list_options.Count; ith_col++)
			{
				m_object_toggle = Instantiate(object_toggle, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
				m_object_toggle.transform.SetParent(cur_row.transform);
				m_object_toggle.transform.localScale = new Vector3(Screen.width / 960f, Screen.width / 960f, Screen.width / 960f);
				RectTransform toggle_rect = m_object_toggle.GetComponent<RectTransform>();
				float cur_loc_x = question_x + Screen.width * qwr * 0.8f + gap_h_size * ith_col;
				float cur_loc_y = (scrollview_length / 2) * 0.85f - gap_v_size * ith_row;
				toggle_rect.anchoredPosition = new Vector2(cur_loc_x, cur_loc_y);
				Toggle cur_toggle = m_object_toggle.GetComponent<Toggle>();
				cur_toggle.isOn = false;
				cur_toggle.group = toggleGroup;
				m_object_toggle.name = "row" + ith_row.ToString() + "_col" + ith_col.ToString();
			}
		}
		// fontsize
		List<float> list_font_size = new List<float>();
		foreach (Transform child in scrollview_content.transform)
		{
			TMP_Text tmp = child.GetComponent<TMP_Text>();
			if (tmp != null)
			{
				tmp.ForceMeshUpdate();
				float renderedSize = tmp.textInfo.characterInfo[0].pointSize;
				list_font_size.Add(renderedSize);
			}
		}
		float min_font_size = Mathf.Min(list_font_size.ToArray());
		foreach (Transform child in scrollview_content.transform)
		{
			TMP_Text tmp = child.GetComponent<TMP_Text>();
			if (tmp != null)
			{
				tmp.enableAutoSizing = false; // 确保不被自动缩放覆盖
				tmp.fontSize = min_font_size;
			}
		}
		show_btn("After completion, press Enter", "center");
	}

	public void show_btn(string btn_txt, string align /* "left" | "center" | "right" */)
	{
		script_main.m_object_button = Instantiate(object_button);
		script_main.m_object_button.transform.SetParent(script_main.canvasUI.transform, false); 
		script_main.m_object_button.name = "btn";
		var label = script_main.m_object_button.transform.GetChild(1).GetComponent<TMP_Text>();
		label.text = btn_txt;
		label.color = Color.black;
		label.enableAutoSizing = false;

		var rt = script_main.m_object_button.GetComponent<RectTransform>();
		rt.pivot = new Vector2(0.5f, 0.5f);
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
		if (align == "left")
		{
			rt.anchorMin = new Vector2(0.05f, 0.05f);
			rt.anchorMax = new Vector2(0.35f, 0.15f);
		}
		else if (align == "right")
		{
			rt.anchorMin = new Vector2(0.65f, 0.05f);
			rt.anchorMax = new Vector2(0.95f, 0.15f);
		}
		else // center
		{
			rt.anchorMin = new Vector2(0.35f, 0.05f);
			rt.anchorMax = new Vector2(0.65f, 0.15f);
		}
		
        const float minPt = 28f;
		const float maxPt = 96f;
		float sizeByHeight = rt.rect.height * 0.42f;
        float fontSize = Mathf.Clamp(sizeByHeight, minPt, maxPt);
        float paddingX = 48f;
        float availableW = Mathf.Max(0.0f, rt.rect.width - paddingX);
        fontSize = FitToWidth(label, availableW, fontSize, minPt);

        label.fontSize = fontSize;
	}
    private float FitToWidth(TMP_Text label, float maxWidth, float startSize, float minSize)
    {
        label.fontSize = startSize;
        Vector2 pref = label.GetPreferredValues(label.text, Mathf.Infinity, Mathf.Infinity);
        if (pref.x <= maxWidth || startSize <= minSize) return startSize;

        float scale = maxWidth / pref.x;
        float newSize = Mathf.Max(minSize, startSize * scale);

        label.fontSize = newSize;
        pref = label.GetPreferredValues(label.text, Mathf.Infinity, Mathf.Infinity);
        if (pref.x > maxWidth)
        {
            float tinyScale = maxWidth / pref.x;
            newSize = Mathf.Max(minSize, newSize * tinyScale);
        }
        return newSize;
    }
	
	public void place_label(float cur_angle, string input_txt, Color32 cur_color, GameObject cur_circle, GameObject input_label)
	{
		Canvas m_canvas = new GameObject("c_label").AddComponent<Canvas>();
		m_canvas.renderMode = RenderMode.WorldSpace;
		m_canvas.gameObject.AddComponent<GraphicRaycaster>();
		m_canvas.worldCamera = script_main.m_MainCamera;
		// m_canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 1000);
		m_canvas.transform.rotation = Quaternion.Euler(90f, 0f, 0f); // 让整个画布贴地
		m_canvas.transform.SetParent(script_main.world_space.transform, false);

		input_label.transform.SetParent(m_canvas.transform, false);
		input_label.transform.position = new Vector3(cur_circle.transform.position.x, 2, cur_circle.transform.position.z);
		input_label.transform.localRotation = Quaternion.identity;
		input_label.AddComponent<Image>().color = cur_color;

		GameObject textGO = new GameObject("Text");
		textGO.transform.SetParent(input_label.transform, false);
		RectTransform textRect = textGO.AddComponent<RectTransform>();
		textRect.anchorMin = new Vector2(0, 0);
		textRect.anchorMax = new Vector2(1, 1);
		textRect.offsetMin = Vector2.zero;
		textRect.offsetMax = Vector2.zero;

		// TMP_FontAsset fontAsset = ;
		TextMeshProUGUI tmp = textGO.AddComponent<TextMeshProUGUI>();
		tmp.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
		tmp.enableAutoSizing = true;
		tmp.fontSizeMin = script_main.min_fontsize * script_main.scaleFactor * 0.6f;
		tmp.fontSizeMax = script_main.max_fontsize * script_main.scaleFactor * 0.6f;
		tmp.color = Color.black;
		tmp.overflowMode = TextOverflowModes.Overflow;
		tmp.alignment = TextAlignmentOptions.Center;
		input_txt = input_txt.Replace(" ", " " + "\u200B");
		string[] words = input_txt.Split(' ');
		if (words.Length >= 2)
		{
			string newText = words[0] + "\n" + string.Join(" ", words.Skip(1).ToArray());
			tmp.text = newText;
		}
		else
		{
			tmp.text = input_txt;
		}
		
		RectTransform rect = input_label.GetComponent<RectTransform>();
		if (cur_angle == 0)
		{
			rect.pivot = new Vector2(0f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Right;
		}
		else if (cur_angle == 180)
		{
			rect.pivot = new Vector2(1f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Left;
		}
		else if (cur_angle == 90)
		{
			rect.pivot = new Vector2(0f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Center;
		}
		else if (cur_angle == 270)
		{
			rect.pivot = new Vector2(1f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Center;
		}
		else if (cur_angle > 0 & cur_angle < 90)
		{
			rect.pivot = new Vector2(0f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Right;
		}
		else if (cur_angle > 270 & cur_angle < 360)
		{
			rect.pivot = new Vector2(0f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Right;
		}
		else if (cur_angle > 90 & cur_angle < 180)
		{
			rect.pivot = new Vector2(1f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Left;
		}
		else if (cur_angle > 180 & cur_angle < 270)
		{
			rect.pivot = new Vector2(1f, 0.5f);
			// tmp.alignment = TextAlignmentOptions.Left;
		}

		// distance to screen left right border
		float cameraDistance = Mathf.Abs(script_main.m_MainCamera.transform.position.z - cur_circle.transform.position.z);

		// 屏幕左右边界的世界坐标
		Vector3 leftWorldPoint = script_main.m_MainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, cameraDistance));
		Vector3 rightWorldPoint = script_main.m_MainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, cameraDistance));

		// 物体到左右边界的距离
		float distanceToLeft = cur_circle.transform.position.x - leftWorldPoint.x;
		float distanceToRight = rightWorldPoint.x - cur_circle.transform.position.x;
		float minValue = Mathf.Min(distanceToLeft, distanceToRight);

		GameObject cur_inner_circle = cur_circle.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
		RectTransform rt = cur_inner_circle.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(minValue*0.9f, rt.rect.size.y);  // 宽 200, 高 100
	}

	public void show_text(string input)
	{
		var bgCanvas = script_main.canvasUI.GetComponent<Canvas>();
		bgCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
		script_main.canvasUI.renderMode = RenderMode.ScreenSpaceOverlay;
		script_main.canvasUI.sortingOrder = 100;

		m_object_txt = Instantiate(object_txt);
		m_object_txt.name = "txt";
		m_object_txt.transform.SetParent(script_main.canvasUI.transform, false);

		// 2) 用锚点定义一个“可读区域”（左右各留 8% 边距，高度占中部 35%）
		var rt = m_object_txt.GetComponent<RectTransform>();
		rt.anchorMin = new Vector2(0.05f, 0.5f);
		rt.anchorMax = new Vector2(0.95f, 0.8f);
		rt.pivot = new Vector2(0.5f, 0.5f);
		rt.offsetMin = Vector2.zero;               // 用 offset=0 让其随锚点自适应
		rt.offsetMax = Vector2.zero;

		// 3) 文本样式（自动换行+自动字号）
		var txt = m_object_txt.GetComponent<TMP_Text>();
		var fontAsset = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
		if (fontAsset) txt.font = fontAsset;

		txt.text = input;
		txt.color = Color.white;
		txt.alignment = TextAlignmentOptions.Center;
		txt.textWrappingMode = TextWrappingModes.Normal;
		txt.overflowMode = TextOverflowModes.Truncate; // 或 Ellipsis

		// 4) 自动字号的上下限（配合 CanvasScaler = ScaleWithScreenSize）
		txt.enableAutoSizing = true;
		txt.fontSizeMin = 36f;   // 别太小
		txt.fontSizeMax = 100f;  // 别太大

		// 5) 可选：给段落一点内边距，让视觉更舒服
		txt.margin = new Vector4(0f, 10f, 0f, 10f);
	}

	public void show_rating_bar(string title)
	{
		//
		script_main.cursor_image.enabled = true;

		// title
		var titleGO = Instantiate(object_txt);
		titleGO.name = "RatingTitle";
		titleGO.transform.SetParent(script_main.canvasUI.transform, false);
		var titleRT = titleGO.GetComponent<RectTransform>();
		titleRT.anchorMin = new Vector2(0.5f, 0.65f);
		titleRT.anchorMax = new Vector2(0.5f, 0.95f);
		titleRT.pivot = new Vector2(0.5f, 0.5f);
		titleRT.anchoredPosition = Vector2.zero;
		// width
		var canvasRT = script_main.canvasUI.GetComponent<RectTransform>();
		float targetWidth = canvasRT.rect.width * 0.9f;
		titleRT.sizeDelta = new Vector2(targetWidth, 200f);  // 高度可自定义

		var txt = titleGO.GetComponent<TMP_Text>();
		txt.text = title;
		txt.color = Color.white;
		txt.alignment = TextAlignmentOptions.Center;
		txt.enableAutoSizing = true;
		txt.fontSizeMin = 35f;
		txt.fontSizeMax = 85f;

		// === 3. 创建容器 ===
		var circleGO = new GameObject("RatingCircle", typeof(RectTransform));
		circleGO.transform.SetParent(script_main.canvasUI.transform, false);
		var circleRT = circleGO.GetComponent<RectTransform>();
		circleRT.anchorMin = new Vector2(0.5f, 0.1f);
		circleRT.anchorMax = new Vector2(0.5f, 0.5f);
		circleRT.pivot = new Vector2(0.5f, 0.5f);
		circleRT.anchoredPosition = Vector2.zero;
		circleRT.sizeDelta = new Vector2(600, 600);  // 控制按钮分布的范围（可调）

		// === 4. 生成 6 个按钮并沿圆分布 ===
		int buttonCount = 6;
		float radius = 200f;  // 半径，可根据分辨率动态调整
		float angleStep = 360f / buttonCount;
		float fontScale = 0.5f;  // 字体大小比例

		for (int i = 0; i < buttonCount; i++)
		{
			float angle = i * angleStep * Mathf.Deg2Rad;
			float x = Mathf.Cos(angle) * radius;
			float y = Mathf.Sin(angle) * radius;

			var btn = Instantiate(object_button);
			int idx = i; 
			btn.name = $"rating_button{script_main.rating_button_order[idx]}";
			btn.transform.SetParent(circleGO.transform, false);

			Button cur_btn = btn.GetComponentInChildren<Button>();
			cur_btn.onClick.AddListener(() => rating_btn_clicked(idx));
			var rt = btn.GetComponent<RectTransform>();
			rt.anchorMin = new Vector2(0.5f, 0.5f);
			rt.anchorMax = new Vector2(0.5f, 0.5f);
			rt.pivot = new Vector2(0.5f, 0.5f);
			rt.anchoredPosition = new Vector2(x, y);
			rt.sizeDelta = new Vector2(120f, 120f); // 每个按钮大小

			// 背景 & 文本
			var bgRT = btn.transform.GetChild(0).GetComponent<RectTransform>();
			bgRT.anchorMin = Vector2.zero; bgRT.anchorMax = Vector2.one;
			bgRT.offsetMin = Vector2.zero; bgRT.offsetMax = Vector2.zero;

			var t = btn.transform.GetChild(1).GetComponent<TMP_Text>();
			var txtRT = t.GetComponent<RectTransform>();
			txtRT.anchorMin = Vector2.zero; txtRT.anchorMax = Vector2.one;
			txtRT.offsetMin = Vector2.zero; txtRT.offsetMax = Vector2.zero;
			t.enableAutoSizing = false;
			t.fontSize = rt.sizeDelta.y * fontScale;
			t.alignment = TextAlignmentOptions.Center;
			t.color = Color.black;
			t.text = script_main.rating_button_order[i].ToString();
		}
		//
		if (script_main.bool_is_decisionSession & script_main.allow_eyelink & script_main.eyelink_is_ready)
		{
			script_main.eyelink.add_label("Rating_onset");
		}
	}


	public void rating_btn_clicked(int i)
	{
		
		cur_rating_response = script_main.rating_button_order[i];
		// controller
		if (script_main.bool_is_self_ratingSession)
		{
			// file_output
			GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_self_rating_"+script_main.cur_sub_id+".txt", script_sess_self_rating.ith_rating_trial.ToString(), script_sess_self_rating.cur_clickbait, cur_rating_response.ToString(), script_main.cur_time_trigger_sent.ToString(), script_sess_self_rating.cur_time_show_rating.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());
		}
		if (script_main.bool_is_risk_ratingSession)
		{
			// file_output
			GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_risk_ratingResponse_"+script_main.cur_sub_id+".txt", script_sess_risk_rating.ith_rating_trial.ToString(), cur_rating_response.ToString(), script_main.cur_time_trigger_sent.ToString(), script_sess_risk_rating.cur_time_show_rating.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());
		}
		if (script_main.bool_is_decisionSession)
		{
			// file_output
			GameObject.Find("Directional Light").GetComponent<script_utility>().WriteLineToFile("output_formal_rating_"+script_main.cur_sub_id+".txt", script_main.ith_trial.ToString(), cur_rating_response.ToString(), script_main.cur_time_trigger_sent.ToString(), script_main.cur_time_show_rating.ToString(), script_main.exp_time.ElapsedMilliseconds.ToString());
		}
		//
		GameObject obj_rating_btn = GameObject.Find("rating_button" + (cur_rating_response).ToString());
		obj_rating_btn.GetComponent<script_scale_animation>().play_scale_animation(1.3f);
		Button cur_btn = obj_rating_btn.GetComponentInChildren<Button>();
		cur_btn.interactable = false;
		StartCoroutine(remove_rating_interest());
    }
	IEnumerator remove_rating_interest()
	{
		yield return new WaitForSeconds(script_main.button_wait_time);
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();

		// controller
		if (script_main.bool_is_self_ratingSession)
		{
			
			GameObject.Find("Directional Light").GetComponent<script_sess_self_rating>().go_to_next_trial();
		}
		if (script_main.bool_is_risk_ratingSession)
		{
			GameObject.Find("Directional Light").GetComponent<script_sess_risk_rating>().show_shock_screen();
		}
		if (script_main.bool_is_decisionSession)
		{
			// file_output
			GameObject.Find("Directional Light").GetComponent<script_main>().go_to_next_trial();
		}
	}


	public void ResetCursorToCenter(string pos)
	{
		script_main.cursor_image.enabled = false;

		RectTransform rt = script_main.cursorRect;

		// 固定 anchor 在中心
		rt.anchorMin = new Vector2(0.5f, 0.5f);
		rt.anchorMax = new Vector2(0.5f, 0.5f);
		rt.pivot = new Vector2(0.5f, 0.5f);

		if (pos == "bottom")
		{
			rt.anchoredPosition = new Vector2(0f, -Screen.height * 0.2f);
			script_main.cursorPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.3f);
		}
		else if (pos == "top")
		{
			rt.anchoredPosition = new Vector2(0f, Screen.height * 0.3f);
			script_main.cursorPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.8f);
		}
		else if (pos == "center")
		{
			rt.anchoredPosition = Vector2.zero;
			script_main.cursorPos = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
		}
		// 🔑 强制这一帧就 rebuild
		Canvas.ForceUpdateCanvases();
	}

	public void show_color_check(string txt)
	{
		// reset_UI_gb();
		// script_main.object_canvas_background.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
		for (int b = 0; b < 2; b++)
		{
			if (b == 0)
			{
				script_main.m_object_button = Instantiate(object_button);
				script_main.m_object_button.transform.SetParent(script_main.canvasUI.transform, false);
				script_main.m_object_button.transform.localEulerAngles = new Vector3(0, 0, 0);
				RectTransform button_rect_init = script_main.m_object_button.GetComponent<RectTransform>();
				button_rect_init.anchorMin = new Vector2(0.3f, 0.8f);
				button_rect_init.anchorMax = new Vector2(0.3f, 0.8f);
				button_rect_init.sizeDelta = new Vector2(Screen.width / 10f, Screen.width / 10f);

				Transform bg = script_main.m_object_button.transform.Find("Background");
				Image bgImage = bg.GetComponent<Image>();
				bgImage.color = new Color(247f / 255f, 206f / 255f, 228f / 255f, 1f); // 红色 (R,G,B,A)
			}
			if (b == 1)
			{
				script_main.m_object_button = Instantiate(object_button);
				script_main.m_object_button.transform.SetParent(script_main.canvasUI.transform, false);
				script_main.m_object_button.transform.localEulerAngles = new Vector3(0, 0, 0);
				RectTransform button_rect_init = script_main.m_object_button.GetComponent<RectTransform>();
				button_rect_init.anchorMin = new Vector2(0.7f, 0.8f);
				button_rect_init.anchorMax = new Vector2(0.7f, 0.8f);
				button_rect_init.sizeDelta = new Vector2(Screen.width / 10f, Screen.width / 10f);

				Transform bg = script_main.m_object_button.transform.Find("Background");
				Image bgImage = bg.GetComponent<Image>();
				bgImage.color = new Color(93f / 255f, 14f / 255f, 50f / 255f, 1f); // 红色 (R,G,B,A)
			}
			script_main.m_object_button.name = "rating_button" + (b + 1).ToString();

			TMP_Text txt_rating = script_main.m_object_button.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();
			txt_rating.enableAutoSizing = true;
			txt_rating.fontSizeMin = script_main.min_fontsize * script_main.scaleFactor;
			txt_rating.fontSizeMax = script_main.max_fontsize * script_main.scaleFactor;
			txt_rating.text = "";

		}
		// title
		m_object_txt = Instantiate(object_txt, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
		m_object_txt.transform.SetParent(script_main.canvasUI.transform, false);
		m_object_txt.transform.localEulerAngles = new Vector3(0, 0, 0);
		m_object_txt.name = "txt";

		// script_scale_animation circle1_ani = m_object_txt.GetComponent<script_scale_animation>();
		// circle1_ani.play_scale_animation();

		var rt = m_object_txt.GetComponent<RectTransform>();
		rt.anchorMin = new Vector2(0.1f, 0.2f);
		rt.anchorMax = new Vector2(0.9f, 0.65f);
		rt.pivot = new Vector2(0.5f, 0.5f);
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;

		// 3) 文本样式（自动换行+自动字号）
		var cur_txt = m_object_txt.GetComponent<TMP_Text>();
		var fontAsset = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
		if (fontAsset) cur_txt.font = fontAsset;

		cur_txt.text = txt;
		cur_txt.color = Color.black;
		cur_txt.alignment = TextAlignmentOptions.Center;
		cur_txt.textWrappingMode = TextWrappingModes.Normal;
		cur_txt.overflowMode = TextOverflowModes.Overflow; // 或 Ellipsis

		// 4) 自动字号的上下限（配合 CanvasScaler = ScaleWithScreenSize）
		cur_txt.enableAutoSizing = true;
		cur_txt.fontSizeMin = 36f;   // 别太小
		cur_txt.fontSizeMax = 100f;  // 别太大
		// ScreenCapture.CaptureScreenshot("/Users/bo/Desktop/rating.png");
	}

	public void show_shock_icon(string cur_image_name)
	{
		var uiCanvas = script_main.canvasUI;

		// 2) 根容器（全屏）
		var root = new GameObject("shock_icon", typeof(RectTransform));
		root.transform.SetParent(uiCanvas.transform, false);
		var rootRT = root.GetComponent<RectTransform>();
		rootRT.anchorMin = Vector2.zero;
		rootRT.anchorMax = Vector2.one;
		rootRT.offsetMin = Vector2.zero;
		rootRT.offsetMax = Vector2.zero;

		// ===========================
		// 顶部：图片 y∈[0.65, 0.95]
		// ===========================
		// 顶部图像：充满 topBand（保持比例，按“封面图/裁剪边”策略）
		var imgGO = new GameObject("shock_icon_image", typeof(RectTransform), typeof(Image), typeof(AspectRatioFitter));
		imgGO.transform.SetParent(uiCanvas.transform, false);
		var imgRT = imgGO.GetComponent<RectTransform>();
		imgRT.anchorMin = new Vector2(0.5f, 0.5f);
		imgRT.anchorMax = new Vector2(0.5f, 0.5f);
		imgRT.pivot = new Vector2(0.5f, 0.5f);
		imgRT.anchoredPosition = Vector2.zero;
		imgRT.sizeDelta = new Vector2(Screen.height/5, Screen.height/5); // 高度由父容器提供

		var img = imgGO.GetComponent<Image>();
		var tex = Resources.Load<Texture2D>(cur_image_name);  // 例如 "ima_trivia_example"
		if (tex != null)
		{
			img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
		}
		img.raycastTarget = false;
		var fitter = imgGO.GetComponent<AspectRatioFitter>();
		fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		fitter.aspectRatio = tex ? (float)tex.width / tex.height : 1f / 1f;
	}



	public void show_trivia_win(string cur_txt, string cur_image_name)
	{
		// 1) 清屏 + 拿到 Overlay 画布（带 CanvasScaler=ScaleWithScreenSize）
		GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
		var uiCanvas = script_main.canvasUI;

		// 2) 根容器（全屏）
		var root = new GameObject("TriviaWin", typeof(RectTransform));
		root.transform.SetParent(uiCanvas.transform, false);
		var rootRT = root.GetComponent<RectTransform>();
		rootRT.anchorMin = Vector2.zero;
		rootRT.anchorMax = Vector2.one;
		rootRT.offsetMin = Vector2.zero;
		rootRT.offsetMax = Vector2.zero;

		// ===========================
		// 顶部：图片 y∈[0.65, 0.95]
		// ===========================
		// 顶部图像：充满 topBand（保持比例，按“封面图/裁剪边”策略）
		var imgGO = new GameObject("TriviaWin_Image", typeof(RectTransform), typeof(Image), typeof(AspectRatioFitter));
		imgGO.transform.SetParent(uiCanvas.transform, false);
		var imgRT = imgGO.GetComponent<RectTransform>();
		imgRT.anchorMin = new Vector2(0.5f, 0.55f);
		imgRT.anchorMax = new Vector2(0.5f, 0.95f);
		imgRT.pivot = new Vector2(0.5f, 0.5f);
		imgRT.anchoredPosition = Vector2.zero;
		imgRT.sizeDelta = new Vector2(0f, 0f); // 高度由父容器提供

		var img = imgGO.GetComponent<Image>();
		var tex = Resources.Load<Texture2D>(cur_image_name);  // 例如 "ima_trivia_example"
		if (tex != null)
		{
			img.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
		}
		img.raycastTarget = false;

		var fitter = imgGO.GetComponent<AspectRatioFitter>();
		fitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		fitter.aspectRatio = tex ? (float)tex.width / tex.height : 16f / 9f;

		// ===========================
		// 中部：文字 y∈[0.35, 0.65]，左右 8% 边距
		// ===========================
		var midBand = new GameObject("TriviaWin_trivia", typeof(RectTransform));
		midBand.transform.SetParent(root.transform, false);
		var midRT = midBand.GetComponent<RectTransform>();
		midRT.anchorMin = new Vector2(0.15f, 0.05f); // 左边距 8%
		midRT.anchorMax = new Vector2(0.85f, 0.5f); // 右边距 8%
		midRT.pivot = new Vector2(0.5f, 0.5f);
		midRT.offsetMin = Vector2.zero;
		midRT.offsetMax = Vector2.zero;

		// win_bg（你原来的背景块）
		var bgGO = new GameObject("TriviaWin_bg", typeof(RectTransform), typeof(Image));
		bgGO.transform.SetParent(midBand.transform, false);
		var bgRT = bgGO.GetComponent<RectTransform>();
		bgRT.anchorMin = Vector2.zero;
		bgRT.anchorMax = Vector2.one;
		bgRT.offsetMin = Vector2.zero;
		bgRT.offsetMax = Vector2.zero;

		var bgImg = bgGO.GetComponent<Image>();
		// 你可以换成九宫格圆角 sprite：bgImg.sprite=...; bgImg.type=Image.Type.Sliced;
		bgImg.color = new Color(1f, 1f, 1f, 0.92f); // 半透明白底

		// 文字（用你的 TMP 预制 object_txt）
		var textGO = Instantiate(object_txt);
		textGO.name = "TriviaText";
		textGO.transform.SetParent(midBand.transform, false);
		var textRT = textGO.GetComponent<RectTransform>();
		textRT.anchorMin = new Vector2(0.04f, 0.06f); // 在背景里再留一点内边距
		textRT.anchorMax = new Vector2(0.96f, 0.94f);
		textRT.offsetMin = Vector2.zero;
		textRT.offsetMax = Vector2.zero;

		var txt = textGO.GetComponent<TMP_Text>();
		var fontAsset = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
		if (fontAsset) txt.font = fontAsset;
		txt.text = cur_txt;
		txt.color = Color.black;                          // 白底黑字
		txt.alignment = TextAlignmentOptions.Center;
		txt.textWrappingMode = TextWrappingModes.Normal;
		txt.overflowMode = TextOverflowModes.Truncate;
		txt.margin = new Vector4(0f, 8f, 0f, 8f);        // 额外行间距
		txt.enableAutoSizing = true;
		txt.fontSizeMin = 36f;                            // 下限别太小
		txt.fontSizeMax = 120f;                           // 上限别太大

		//
		StartCoroutine(TypewriterEffect(txt, 0.1f));
		// ===========================
		// 底部：按钮 y∈[0.12, 0.28]，居中
		// ===========================
		// // 用你的按钮预制
		// script_main.m_object_button = Instantiate(object_button);
		// script_main.m_object_button.name = "btn";
		// script_main.m_object_button.transform.SetParent(uiCanvas.transform, false);
		// var btnRT = script_main.m_object_button.GetComponent<RectTransform>();
		// btnRT.anchorMin = new Vector2(0.35f, 0.05f);
		// btnRT.anchorMax = new Vector2(0.65f, 0.15f);
		// btnRT.offsetMin = Vector2.zero;
		// btnRT.offsetMax = Vector2.zero;
		// // 按钮文字
		// var btnLabel = script_main.m_object_button.transform.GetChild(1).GetComponent<TMP_Text>();
		// btnLabel.text = "Press Enter to rate";
		// btnLabel.color = Color.black;
		// btnLabel.enableAutoSizing = true;
		// btnLabel.fontSizeMin = 32f;
		// btnLabel.fontSizeMax = 100f;
		// btnLabel.alignment = TextAlignmentOptions.Center;
	}

	IEnumerator TypewriterEffect(TMP_Text textComponent, float charInterval)
	{
		textComponent.maxVisibleCharacters = 0;
		textComponent.ForceMeshUpdate();

		TMP_TextInfo textInfo = textComponent.textInfo;

		for (int w = 0; w < textInfo.wordCount; w++)
		{
			TMP_WordInfo wordInfo = textInfo.wordInfo[w];
			string word = wordInfo.GetWord();

			// ✅ 这个词在整段文本中的“真正结束位置”
			int wordEndCharIndex =
				wordInfo.firstCharacterIndex + wordInfo.characterCount;

			// ✅ 一次性显示整个 word（不会再半个）
			textComponent.maxVisibleCharacters = wordEndCharIndex;

			// 记录单词顺序 & 时间
			script_main.list_trivia_word_sequence.Add(word);
			script_main.list_trivia_time_sequence.Add(
				script_main.exp_time.ElapsedMilliseconds.ToString()
			);

			float wordPause = Random.Range(0.3f, 0.7f);
			yield return new WaitForSeconds(wordPause);
		}

		Invoke("clear_trivia_win", 0.5f);		
	}

	public void clear_trivia_win()
	{
		StartCoroutine(script_main.remove_trivia_items());
	}


	public void SimulateUIClick(Vector2 pos)
	{
		PointerEventData pointerData = new PointerEventData(EventSystem.current)
		{
			position = pos
		};
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(pointerData, results);

		foreach (var r in results)
		{
			var btn = r.gameObject.GetComponentInParent<Button>();
			if (btn != null && btn.interactable)
			{
				btn.onClick.Invoke();  // ✅ 直接调用，最稳定
				return;
			}
		}
	}

	public void reset_all_state()
	{
		script_main.ith_trial = 0;
		script_sess_risk_rating.ith_rating_trial = 0;
		script_sess_self_rating.ith_rating_trial = 0;
		script_main.bool_is_self_ratingSession = false;
		script_main.bool_is_risk_ratingSession = false;
		script_main.bool_is_decisionSession = false;
		script_main.bool_is_on_trigger_screen = false;
		script_main.bool_allow_record_cursor_pos = false;
		script_main.cursor_pos_record_timer = 0f;
		script_main.bool_is_postexp_scale1 = false;
		script_main.bool_is_postexp_scale2 = false;
		script_main.bool_is_postexp_scale3 = false;
		StopAllCoroutines();
	}

	public void reset_UI_gb()
	{
		var c = script_main.canvasUI;
		c.renderMode = RenderMode.ScreenSpaceOverlay;
		c.sortingOrder = 100;
		var scaler = c.GetComponent<UnityEngine.UI.CanvasScaler>();
		if (!scaler) scaler = c.gameObject.AddComponent<UnityEngine.UI.CanvasScaler>();
		scaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
		scaler.referenceResolution = new Vector2(1920, 1080);
		scaler.matchWidthOrHeight = 0.5f;   // 两边取中（或按你需要 0/1）
	}

	public float get_space_ori(float cur_ori)
    {
		float temp_angle_ref = 0f;

		if (script_main.fear_prob_1==0.06f)
		{
			temp_angle_ref = cur_ori;
		}
		if (script_main.fear_prob_1==0.12f)
		{
			temp_angle_ref = Mathf.DeltaAngle(90, cur_ori);
		}
		if (script_main.fear_prob_1==0.18f)
		{
			temp_angle_ref = Mathf.DeltaAngle(180, cur_ori);
		}
		if (script_main.fear_prob_1==0.24f)
		{
			temp_angle_ref = Mathf.DeltaAngle(270, cur_ori);
		}
		return temp_angle_ref;
    }


    public void WriteListToTxt(List<string> list, string fileName)
    {
        string projectRoot = Directory.GetParent(Application.dataPath).FullName;

        string filePath = Path.Combine(projectRoot, fileName);

        File.WriteAllLines(filePath, list);
    }

	public void WriteLineToFile(string fileName, params string[] cols)
	{
		string projectRoot = Directory.GetParent(Application.dataPath).FullName;
		string filePath = Path.Combine(projectRoot, fileName);

		using (StreamWriter writer = new StreamWriter(filePath, true))
		{
			writer.WriteLine(string.Join("\t", cols));
		}
	}


	public void WriteMousePos(int trialNumber, List<string> col1List, List<string> col2List)
	{
		string projectRoot = Directory.GetParent(Application.dataPath).FullName;
		string folderPath = Path.Combine(projectRoot, "output_" + script_main.cur_sub_id + "_mousePos"); 

		if (!Directory.Exists(folderPath))
		{
			Directory.CreateDirectory(folderPath);
		}
		string trialFileName = $"trial_{trialNumber:D2}.txt";
		string filePath = Path.Combine(folderPath, trialFileName);
		using (StreamWriter writer = new StreamWriter(filePath, false)) // false 表示覆盖写入
		{
			int rowCount = Mathf.Min(col1List.Count, col2List.Count); // 防止长度不一致
			for (int i = 0; i < rowCount; i++)
			{
				writer.WriteLine($"{col1List[i]}\t{col2List[i]}");
			}
		}
	}

	public void WriteWordSequence(int trialNumber, List<string> col1List, List<string> col2List)
	{
		string projectRoot = Directory.GetParent(Application.dataPath).FullName;
		string folderPath = Path.Combine(projectRoot, "output_" + script_main.cur_sub_id + "_wordSequence"); 

		if (!Directory.Exists(folderPath))
		{
			Directory.CreateDirectory(folderPath);
		}
		// 构造 trial 文件名，例如 trial_01.txt
		string trialFileName = $"trial_{trialNumber:D2}.txt";
		string filePath = Path.Combine(folderPath, trialFileName);

		using (StreamWriter writer = new StreamWriter(filePath, false)) // false 表示覆盖写入
		{
			int rowCount = Mathf.Min(col1List.Count, col2List.Count); // 防止长度不一致
			for (int i = 0; i < rowCount; i++)
			{
				writer.WriteLine($"{col1List[i]}\t{script_main.cur_time_trigger_sent.ToString()}\t{col2List[i]}");
			}
		}
	}

}



