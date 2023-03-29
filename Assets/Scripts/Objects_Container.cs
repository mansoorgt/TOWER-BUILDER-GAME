using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Objects_Container : MonoBehaviour
{
    public Sprite container_Sprite;
    [Header("Objects")]
    public List<GameObject> SingleBox;
    public List<GameObject> TopSingleBox;
    public List<GameObject> threeBox;
    public List<GameObject> TopthreeBox;


    [Space(12)]
    [Header("Sprites")]
    public Sprite[] SignleBox_sprite;
    public Sprite[] TopSingleBox_sprite;
    public Sprite[] threeBox_sprite;
    public Sprite[] TopthreeBox_sprite;

}
