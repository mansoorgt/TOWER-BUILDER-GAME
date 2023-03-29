using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Crain : MonoBehaviour
{

    public static Crain instace;

    public Transform Crain_tr;
    public Rigidbody Crain_rb;
    public Slider Lr_slider,Ud_Slider;

    float CrainHight;
    public float MaxCrainHight;
    public float MinCrainHight;

    public float Lr_Min_X, Lr_Max_X;
    public float Ud_Min_Y=1, Ud_Max_Y=5;

    public Transform Cokka;
    [Range(0, 2)]
    public float cokka_Offset;
    


    public GameObject Pre_Box;
    
    GameObject Temp_Box;

    [Header("Linerender")]
    public LineRenderer Lr;
    public Vector3 LineRender_V3;

    Vector3 LastPos_lr;
    Vector3 LastPos_ud;
    private void Awake()
    {
        instace = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Lr_slider.minValue = Lr_Min_X;
        Lr_slider.maxValue = Lr_Max_X;

        Lr_slider.value = (Lr_Min_X + Lr_Max_X) / 2;

        Ud_Slider.minValue = Ud_Min_Y;
        Ud_Slider.maxValue = Ud_Max_Y;
        
        Ud_Slider.value = (Ud_Max_Y + Ud_Min_Y) / 2;

        CrainHight = MinCrainHight;


        ///Starting To set A postion to the cokka becose the coka falling in upwards ;

        

       // SoftJointLimit CokkaLimit = Cokka.GetComponent<ConfigurableJoint>().linearLimit;
       // CokkaLimit.limit = Ud_Slider.value - cokka_Offset;
       // Cokka.GetComponent<ConfigurableJoint>().linearLimit = CokkaLimit;



        //createBox();


        //  CreateRope();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //LeftAndRight
        Vector3 pos = new Vector3(Lr_slider.value, 0, 0);
       
        Crain_tr.localPosition = Vector3.Lerp(Crain_tr.localPosition, pos, 0.1f);

        //UpAndDown

        SoftJointLimit CokkaLimit = Cokka.GetComponent<ConfigurableJoint>().linearLimit;
        CokkaLimit.limit = Ud_Slider.value - cokka_Offset;
        Cokka.GetComponent<ConfigurableJoint>().linearLimit = CokkaLimit;

        
        Vector3 HightPos = new Vector3(0, CrainHight, 0);

        if (transform.position != HightPos)
        {
            transform.position = Vector3.Lerp(transform.position, HightPos, 2 * Time.deltaTime);

        }

        LineRender();


        //speedofLr

    //  float speed_lr = (Crain_tr.transform.localPosition.x - LastPos_lr.x);
    //  speed_lr = Mathf.Abs(speed_lr);
    //  LastPos_lr.x = Crain_tr.transform.localPosition.x;
    //
    //  //audioOFlr
    //  if (!Audio_manager.instance.chek_is_playing("crain_sound"))
    //  {
    //      Audio_manager.instance.Play("crain_sound", 0, true);
    //  }
    //  Audio_manager.instance.GetAudioSource("crain_sound").pitch =speed_lr;
    //  Debug.Log(speed_lr);

        //speedOfUp

       
      // 
      // if (!Audio_manager.instance.chek_is_playing("Crain_chain"))
      // {
      //     Audio_manager.instance.Play("Crain_chain", 0,true);
      // }
      // Audio_manager.instance.GetAudioSource("Crain_chain").pitch = speed_Ud;
    }
    public void createBox()
    {
        if (Cokka.GetComponent<Coka>().Is_trigger_Another_obj)
        {
            Debug.LogWarning("Is_inTrigger");
            return;
        }


        if (Cokka.GetComponent<Coka>().Collison_obj != null)
        {
            if (Temp_Box == null)
            {
                // grab 
                Cokka.GetComponent<Coka>().Is_trigger_Another_obj = false;
                Cokka.GetComponent<Coka>().Trigger_remainderBox.gameObject.SetActive(false);
                Temp_Box = Cokka.GetComponent<Coka>().Collison_obj;
                Destroy(Temp_Box.GetComponent<Rigidbody>());
                Temp_Box.transform.parent = Cokka.transform;

                Cokka.GetComponent<BoxCollider>().enabled = false;

                // Temp_Box.transform.localPosition = new Vector3(0, -Temp_Box.GetComponent<Object_sc>().Offest, 0);

               // Temp_Box.transform.localEulerAngles = new Vector3(0, Temp_Box.GetComponent<Object_sc>().StartRot.y, 0);
                Temp_Box.GetComponent<Object_sc>().IsRemoved = false;
            }

          
        }
        else
        {
            if (Temp_Box == null)
            {
                //Normal Cration

                //if the coka trigger in another object
                if (Cokka.GetComponent<Coka>().Is_trigger_Another_obj)
                {
                   // Debug.LogWarning("Is_inTrigger");

                   // Ud_Slider.value -= 2;
                  //  Cokka.GetComponent<BoxCollider>().size = new Vector3(Cokka.GetComponent<BoxCollider>().size.x, 0, Cokka.GetComponent<BoxCollider>().size.z);
                }

                Cokka.GetComponent<BoxCollider>().enabled = false;
                Vector3 SponPoint = Cokka.transform.position + new Vector3(0, -Ud_Slider.value, 0);
                
                Temp_Box = Instantiate(Pre_Box, SponPoint,Quaternion.identity);
                Temp_Box.transform.parent = Cokka.transform;
                Temp_Box.transform.localEulerAngles = Pre_Box.transform.eulerAngles;
                
                Temp_Box.transform.localPosition = new Vector3(0, -Temp_Box.GetComponent<Object_sc>().Offest, 0);

                //making a offset betweeen cokka and tempBOx
                Temp_Box.transform.localPosition+=new Vector3(0, Cokka.GetComponent<Coka>().cokka_mesh.transform.localPosition.y, 0);

                Temp_Box.GetComponent<Object_sc>().IsRemoved = false;
                Temp_Box.GetComponent<Object_sc>().IsPlacable = true;
                
            }
        }


        



    }
    public void RemoveBox()
    {

       // under the code ditermine when player press remove button The "tempBox" in  trigger right then remove it  
       // &&Temp_Box.GetComponent<Object_sc>().IsPlacable


       
        if (Temp_Box != null)
        {
            Temp_Box.GetComponent<Object_sc>().IsRemoved=true;
            Temp_Box.GetComponent<Object_sc>().IsPlaced = true;

            Temp_Box.transform.parent = null;

            Temp_Box.AddComponent<Rigidbody>();
            Temp_Box.GetComponent<Rigidbody>().isKinematic = false;
            Temp_Box.GetComponent<Rigidbody>().useGravity = true;
            Temp_Box.GetComponent<Rigidbody>().mass = 100;
 
            Ud_Slider.value -= 0.5f;

            Temp_Box = null;
            Cokka.GetComponent<BoxCollider>().enabled = true;
            Cokka.GetComponent<Coka>().Collison_obj = null;

        }
        else
        {
            Debug.Log("Is_notPlacable");
        }

       

       // Temp_Box.GetComponent<Rigidbody>().drag = 0;
       // Destroy(Temp_box_Joint);
    }
    public void DelecteSlectedBox()
    {
        if (Temp_Box != null)
        {
            Temp_Box.GetComponent<Object_sc>().Is_destroyed = true;


           if (Temp_Box.GetComponent<Object_sc>().Trail_box != null)
            {

                //when adding list of trailboxs autoMaticaly adding a empty field so i added this  
                Temp_Box.GetComponent<Object_sc>().Trail_box.RemoveAt(0);

                foreach(TrailTile m_trail in Temp_Box.GetComponent<Object_sc>().Trail_box)
                {
                    m_trail.Meta.SetInt("Same", 0);
                    m_trail.Meta.SetInt("Wrong", 0);
                    
                    m_trail.t = 0;
                    m_trail.Fade_Out = true;
                }
               
            }


            Cokka.GetComponent<BoxCollider>().enabled = true;
            Cokka.GetComponent<Coka>().Collison_obj  = null;
            Destroy(Temp_Box);
            Temp_Box = null;

        }
    }
    public void IncreaseHight()
    {
        if (CrainHight <= MaxCrainHight)
        {
            CrainHight++;
        }
        if (!Audio_manager.instance.chek_is_playing("crain_Up"))
        {
            Audio_manager.instance.Play("crain_Up", false);
        }
       // Enveronment_sc.instace.ChangeAMount_increase();
    }
    public void DecreaseHight()
    {
        if (CrainHight >= MinCrainHight)
        {
            CrainHight--;
        }
        if (!Audio_manager.instance.chek_is_playing("crain_Up"))
        {
            Audio_manager.instance.Play("crain_Up",false);
        }

     //   Enveronment_sc.instace.ChangeAMount_decrease();
    }
    
    void LineRender()
    {
        Vector3 FirstPos = Crain_rb.transform.position + LineRender_V3;
        Lr.SetPosition(0, FirstPos);

        Vector3 EndPos = Cokka.transform.position + new Vector3(0, 0.1f, 0);
        Lr.SetPosition(1, EndPos);
       
    }
    
}
