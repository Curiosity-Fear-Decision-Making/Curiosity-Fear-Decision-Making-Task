using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class script_menu : MonoBehaviour
{
    public GameObject object_button;
    public GameObject object_txt;
    GameObject m_object_button;
    GameObject m_object_txt;
	public GameObject object_input_subID;
	GameObject m_object_input_subID;
    public static TMP_InputField input_subID;

    public void show_menu()
    {
        GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();

        // UI reset
        GameObject.Find("Directional Light").GetComponent<script_utility>().reset_UI_gb();
        // system cursor
        script_main.cursor_image.enabled = false;
        script_main.allowCursorMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //
        script_main.bool_is_in_menu = true;

        script_main.bool_is_self_ratingSession = false;
        script_main.bool_is_risk_ratingSession = false;
        script_main.bool_is_decisionSession = false;
        script_main.bool_is_QuestionnaireSession = false;

        script_main.bool_is_on_trigger_screen=false;

        script_main.bool_is_postexp_scale3 = false;
        script_main.bool_is_postexp_scale2 = false;
        script_main.bool_is_postexp_scale1 = false;
        script_main.ith_trial = 0;
                
        // 实例化预制体
        m_object_input_subID = Instantiate(object_input_subID);
        m_object_input_subID.name = "input_subID";
        m_object_input_subID.transform.SetParent(script_main.canvasUI.transform, false);

        // 设置外层 RectTransform（如果需要）
        RectTransform outerRT = m_object_input_subID.GetComponent<RectTransform>();
        outerRT.anchorMin = new Vector2(0.5f, 0.7f);
        outerRT.anchorMax = new Vector2(0.5f, 0.7f);
        outerRT.anchoredPosition = Vector2.zero;

        Transform innerInputField = m_object_input_subID.transform.Find("InputField (TMP)");
        RectTransform inputFieldRT = innerInputField.GetComponent<RectTransform>();
        inputFieldRT.sizeDelta = new Vector2(300, 80);  

        input_subID = innerInputField.GetComponent<TMP_InputField>();
        TMP_Text textComponent = input_subID.textComponent;
        textComponent.enableAutoSizing = true;
        textComponent.fontSizeMin = 20;
        textComponent.fontSizeMax = 100;

        //
        script_main.menu_btn_self_rating = show_btn(new Vector2(0.5f, 0.57f), "  (V). Self-interest Rating");
        script_main.menu_btn_risk_rating = show_btn(new Vector2(0.5f, 0.42f), "  (B). Risk Rating");
        script_main.menu_btn_start = show_btn(new Vector2(0.5f, 0.27f), "  (N). Start Task");
        script_main.menu_btn_questionnaire = show_btn(new Vector2(0.5f, 0.12f), "  (M). Questionnaires(outside MRI)");

        script_main.menu_btn_self_rating.GetComponentInChildren<Button>().interactable = true;
        script_main.menu_btn_risk_rating.GetComponentInChildren<Button>().interactable = true;
        script_main.menu_btn_start.GetComponentInChildren<Button>().interactable = true;
        script_main.menu_btn_questionnaire.GetComponentInChildren<Button>().interactable = true;

        show_text("Curiosity Gamble Task", "top");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.V))
        {
            //
            script_main.bool_is_self_ratingSession = true;
            script_main.bool_is_risk_ratingSession = false;
            script_main.bool_is_decisionSession = false;
            script_main.bool_is_QuestionnaireSession = false;
            //
            script_sess_self_rating.ith_rating_trial = 0;
            // idx randomized trials
            script_main.list_random_idx = new List<int>();
            script_main.list_random_idx = Enumerable.Range(0, script_main.list_condition_trivia_idx.Count).ToList();
            script_main.list_random_idx = script_utility.ShuffleList(script_main.list_random_idx);
            //
            script_main.bool_is_in_menu = false;
            script_main.cur_sub_id = input_subID.text;
            GameObject.Find("Directional Light").GetComponent<script_main>().trigger_screen();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            //
            script_main.bool_is_self_ratingSession = false;
            script_main.bool_is_risk_ratingSession = true;
            script_main.bool_is_decisionSession = false;
            script_main.bool_is_QuestionnaireSession = false;
            //
            script_sess_risk_rating.ith_rating_trial = 0;
            // idx randomized trials
            script_main.list_random_idx = new List<int>();
            script_main.list_random_idx = Enumerable.Range(0, script_main.list_condition_risk_sess.Count).ToList();
            script_main.list_random_idx = script_utility.ShuffleList(script_main.list_random_idx);
            //
            script_main.bool_is_in_menu = false;
            script_main.cur_sub_id = input_subID.text;
            GameObject.Find("Directional Light").GetComponent<script_main>().trigger_screen();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            //
            script_main.bool_is_self_ratingSession = false;
            script_main.bool_is_risk_ratingSession = false;
            script_main.bool_is_decisionSession = true;
            script_main.bool_is_QuestionnaireSession = false;

            //
            script_main.ith_trial = 0;
            // idx randomized trials
            script_main.list_random_idx = new List<int>();
            script_main.list_random_idx = Enumerable.Range(0, script_main.list_condition_trivia_idx.Count).ToList();
            script_main.list_random_idx = script_utility.ShuffleList(script_main.list_random_idx);
            //
            script_main.bool_is_in_menu = false;
            script_main.cur_sub_id = input_subID.text;
            GameObject.Find("Directional Light").GetComponent<script_main>().trigger_screen();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            //
            script_main.bool_is_self_ratingSession = false;
            script_main.bool_is_risk_ratingSession = false;
            script_main.bool_is_decisionSession = false;
            script_main.bool_is_QuestionnaireSession = true;
            //
            GameObject.Find("Directional Light").GetComponent<script_clearAll>().clear_object();
			script_main.bool_is_postexp_scale1 = true;
			// system cursor
			script_main.cursor_image.enabled = false;
			script_main.allowCursorMove = false;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			//
            script_main.cur_sub_id = input_subID.text;
			GameObject.Find("Directional Light").GetComponent<script_utility>().show_scale();
        }
    }



    public GameObject show_btn(Vector2 anchor, string btn_txt)
    {
        // 1) 实例化
        m_object_button = Instantiate(object_button);
        m_object_button.transform.SetParent(script_main.canvasUI.transform, false);
        m_object_button.name = "btn";

        // 2) 定位与尺寸（参考分辨率下的设计尺寸，CanvasScaler 会负责缩放到真机像素）
        var rt = m_object_button.GetComponent<RectTransform>();
        rt.anchorMin = anchor;
        rt.anchorMax = anchor;
        rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = Vector2.zero;

        const float kRefWidth = 750f;   // 你的按钮设计宽
        const float kRefHeight = 100f;   // 你的按钮设计高
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, kRefWidth);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, kRefHeight);

        // 3) 文本
        var label = m_object_button.transform.GetChild(1).GetComponent<TMP_Text>();
        label.enableAutoSizing = false;
        label.text = btn_txt;
        label.color = Color.black;
        label.alignment = TextAlignmentOptions.Left;
        label.textWrappingMode = TextWrappingModes.Normal;

        // 4) 先按按钮高度给一个“初始字号”（随分辨率等比放大）
        float sizeByHeight = rt.rect.height * 0.42f;

        // 5) 设最小/最大保护，避免极端分辨率下过大/过小
        const float minPt = 28f;
        const float maxPt = 96f;
        float fontSize = Mathf.Clamp(sizeByHeight, minPt, maxPt);
        float paddingX = 48f;
        float availableW = Mathf.Max(0.0f, rt.rect.width - paddingX);
        fontSize = FitToWidth(label, availableW, fontSize, minPt);
        label.fontSize = fontSize;
        return m_object_button;
    }

    private float FitToWidth(TMP_Text label, float maxWidth, float startSize, float minSize)
    {
        // 先用起始字号试一次
        label.fontSize = startSize;
        // 用 label 当前设置计算它的首选尺寸（不渲染、只测量）
        Vector2 pref = label.GetPreferredValues(label.text, Mathf.Infinity, Mathf.Infinity);
        if (pref.x <= maxWidth || startSize <= minSize) return startSize;

        // 超宽 → 比例压缩：新字号 = 旧字号 * (可用宽 / 需要宽)
        float scale = maxWidth / pref.x;
        float newSize = Mathf.Max(minSize, startSize * scale);

        // 再做一次细调，避免浮点误差导致 1-2 像素超出
        label.fontSize = newSize;
        pref = label.GetPreferredValues(label.text, Mathf.Infinity, Mathf.Infinity);
        if (pref.x > maxWidth)
        {
            // 再略缩一点点
            float tinyScale = maxWidth / pref.x;
            newSize = Mathf.Max(minSize, newSize * tinyScale);
        }
        return newSize;
    }


    void show_text(string cur_txt, string block = "middle")
    {
        m_object_txt = Instantiate(object_txt);
        m_object_txt.transform.SetParent(script_main.canvasUI.transform, false);
        m_object_txt.name = "txt";

        var rt = m_object_txt.GetComponent<RectTransform>();
        if (block == "top") {
            rt.anchorMin = new Vector2(0.08f, 0.75f);
            rt.anchorMax = new Vector2(0.92f, 0.95f);
        } else if (block == "bottom") {
            rt.anchorMin = new Vector2(0.08f, 0.18f);
            rt.anchorMax = new Vector2(0.92f, 0.38f);
        } else { // middle
            rt.anchorMin = new Vector2(0.08f, 0.40f);
            rt.anchorMax = new Vector2(0.92f, 0.72f);
        }
        rt.offsetMin = rt.offsetMax = Vector2.zero; // 占满该区域

        var txt = m_object_txt.GetComponent<TMP_Text>();
        txt.text = cur_txt;
        txt.enableAutoSizing = true;
        txt.fontSizeMin = script_main.min_fontsize;
        txt.fontSizeMax = script_main.max_fontsize;
        txt.textWrappingMode = TextWrappingModes.Normal;
        txt.overflowMode = TextOverflowModes.Overflow;
        txt.color = Color.black;
        txt.alignment = TextAlignmentOptions.Center;
    }




}


