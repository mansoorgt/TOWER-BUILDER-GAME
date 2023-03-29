using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Coka : MonoBehaviour
{
    
    public GameObject Collison_obj;

    public GameObject cokka_mesh;
    public GameObject Trigger_remainderBox;
    public Material Trigger_reminder_meta;
    public bool Is_trigger_Another_obj { set; get; }
    
    private void Start()
    {
       // Trigger_reminder_meta = Trigger_remainderBox.GetComponent<MeshRenderer>().material;


    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.layer == 9)
        {
            Collison_obj = collision.gameObject;

            Is_trigger_Another_obj = false;

           
            Trigger_reminder_meta.SetInt("Push_up", 1);
        }


        Debug.Log(collision.gameObject.name);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            
            Collison_obj = null;

            Is_trigger_Another_obj = true;

            Trigger_reminder_meta.SetInt("Push_up", 0);
        }
    }
    void OnTriggerEnter(Collider other)
    {
                                         
        if (other.gameObject.layer == 9)
        {

            

            Is_trigger_Another_obj = true;

            if (other.GetComponent<Object_sc>().Col_Count>=1)
            {
                Debug.Log("trigger");
                Trigger_remainderBox.SetActive(true);
            }
          

            Trigger_reminder_meta.SetInt("Push_up", 0);


        }
                                          
        if (other.gameObject.tag == "Thara")
        {
            
            Is_trigger_Another_obj = true;

            // checkig is placed or not Becuse when remoce prebox is showing the wrong mark  is showing the wrong mark
            if(other.GetComponent<Object_sc>() != null)
            {
                if (other.GetComponent<Object_sc>().Col_Count > 1)
                {
                    Debug.Log("isOopedn");
                    Trigger_remainderBox.SetActive(true);
                }
            }
           
            Trigger_reminder_meta.SetInt("Push_up", 0);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {

            
            Debug.Log(gameObject.name+"exited");
            Is_trigger_Another_obj =false;
            Trigger_remainderBox.SetActive(false);
        }
        if (other.gameObject.tag == "Thara")
        {
            Debug.Log("exited");
            Is_trigger_Another_obj = false;
            Trigger_remainderBox.SetActive(false);
        }
    }
}
