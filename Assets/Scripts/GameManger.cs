using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.VFX;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameManger : MonoBehaviour
{
    public static GameManger instace;

    public TextMeshProUGUI Life_text;
    public TextMeshProUGUI Time_start_text;
    public TextMeshProUGUI Finished_trails_count,max_trailCount;
    float Time_Start;
    public float Life_MaxValue;
    
    public TrailManeger m_TrailManager;
    public int Max_trailboxes;
    public VisualEffect FireWork_vfx;
    public ParticleSystem Prt_color;

    public Levels Levels_tr;
    //count of reapeat of the same trail;
    public int Count_of_repeat;

    public bool Finised;
    public Lobby_play_button pla;
    void Start()
    {
        
        instace = this;
        Prt_color.gameObject.SetActive(false);
        Life_text.text = Life_MaxValue.ToString();

        Levels_tr.levles[Levels_tr.Next_count].gameObject.SetActive(true);
        m_TrailManager = Levels_tr.levles[Levels_tr.Next_count].GetComponent<TrailManeger>();
        Count_of_repeat = ES3.Load<int>("Count_of_repeat", Count_of_repeat);


        float Exp = UnityEngine.Random.Range(100, 500);
        if (Count_of_repeat > 0)
        {
 
            Exp=Exp* Count_of_repeat*2;
        }
        UI_manager.instace.exp_text.text = Exp.ToString();

        Finished_trails_count.text = "0";
        max_trailCount.text = "/"+m_TrailManager.Max_trailCount.ToString();

        pla = Lobby_play_button.instace;
    }
    private void Update()
    {
        Time_Start += Time.deltaTime;
        int seconds = (int)(Time_Start % 60);
        int minutes = (int)(Time_Start / 60) % 60;
        Time_start_text.text = minutes+":"+seconds;

      

    }
    public void LifeTextUpdate(float MinezValue)
    {
        Life_MaxValue -= MinezValue;
        Life_text.text = Life_MaxValue.ToString();

        if (Life_MaxValue < 1)
        {
            Un_finished();
        }
    }
    public float exp_t;
    float Doller_Durrent;
    public void Finished()
    {
        Finised = true;
        StartCoroutine(Finshed_c());
        Prt_color.gameObject.SetActive(true);
        Prt_color.Play();
        FireWork_vfx.Play();

        Audio_manager.instance.Play("ending_Spark", 0, false);
        Audio_manager.instance.Play("ending_FireWork", 0, true);
    

        Levels_tr.GetComponent<Levels>().One_level_finished();
        //if using for when testing levelscene playbutton not there(becouse Lobbyplaybutton.sc is coming from the looby scene)
        if (Lobby_play_button.instace != null)
        {
            Lobby_play_button.instace.get_next_trailSet();
        }

        UI_manager.instace.finshed_text.text = Time_start_text.text;

        float Exp = UnityEngine.Random.Range(100, 500);
        if (Count_of_repeat > 0)
        {

            Exp = Exp * Count_of_repeat * 2;
        }

        UI_manager.instace.exp_text.text = Exp.ToString();
       
        if (ES3.KeyExists("EXP"))
        {
            exp_t = ES3.Load<float>("EXP");
            

        }
        exp_t += Exp;
        ES3.Save<float>("EXP", exp_t);



        if (Count_of_repeat == 0)
        {
            UI_manager.instace.earned_text.text = Lobby_play_button.instace.each_trail_price.ToString();

            if (ES3.KeyExists("Doller"))
            {
                 Doller_Durrent = ES3.Load<float>("Doller");
            }
           
            Doller_Durrent += Lobby_play_button.instace.each_trail_price;
            ES3.Save("Doller", Doller_Durrent);
        }
        else
        {
            UI_manager.instace.earned_text.text = 0.ToString();
        }
      

    }
    IEnumerator Finshed_c()
    {
        yield return new WaitForSeconds(5);

        UI_manager.instace.ingame_ui.SetActive(false);
        UI_manager.instace.EndScreen_ui.SetActive(true);

        UI_manager.instace.BuildingAnimation.Play();
        Audio_manager.instance.Play("Finished_musics", true);
    }
    public void Un_finished()
    {
        if (Finised)
        {
            return;
        }

        StartCoroutine(Un_Finshed_c());
        Audio_manager.instance.Play("Un_finished_efx", 0,false);
        
        Time.timeScale = Mathf.Lerp(1, 0.2f, 0.1f);

    }
    IEnumerator Un_Finshed_c()
    {
        yield return new WaitForSeconds(5);

        UI_manager.instace.ingame_ui.SetActive(false);
        UI_manager.instace.Un_finished_ui.SetActive(true);

        UI_manager.instace.BuildingAnimation_unFinshed.Play();
    }
    public void Repeat()
    {
        Finised = false;
        Levels_tr.GetComponent<Levels>().repeat();
        Count_of_repeat++;
        
        ES3.Save<int>("Count_of_repeat", Count_of_repeat);
        ES3.Save<bool>("Is_repeat", true);

        //muting in gamew sfx sound
        Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);


        Loading_manager.instace.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    public void Next_level()
    {

        Finised = false;
        ES3.Save<int>("Count_of_repeat", 0);
        ES3.Save<bool>("Is_repeat", false);

        //muting in gamew sfx sound
        Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);

        if (Lobby_play_button.instace != null)
        {

            Lobby_play_button.instace.OnClik_on_playButton();
        }


    }
    public void Go_lobby()
    {
        //muting in gamew sfx sound
        Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);

        Destroy(Lobby_play_button.instace.gameObject);
        Loading_manager.instace.LoadLevel((int)MyScenes.lobby);
    }
    public void One_More_trail_add()
    {
        Finished_trails_count.gameObject.SetActive(false);
        Finished_trails_count.text = m_TrailManager.Finished_count.ToString();
        Invoke(nameof(Invoke_trail_add), 1f);
    }
    public void One_More_trail_Less()
    {

        Finished_trails_count.text = m_TrailManager.Finished_count.ToString();
        
    }
    public void Invoke_trail_add()
    {
        Finished_trails_count.rectTransform.DOScale(10, 0);
        Finished_trails_count.gameObject.SetActive(true);
        Finished_trails_count.rectTransform.DOScale(2.6f, 0.2f).SetEase(Ease.InBounce);

    }
 
}
