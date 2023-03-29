using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby_play_button : MonoBehaviour
{
    public static Lobby_play_button instace { set; get; } 
    public TrailSet_info start_trailSet;

    public int next_trailCount;
    public List<TrailSet_info> trailset_list;
    public List<bool> Finished_TrailSets;

    public List<Objects_Container> Inventory;
    //extra 
    public float each_trail_price;
    
    private void Awake()
    {
       

        if (instace == null)
        {

            instace = this;
            if (ES3.KeyExists("trailsets"))
            {
                Debug.Log("Loaded");
                start_trailSet = ES3.Load<TrailSet_info>("accepted_trailset");
                instace = ES3.Load<Lobby_play_button>("trailsets");
            }
        }
        else
        {
             Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
       
    }
    public void New_trailSetadd(List<TrailSet_info> New_trailSet)
    {
        //adding the number of count of equl to new_trailset items
        for(int i = 0; i < New_trailSet.Count; i++)
        {
            Finished_TrailSets.Add(false);
        }
        trailset_list.Clear();
        trailset_list = New_trailSet;

        ES3.Save<Lobby_play_button>("trailsets", Lobby_play_button.instace);

        next_trailCount = 0;
        start_trailSet = trailset_list[0];

;
    }
    //when finished the trail set in game add this fuction
    public void get_next_trailSet()
    {
        Finished_TrailSets[next_trailCount] = true;
        next_trailCount++;
        //when finsh current contract trailsets
        if (next_trailCount > trailset_list.Count)
        {
            start_trailSet = null;
            Need_slecte_NewContract();
            Recived_contract.instance.finished_contrct(false);

            ES3.Save<Lobby_play_button>("trailsets",Lobby_play_button.instace);
            return;
        }
       
        start_trailSet = trailset_list[next_trailCount];
        //ES3.Save<Lobby_play_button>("trailsets", Lobby_play_button.instace);
        ES3.Save<TrailSet_info>("accepted_trailset",start_trailSet);
        Debug.Log("Saved");
    }


    public void Need_slecte_NewContract()
    {

    }
    public void OnClik_on_playButton()
    {

        if (ES3.KeyExists("accepted_trailset"))
        {
            start_trailSet = ES3.Load<TrailSet_info>("accepted_trailset");
        }
     

        if (start_trailSet == null)
        {

            Notifie.instace.OpenSimpleNotifie("you need to accept any contract to start the game", "F05C5C");
            Debug.Log("you need to accept any contract to start the game");

        }
        else
        {
            Debug.Log(start_trailSet.Scene_int + "this");
            if (start_trailSet.Scene_int == 1)
            {
                Loading_manager.instace.LoadLevel((int)MyScenes.Riyad);


            }
            if (start_trailSet.Scene_int == 2)
            {
                Loading_manager.instace.LoadLevel((int)MyScenes.uae);


            }
            if (start_trailSet.Scene_int == 3)
            {
                Loading_manager.instace.LoadLevel((int)MyScenes.quator);


            }

        }

    }
}

