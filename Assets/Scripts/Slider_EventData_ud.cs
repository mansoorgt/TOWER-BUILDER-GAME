
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slider_EventData_ud : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    public RectTransform Slider_point;

    

    public void OnBeginDrag(PointerEventData eventData)
    {
       if (!Audio_manager.instance.chek_is_playing("Crain_chain"))
       {
           Audio_manager.instance.Play("Crain_chain", 0, true);
       }
    }
    Vector3 last_point;
  
    public void OnEndDrag(PointerEventData eventData)
    {
        Audio_manager.instance.GetAudioSource("Crain_chain").Stop();

    }
    private void FixedUpdate()
    {   
           float speed_Ud = (Slider_point.position - last_point).magnitude;
           // speed_lr = Mathf.Abs(speed_Ud);
         
           last_point = Slider_point.position;


           if (speed_Ud ==0)
           {

                 Audio_manager.instance.GetAudioSource("Crain_chain").volume = Mathf.Lerp(Audio_manager.instance.GetAudioSource("Crain_chain").volume,0, 0.5f);
           }
           else
           {
            if (Audio_manager.instance.GetAudioSource("Crain_chain").volume < 0.5f)
            {
                Audio_manager.instance.GetAudioSource("Crain_chain").volume = Mathf.Lerp(Audio_manager.instance.GetAudioSource("Crain_chain").volume, 0.5f, 0.5f);

            }

            if (!Audio_manager.instance.chek_is_playing("Crain_chain"))
             {
                
                 Audio_manager.instance.Play("Crain_chain", 0, true);
             }
           }
        

    }
}
