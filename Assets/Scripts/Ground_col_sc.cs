using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_col_sc : MonoBehaviour
{


    public List<GameObject> Pre;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Object_sc>() != null)
        {
            if (!Pre.Contains(collision.gameObject))
            {
                Pre.Add(collision.gameObject);
                collision.gameObject.GetComponent<Object_sc>().Is_falsed = true;
                GameManger.instace.LifeTextUpdate(1);

            }
           
           
        }
      
    }
}
