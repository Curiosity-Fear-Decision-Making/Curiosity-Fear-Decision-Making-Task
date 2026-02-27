using System.Collections;
using UnityEngine;
public class script_scale_animation : MonoBehaviour
{

    float scale_size;
    public void play_zoom_animation_for_circle(float cur_scale_size)
    {
        scale_size = cur_scale_size;
        StopAllCoroutines();
        StartCoroutine(zoom_animation());
    }

    IEnumerator zoom_animation()
    {
        Vector3 original = transform.localScale;
        Vector3 target = original * scale_size;
        float time = 0f;

        while (time < 0.1f)
        {
            transform.localScale = Vector3.Lerp(original, target, time / 0.1f);
            time += Time.deltaTime;
            yield return null;
        }

        // yield return new WaitForSeconds(0.2f);

        time = 0f;
        while (time < 0.1f)
        {
            transform.localScale = Vector3.Lerp(target, original, time / 0.1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = original;

        // if (script_main.bool_is_inst_f_period)
        // {
        //     GameObject[] array_fn = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        //     foreach (GameObject obj in array_fn)
        //     {
        //         if (obj.name == "fn")
        //         {
        //             Destroy(obj);
        //         }
        //     }
        // }

    }


    public void play_scale_animation(float cur_scale_size)
    {
        scale_size = cur_scale_size;
        StopAllCoroutines();
        StartCoroutine(scale_animation());
    }

    IEnumerator scale_animation()
    {
        Vector3 original = transform.localScale;
        Vector3 target = original * scale_size;
        float time = 0f;

        while (time < 0.1f)
        {
            transform.localScale = Vector3.Lerp(original, target, time / 0.1f);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        while (time < 0.1f)
        {
            transform.localScale = Vector3.Lerp(target, original, time / 0.1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = original;


    }



}
