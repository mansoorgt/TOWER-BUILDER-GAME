using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Circle_slider : MonoBehaviour ,IBeginDragHandler,IDragHandler
{
   public  Vector2 startV2,endV2;
    public RectTransform handle; 
    public void OnBeginDrag(PointerEventData eventData)
    {
        startV2 = eventData.position;
    }

    public void OnDrag(PointerEventData data)
    {
        if (data.dragging)
        {
            endV2 = data.position;
            Vector2 deltaV2 = endV2 - startV2;
            deltaV2 = deltaV2.normalized;
            handle.Rotate(new Vector3(0, 0, -deltaV2.y), Space.Self);

            startV2 = endV2;
        }
    }
}
