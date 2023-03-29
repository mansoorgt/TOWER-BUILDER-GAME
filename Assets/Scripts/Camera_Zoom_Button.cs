using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Camera_Zoom_Button : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler
{

    Vector2 DragstartPos;
    public bool dragEnd;
    public float clamp_posY;
    public CameraSc CameraSc;
    public void OnBeginDrag(PointerEventData eventData)
    {
       // DragstartPos = Input.mousePosition;
    
       DragstartPos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    { 
        gameObject.transform.localPosition = -new Vector3(0,Mathf.Clamp(DragstartPos.y - Input.mousePosition.y, -clamp_posY,clamp_posY),0);
        if (gameObject.transform.localPosition.y > 1)
        {
 
            CameraSc.Zoom(1f);
        }
        if(gameObject.transform.localPosition.y < -1)
        {

            CameraSc.Zoom(-1);
        }
        
      //  gameObject.transform.localPosition = new Vector2(0, DragstartPos.y - Input.GetTouch(0).position.y);


    }
    private void Update()
    {
        if (dragEnd)
        {
            gameObject.transform.localPosition = Vector3.Lerp(gameObject.transform.localPosition,new Vector3(0,0,0), 1f);
        }
        if (gameObject.transform.localPosition.y == 0)
        {
            dragEnd = false;
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        dragEnd = true;
    }
}
