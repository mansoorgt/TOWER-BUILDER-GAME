using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrailTile : MonoBehaviour
{
    public Shader Trail_box_Shader;
    public Texture m_Texture;
    public Material Meta { set; get; }

    public bool Fade_Out;
    bool Fade_In;

    float Velocity;


    public float t { set; get; }
    public float Max,min;

    public GameObject Placed_obj;


    public TrailManeger Parent_trailManeger;


    //obj placed in currect place
    public bool Is_placed;

   
    private void Start()
    {

      //  Parent_trailManeger = transform.parent.transform.parent.GetComponent<TrailManeger>();
        Meta = new Material(Trail_box_Shader);
        Meta.SetTexture("Texture", m_Texture);

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        mesh.material = Meta;

        Meta.SetInt("Same", 0);
        Meta.SetInt("Wrong", 0);

       
    }
    private void Update()
    {
        if (Fade_Out)
        {
            Meta.SetFloat("Fade_Value", Mathf.Lerp(Meta.GetFloat("Fade_Value"), Max, t));
            t += 0.1f * Time.deltaTime;

        }

    }
    private void OnTriggerEnter(Collider other)
    {

     //   print(other.gameObject.name + "triggerd");
        if (other.gameObject.tag != "Coka")
        {
            if (!other.GetComponent<Object_sc>().Trail_box.Contains(gameObject.GetComponent<TrailTile>()))
            {
                other.GetComponent<Object_sc>().Trail_box.Add(gameObject.GetComponent<TrailTile>());
            }

            if (other.gameObject.tag == this.gameObject.tag)
            {
              //  Debug = "Same_one";

                Meta.SetInt("Same", 1);
                Meta.SetInt("Wrong", 0);
                
                other.gameObject.GetComponent<Object_sc>().IsPlacable = true;

                //other.GetComponent<Object_sc>().Trail_box = gameObject.GetComponent<TrailTile>();

                t = 0;
                Fade_Out = false;


            }
            else
            {
              //  Debug = "WrongThing";


               
               other.gameObject.GetComponent<Object_sc>().IsPlacable = false;
                

                Meta.SetInt("Same", 0);
                Meta.SetInt("Wrong", 1);



            }
        }

        

        


    }
    private void OnTriggerStay(Collider other)
    {

        if(other.gameObject.tag != "Coka")
        {

            if (!other.GetComponent<Object_sc>().Trail_box.Contains(gameObject.GetComponent<TrailTile>()))
            {
                other.GetComponent<Object_sc>().Trail_box.Add(gameObject.GetComponent<TrailTile>());
            }
          

            if (other.gameObject.tag == this.gameObject.tag && other.gameObject.GetComponent<Object_sc>().IsPlaced)
            {
             //   Debug = "Trail_gos_alpha";

                
                Meta.SetFloat("Fade_Value", Mathf.Lerp(Meta.GetFloat("Fade_Value"), min, t));
                t += 0.1f * Time.deltaTime;

                Placed_obj = other.gameObject;

                if (Is_placed)
                {
                    return;
                }

                
                Is_placed = true;


                if (Is_placed)
                {
                    Parent_trailManeger.Finished_count++;
                    GameManger.instace.One_More_trail_add();
               
                    if (Parent_trailManeger.Finished_count == Parent_trailManeger.Max_trailCount)
                    {
                        Invoke(nameof(check_is_fineshed), 3);
                    }
                }

               
               // other.GetComponent<Object_sc>().Trail_box = gameObject.GetComponent<TrailTile>();
            }
            else
            {
             //   Debug = "Trail_gos_red";

                // Meta.SetFloat("Fade_Value", Mathf.Lerp(3, -2, 2 * Time.deltaTime));
            }
            
          
        }
      
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Coka")
        {

            other.GetComponent<Object_sc>().Trail_box.Remove(gameObject.GetComponent<TrailTile>());
            // if (other.gameObject==Placed_obj)
            // {
            //     Debug = "Trail_retun_to_normal";
            //
            // }

        }




        //is gameobeject layer "MainObject" layer
        if (other.gameObject.layer == 9)
        {

            Meta.SetInt("Same", 0);
            Meta.SetInt("Wrong", 0);

            t = 0;
            Fade_Out = true;


            other.GetComponent<Object_sc>().IsPlacable = true;
            
            //this thig actulay want to put to in crain.cs/ctreatBox but not getting right result so put there
            other.GetComponent<Object_sc>().IsPlaced = false;

          //  

            
            Placed_obj = null;

           if (!Is_placed)
           {
               return;
           }

           
            Is_placed = false;

            Parent_trailManeger.Finished_count--;
            GameManger.instace.One_More_trail_Less();
        }

       
       
                    
    }

    public void check_is_fineshed()
    {
        if (Parent_trailManeger.Finished_count != Parent_trailManeger.Max_trailCount)
        {
            return;
        }

        GameManger.instace.Finished();
    }
}
