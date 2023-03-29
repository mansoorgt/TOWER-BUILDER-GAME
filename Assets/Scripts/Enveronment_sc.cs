using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[ExecuteInEditMode]
public class Enveronment_sc : MonoBehaviour
{
    public static Enveronment_sc instace;

   // public List<Material> TempMetas;
   // public List<Material> Metas;
   
    public Shader Shader;

    [Range(0,50)]
    public float Amount=0;
    public float Max_amount = 10;

    float changedAmount;


    //CameraClaculation for CurveWorld Amount
    Camera MainCamera;
    [Range(0,100)]
    public float Hightmax;
    

    private void Awake()
    {
        instace = this;


   //   for(int i = 0; i < transform.childCount; i++)
   //     {
   //         if (transform.GetChild(i).GetComponent<MeshRenderer>() != null)
   //         {
   //
   //             foreach(Material meta in transform.GetChild(i).GetComponent<MeshRenderer>().materials)
   //             {
   //                 TempMetas.Add(meta);
   //                
   //             }
   //             
   //         }
   //       
   //     }
   //
   //   for(int i = 0; i < TempMetas.Count; i++)
   //     {
   //         if (TempMetas[i].shader == Shader)
   //         {
   //             Metas.Add(TempMetas[i]);
   //         }
   //     }
   //

        MainCamera = Camera.main;
    }
    private void Update()
    {
      // foreach(Material Shader in Metas)
      // {
      //     Shader.SetFloat("Amount", Amount);
      // }
      //
      // Amount = Mathf.Lerp(Amount, changedAmount, 0.01f);
      //
        calculatiomCurveAmount();
    }

    public float  amount; 
    public void ChangeAMount_increase()
    {
       // changedAmount += amount;
    }
    public void ChangeAMount_decrease()
    {
      //  changedAmount -= amount;
    }
    public Vector3 Start_curve_point;
    void calculatiomCurveAmount()
    {
        Vector3 StartPoint = new Vector3(0, 0, 0);
        Vector3 Max_hight = new Vector3(0, Hightmax, 0);
        Start_curve_point = new Vector3(0, Hightmax / 4, 0);

        //distacew between max hight and start curve point 
        float Distance_hight = Max_hight.y - Start_curve_point.y ;
        float Campos = MainCamera.transform.position.y-Start_curve_point.y;
        
        Debug.DrawLine(StartPoint+new Vector3(10,0,0), Max_hight + new Vector3(10, 0, 0), Color.red);

        float camera_y = MainCamera.transform.position.y;

      
        if (camera_y >= Start_curve_point.y)
        {

          float a =Distance_hight/Max_amount;
            Amount = a * Campos / Max_amount;
        
        }
        else
        {
            amount = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Start_curve_point + new Vector3(10, 0, 0), 0.5f);

      
    }
}
