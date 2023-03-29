using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recived_contract : MonoBehaviour
{


    //this script use for saving/loading recived contract.es3 cant save directly save/load list for creating another script and save that script throgh this method save/load list


    public static Recived_contract instance;
    public List<Contract> m_Recived_contract;
    public GameObject lineBox_parent;
    public List<GameObject> listOfLineBoxes;
    public List<bool> Finished_boolList;

    public int accepted_listnumber;

    private void Awake()
    {
        instance = this;


      //  Debug.Log("Acceppted int =" + ES3.Load<int>("Accepted_lineBox_int"));

        if (ES3.KeyExists("Recived_list"))
        {
            m_Recived_contract = ES3.Load<Recived_contract>("Recived_list").m_Recived_contract;
        }
     
    }
    private void Start()
    {

        listOfLineBoxes.Clear();
      for (int i = 0; i < lineBox_parent.transform.childCount; i++)
      {
          listOfLineBoxes.Add(lineBox_parent.transform.GetChild(i).gameObject);
      }
        listOfLineBoxes.Reverse();

        if (ES3.KeyExists("recived_contract"))
        {
            Finished_boolList = ES3.Load<Recived_contract>("Recived_list").Finished_boolList;

        }

        for(int i = 0; i < Finished_boolList.Count; i++)
        {
            if (Finished_boolList[i])
            {
                listOfLineBoxes[i].GetComponent<LineBox_ui>().is_finished = true;
                
            }
        }

        if (ES3.KeyExists("Accepted_ListNumber"))
        {

             accepted_listnumber = ES3.Load<int>("Accepted_ListNumber");
          
            listOfLineBoxes[accepted_listnumber].GetComponent<LineBox_ui>().Is_accepted = true;


        }

        finished_contrct(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            finished_contrct(false);
            Debug.Log("Save");
        }
    }
    
                                 //if start 
    public void finished_contrct(bool start)
    {
       
        //
       //after all set of trail bo of  one contract at level secen need to use this fuction and also "start" as false
       //req=accepted list number


        if (!start)
        {
            ES3.Save<Recived_contract>("Recived_list", Recived_contract.instance);
            Finished_boolList[accepted_listnumber] = true;
            for (int i = 0; i < Finished_boolList.Count; i++)
            {
                if (Finished_boolList[i]==true)
                {
                  //  listOfLineBoxes[i].GetComponent<LineBox_ui>().Tick.isOn = true;
                    listOfLineBoxes[i].GetComponent<LineBox_ui>().BackGorund_img.sprite = listOfLineBoxes[i].GetComponent<LineBox_ui>().Done_contract_img;
                    NewContracts.instace.Allow_newContract=true;
                    ES3.Save<bool>("Allow_new_contract", true);
                }
            }
        }
        else
        {
            for (int i = 0; i < Finished_boolList.Count; i++)
            {
                if (Finished_boolList[i]==true)
                {
                  //  listOfLineBoxes[i].GetComponent<LineBox_ui>().Tick.isOn = true;
                    listOfLineBoxes[i].GetComponent<LineBox_ui>().BackGorund_img.sprite = listOfLineBoxes[i].GetComponent<LineBox_ui>().Done_contract_img;
                   

                }
            }
        }
             
    }


}
