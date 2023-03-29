using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Invertory_button_sc : MonoBehaviour
{
    public Sprite Normal_Plane;
    public Sprite sleceted_plane;

    public Image Icon_image_comp;
    //Adding when Instatiate to the invetory box;
    public Sprite Icon_sprite;


    // Start is called before the first frame update
    void Start()
    {
        Icon_image_comp.sprite = Icon_sprite;
    }

   
}
