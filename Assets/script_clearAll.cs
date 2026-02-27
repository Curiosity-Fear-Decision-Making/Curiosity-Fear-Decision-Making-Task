
using UnityEngine;

public class script_clearAll : MonoBehaviour
{

    public void clear_object()
    {
        CancelInvoke();
        GameObject[] array_txt = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in array_txt)
        {
            if (obj.name == "txt")
            {
                Destroy(obj);
            }
        }
        GameObject[] array_btn = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in array_btn)
        {
            if (obj.name == "btn")
            {
                Destroy(obj);
            }
        }
        GameObject[] bullseye_circle1 = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in bullseye_circle1)
        {
            if (obj.name == "object_circle1_bullseye")
            {
                Destroy(obj);
            }
        }
        GameObject[] bullseye_circle2 = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in bullseye_circle2)
        {
            if (obj.name == "object_circle2_bullseye")
            {
                Destroy(obj);
            }
        }
        GameObject[] txt_gb = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in txt_gb)
        {
            if (obj.name == "txt_bg")
            {
                Destroy(obj);
            }
        }

        Destroy(GameObject.Find("object_fix"));
        Destroy(GameObject.Find("subid"));
        Destroy(GameObject.Find("space"));
        Destroy(GameObject.Find("object_circle1"));
        Destroy(GameObject.Find("object_circle2"));
        Destroy(GameObject.Find("legend_c"));
        Destroy(GameObject.Find("legend_f"));
        Destroy(GameObject.Find("controler"));
        Destroy(GameObject.Find("TriviaImage_example"));
        Destroy(GameObject.Find("scrollview"));
        Destroy(GameObject.Find("TriviaWin"));
        Destroy(GameObject.Find("TriviaWin_Image"));
        Destroy(GameObject.Find("TriviaWin_trivia"));
        Destroy(GameObject.Find("TriviaWin_bg"));
        Destroy(GameObject.Find("image_object_trivia"));
        Destroy(GameObject.Find("outcome_txt"));
        Destroy(GameObject.Find("RatingRow"));
        Destroy(GameObject.Find("RatingTitle"));
        Destroy(GameObject.Find("shock_icon"));
        Destroy(GameObject.Find("shock_icon_image"));
        
        GameObject[] id_sub = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in id_sub)
        {
            if (obj.name == "input_subID")
            {
                Destroy(obj);
            }
        }


        GameObject[] RatingCircle = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in RatingCircle)
        {
            if (obj.name == "RatingCircle")
            {
                Destroy(obj);
            }
        }
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in allObjects)
        {
            if (obj.name == "c_label")
            {
                Destroy(obj);
            }
        }
        GameObject[] array_fn = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in array_fn)
        {
            if (obj.name == "fn")
            {
                Destroy(obj);
            }
        }
		// for (int b = 0; b < 6; b++)
		// {
		// 	Destroy(GameObject.Find("rating_button" + (b + 1).ToString()));
	    // }

        GameObject[] rating_buttons = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        foreach (GameObject obj in rating_buttons)
        {
            if (obj.name.StartsWith("rating_button"))
            {
                Destroy(obj);
            }
        }


    }




}