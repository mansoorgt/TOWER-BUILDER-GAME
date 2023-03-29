using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{

    public enum AudioTypes { sfx,Music}
    public AudioTypes audioTypes;

    public string Name;
    public List<AudioClip> audioclip;
   
    [HideInInspector]
    public AudioSource Source;
 
    [Range(0,1)]
    public float Volume;

    public bool Loopig;


}
