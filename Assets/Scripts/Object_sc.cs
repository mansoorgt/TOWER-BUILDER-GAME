using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_sc : MonoBehaviour
{
    public bool IsRemoved;
    public bool IsPlaced;
    public bool IsPlacable;
    public bool Is_falsed;
    public float Offest=1;
    public bool Is_destroyed;

    public List<TrailTile> Trail_box;

    public Vector3 StartRot{ set; get; }

    public int Col_Count;

    public string ground,thara;
         
   
    // Start is called before the first frame update
    void Start()
    {
        IsPlaced = false;
        Is_destroyed = false;
        StartRot = transform.localEulerAngles;
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider != null&&IsRemoved)
        {
            if (collision.collider.tag != "Coka"&&collision.collider.tag!="Ground")
            {

                Col_Count++;
                
                IsPlaced = true;
                Audio_manager.instance.Play("brick_fall", true);
               
            }
              
        }
        thara = collision.collider.gameObject.name;
        if (collision.collider.tag=="Ground"&&Col_Count > 1&&collision.collider.tag!="Thara")
        {
          

         //   GameManger.instace.LifeTextUpdate(1);
          //  Debug.Log("isFalsed");
          //  IsPlaced = false;
        }
        
        
    }
   

}
