using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Slider_EventData_lr : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    public RectTransform Slider_point;
    public void OnBeginDrag(PointerEventData eventData)
    {
       if (!Audio_manager.instance.chek_is_playing("crain_Lr"))
       {
           Audio_manager.instance.Play("crain_Lr", 0, true);
       }
    }
    Vector3 last_point;
  
    public void OnEndDrag(PointerEventData eventData)
    {
     //   Audio_manager.instance.GetAudioSource("crain_Lr").Stop();

    }
    private void Update()
    {   
           float speed_Ud = (Slider_point.position - last_point).magnitude;
        // speed_lr = Mathf.Abs(speed_Ud);

           last_point = Slider_point.position;
        
           if (speed_Ud ==0)
           {

                  Audio_manager.instance.GetAudioSource("crain_Lr").volume = Mathf.Lerp(Audio_manager.instance.GetAudioSource("crain_Lr").volume,0,0.1f);
           }
           else
           {
            if (Audio_manager.instance.GetAudioSource("crain_Lr").volume < 1f)
            {
                Audio_manager.instance.GetAudioSource("crain_Lr").volume = Mathf.Lerp(Audio_manager.instance.GetAudioSource("crain_Lr").volume,1f,0.1f);

            }

            if (!Audio_manager.instance.chek_is_playing("crain_Lr"))
             {
                
                 Audio_manager.instance.Play("crain_Lr", 0, true);
             }
           }
        

    }
}
