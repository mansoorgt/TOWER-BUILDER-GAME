using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puase_menu : MonoBehaviour
{
    public static Puase_menu instace { set; get; }

    public GameObject Pause_ui;
    public GameObject Main_gameUi;
    public Slider Sfx_slider, music_slider;
    public Image Sfx_mute_bu,Music_mute_bu;
    public Sprite sfx_normal, sfx_mute,music_normal,music_mute;

    public bool Sfx_Mute_bool,Music_Mute_bool;

    
    private void Start()
    {


        instace = this;

        if (ES3.KeyExists("Sfx_volume"))
        {
            float value = ES3.Load<float>("Sfx_volume");
            Sfx_slider.value = value;
            Sfx_volume = value;
            Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", value);
        }
        if (ES3.KeyExists("music_volume"))
        {
            float value = ES3.Load<float>("music_volume");
            music_slider.value = value;
            music_volume = value;
            Audio_manager.instance.music.audioMixer.SetFloat("Music_volume", value);
        }

        if (ES3.KeyExists("Sfx_mute"))
        {
            Sfx_Mute_bool = ES3.Load<bool>("Sfx_mute");
            
            if (Sfx_Mute_bool)
            {

                Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);
                Sfx_mute_bu.sprite = sfx_mute;
            }
            else
            {

                Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", Sfx_volume);
                Sfx_mute_bu.sprite = sfx_normal;
            }
        }

        if (ES3.KeyExists("music_mute"))
        {
            Music_Mute_bool = ES3.Load<bool>("music_mute");
            if (Music_Mute_bool)
            {

                Audio_manager.instance.music.audioMixer.SetFloat("Music_volume", -80);
                Music_mute_bu.sprite = music_mute;
            }
            else
            {

                Audio_manager.instance.music.audioMixer.SetFloat("Music_volume", music_volume);
                Music_mute_bu.sprite = music_normal;
            }
        }


    }
    public static float Sfx_volume { set; get; }
    public static float music_volume { set; get; }
    public void Pause()
    {
        Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);
        Main_gameUi.SetActive(false);
        Pause_ui.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        if (sfx_mute)
        {
            Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", Sfx_volume);
        }
       
     
        Main_gameUi.SetActive(true);
        Pause_ui.SetActive(false);
        Time.timeScale = 1;

        Save_thigs();
    }
    public void sfx_slider(float Value)
    {
        Sfx_volume = Value;
        Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", Sfx_volume);
       

    }
    public void Music_slider(float Value)
    {
        music_volume = Value;
        Audio_manager.instance.music.audioMixer.SetFloat("Music_volume", music_volume);

    }
 
    public void Sfx_mute()
    {
       

        if (!Sfx_Mute_bool)
        {
            Sfx_Mute_bool = true;
            Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", -80);
            Sfx_mute_bu.sprite = sfx_mute;
        }
        else
        {
            Sfx_Mute_bool = false;
            Audio_manager.instance.Sfx.audioMixer.SetFloat("Sfx_volume", Sfx_volume);
            Sfx_mute_bu.sprite = sfx_normal;
        }
        
       
    }

    public void Music_mute()
    {


        if (!Music_Mute_bool)
        {
            Music_Mute_bool = true;
            Audio_manager.instance.music.audioMixer.SetFloat("Music_volume", -80);
            Music_mute_bu.sprite = music_mute;
        }
        else
        {
            Music_Mute_bool = false;
            Audio_manager.instance.music.audioMixer.SetFloat("Music_volume", music_volume);
            Music_mute_bu.sprite = music_normal;
        }


    }

    public void Save_thigs()
    {
        //need to save ON exit "to do";
        ES3.Save<float>("Sfx_volume", Sfx_volume);
        ES3.Save<float>("music_volume", music_volume);

        ES3.Save<bool>("Sfx_mute", Sfx_Mute_bool);
        ES3.Save<bool>("music_mute" ,Music_Mute_bool);

    }
    
    public void OnDisable()
    {
        Save_thigs();
    }
    public void exit()
    {
        Time.timeScale = 1;
        GameManger.instace.Go_lobby();
    }
}
