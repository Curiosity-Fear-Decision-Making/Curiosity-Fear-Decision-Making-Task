using UnityEngine;
using UnityEngine.UI;

namespace RainbowArt.CleanFlatUI
{
    [ExecuteAlways]
    public class script_circle_pregressbar : MonoBehaviour
    {
        // float minValue = 0f;
        float maxValue = 100.0f;        

        [SerializeField]
        Image foreground;
        float currentValue = 100f;
        // float decreaseRate = 100 / 5; //5s
        
       

        // void Start()
        // {
        // }


        public void start_circle_timer()
        {
            currentValue = maxValue;
            // script_main.bool_circle_timer_start = true;
        }

        void Update()
        {
            // if (script_main.bool_circle_timer_start)
            // {
                
            //     currentValue -= decreaseRate * Time.deltaTime;
            //     if (currentValue <= minValue)
            //     {
            //         script_main.bool_circle_timer_start = false;
            //         currentValue = minValue;

            //         script_main.cursor_pos_record_timer = 0;
            //         script_main.bool_allow_record_cursor_pos = false;
            //         script_main.bool_clickbait_timeout = true;
            //     }
            //     UpdateForeground();
            // }
        }

        void UpdateForeground()
        {
            foreground.fillAmount = currentValue / maxValue;
        }

    }
}