using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Contract : MonoBehaviour
{

    public TrailSet_info Trail_set;
    [Space(30)]  
    public Sprite Avatar;
    public string Avatar_name;
    [Space(30)]

    //"date" means To finish within days
    public int _life;
    public float Finsh_price;
    public int Totalbuildigs { set; get; }
    public List<TrailSet_info> trailBoxSets;
 
    public bool Is_accepted;
    public Image Avater_image;
  //  public GameObject ParentOfDates;
    public DateTime EndDate;
    public DateTime Startdate;
    public LineBox_ui c_LineBox_ui;
    public Animator Open_animator;
    public TextMeshProUGUI parties_content_text, StartDate_text, endDate_text, TotalMoeny,total_buildings_text,each_building_price_text,later_bu_text;
    public GameObject Accept_Bu;
    private void Awake()
    {
      //  Totalbuildigs = trailBoxSets.Count;

        //Setup texts
     //   finish_price_text.text = ""+Finish_price;
    //    totalBuildings_text.text = "" + Totalbuildigs;
     //  life_text.text = "" + _life;
        

    }
    private void Start()
    {
        Avatar_name = Avatar.name;
        Set_texts();

      
    }
   
    public void Accept()
    {
        //check already have one contract accepted

        if (!NewContracts.instace.Allow_newContract)
        {
            Debug.LogWarning("Errror");
            return;
        }
        
    
        Is_accepted = true;

       // ParentOfDates.SetActive(true);

        //after finsh the contract will tick the line box
       // c_LineBox_ui.GetComponent<LineBox_ui>().Tick.isOn = true;
       
      //  c_LineBox_ui.GetComponent<LineBox_ui>().selected_img.gameObject.SetActive(true);


      //  Debug.Log("leftDays=" + dif.TotalDays);
        Debug.Log("End=" + EndDate.ToString("dd:MM:yyyy"));
        Lobby_play_button.instace.start_trailSet = Trail_set;


        Lobby_play_button.instace.New_trailSetadd(trailBoxSets);

        ES3.Save<TrailSet_info>("accepted_trailset", c_LineBox_ui.GetComponent<LineBox_ui>().P_contract.Trail_set);
        ES3.Save<Contract>("accepted_contract", c_LineBox_ui.P_contract);

        //to allow accepet new contract
        NewContracts.instace.Allow_newContract = false;
        ES3.Save<bool>("Allow_new_contract", NewContracts.instace.Allow_newContract);


        c_LineBox_ui.GetComponent<LineBox_ui>().Is_accepted = true;
        
        ES3.Save<int>("Accepted_ListNumber", c_LineBox_ui.listNumber);

        c_LineBox_ui.GetComponent<LineBox_ui>().BackGorund_img.sprite = c_LineBox_ui.GetComponent<LineBox_ui>().slected_contract_img;

        Accept_Bu.SetActive(false);
        later_bu_text.text = "Back";

        Lobby_play_button.instace.each_trail_price = Finsh_price / trailBoxSets.Count;
        ES3.Save<Lobby_play_button>("trailsets", Lobby_play_button.instace);
        //  for(int i = 0; i < Recived_contract.instance.listOfLineBoxes.Count; i++)
        //  {
        //      if (Recived_contract.instance.listOfLineBoxes[i] == c_LineBox_ui)
        //      {
        //          ES3.Save<int>("Accepted_lineBox_int",i);
        //
        //          Debug.Log("Saved=" + i);
        //      }
        //  }


    }
    public void Later()
    {
        if(later_bu_text.text != "Back")
        {
            Is_accepted = false;
        }

        Destroy(gameObject, 0.25f);
        c_LineBox_ui.Close_contractpaper();
    }
    public void addMoreDays()
    {
       
      /// EndDate = EndDate.AddDays(7);
      /// 
      /// TimeSpan dif = CurrentDate.Subtract(EndDate);
      /// Debug.Log("leftDays=" + dif.TotalDays);
      /// Debug.Log("End="+EndDate.ToString("dd:MM:yyyy"));
    }
    void Set_texts()
    {
        Avater_image.sprite = Avatar;

        Startdate = DateTime.Now;

        string Player_name="Mansoor";
        string owner_name=Avatar_name;
        parties_content_text.text = "This Construction Contract is made on  " +  Startdate.ToLocalTime().ToString("dd-MM-yyyy")  + "  and indicates the terms of the agreement between  " + Player_name + " and " + owner_name;

        


        EndDate = Startdate;
        EndDate = EndDate.AddDays(_life);
        TimeSpan dif = Startdate.Subtract(EndDate);
        StartDate_text.text = Startdate.ToLocalTime().ToString("dd-MM-yyyy");
        endDate_text.text = EndDate.ToLocalTime().ToString("dd-MM-yyyy");
        total_buildings_text.text = trailBoxSets.Count.ToString();
        each_building_price_text.text = (Finsh_price / trailBoxSets.Count).ToString();
        TotalMoeny.text = Finsh_price.ToString()+"$";

    }
}
