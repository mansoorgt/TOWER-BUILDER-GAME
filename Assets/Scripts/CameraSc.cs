using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;
public class CameraSc : MonoBehaviour
{
    public Transform crainP;
    public Vector3 Offest;
    public Vector3 ZoomPos;

    public float speed=2f;
    
    //zoom;
    public float zoomOutmin = 5;
    public float zoomOutMx = 10;

 
    public PlayableDirector Playable;
    public CanvasGroup Main_canvas_group;
    // Update is called once per frame

    private void OnEnable()
    {

        ZoomPos.z = zoomOutMx / 2;
        if (Playable.state == PlayState.Playing)
        {

            Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);


            float dd = (float)Playable.duration;
            Invoke(nameof(After_intro), dd);
        }

    }
    
    void LateUpdate()
    {


        if (Playable.state == PlayState.Playing)
        {
            Debug.Log("playing");
            

            Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);
            return;
        }

        ZoomPos.z = Mathf.Clamp(ZoomPos.z, zoomOutmin, zoomOutMx);
        Vector3 To_pos = crainP.position +Offest+(ZoomPos);
        transform.position = Vector3.Lerp(transform.position, To_pos, speed * Time.deltaTime);

    }
    public void Zoom(float Zoom_float)
    {
        if (Zoom_float > 0)
        {
            ZoomPos += new Vector3(0, 0, Zoom_float);
          
            //Offest = new Vector3(Offest.x, Offest.y, Mathf.Clamp(Offest.z + Zoom_float, zoomOutmin, zoomOutMx));
        }
        if(Zoom_float<-0)
        {

            ZoomPos += new Vector3(0, 0, Zoom_float);
          
            // Offest = new Vector3(Offest.x, Offest.y, Mathf.Clamp(Offest.z - Zoom_float, zoomOutmin, zoomOutMx));
        }
       
    }
    
   void After_intro()
    {
        Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", ES3.Load<float>("Sfx_volume"));
      
        Main_canvas_group.alpha = Mathf.Lerp(0, 1, 1);
    }
}
