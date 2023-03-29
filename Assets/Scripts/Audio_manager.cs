using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
public class Audio_manager : MonoBehaviour
{

    public AudioMixerGroup Sfx, music;
    public Sound[] Sounds;
    public static Audio_manager instance;
    // Start is called before the first frame update


    // if anyy eeror change "OnEnable" to "Awake'
    void OnEnable()
    {
  
        instance = this;
       foreach(Sound S in Sounds)
        {
            S.Source = gameObject.AddComponent<AudioSource>();
            S.Source.playOnAwake = false;
            S.Source.clip = S.audioclip[0];
            S.Source.volume = S.Volume;
            S.Source.loop = S.Loopig;

            switch (S.audioTypes)
            {
                case Sound.AudioTypes.sfx:
                    S.Source.outputAudioMixerGroup = Sfx;
                    break;
                case Sound.AudioTypes.Music:
                    S.Source.outputAudioMixerGroup = music;
                    break;

            }
        }
    }
    public void Play(string Name,bool moreThanOne_clip)
    {
        if (!moreThanOne_clip)
        {
            Sound S = Array.Find(Sounds, Sound => Sound.Name == Name);
            S.Source.Play();
        }
        else
        {
           
            Sound s= Array.Find(Sounds, Sound => Sound.Name == Name);
            List<AudioClip> clips = s.audioclip;

            AudioClip SelcetedClip = clips[UnityEngine.Random.Range(0, clips.Count)];
            s.Source.clip = SelcetedClip;
            s.Source.Play();
        }

    }
    public void Play(string Name, int specific_one,bool looping)
    {
       
        Sound S = Array.Find(Sounds, Sound => Sound.Name == Name);
        S.Source.clip = S.audioclip[specific_one];
       
        S.Source.loop = looping;
        
        S.Source.Play();
        
        

    }

    public bool chek_is_playing(string name)
    {
        Sound S = Array.Find(Sounds, Sound => Sound.Name == name);
        
        return S.Source.isPlaying;
        
    }
    public AudioSource GetAudioSource(string name)
    {
        Sound S = Array.Find(Sounds, Sound => Sound.Name == name);
        return S.Source;
    }


}
