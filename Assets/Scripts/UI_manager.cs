using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_manager : MonoBehaviour
{
    public static UI_manager instace { set; get; }
    public GameObject ingame_ui;
    public GameObject EndScreen_ui;
    public GameObject Un_finished_ui;
    public Animation BuildingAnimation;
    public Animation BuildingAnimation_unFinshed;

    public TextMeshProUGUI finshed_text, exp_text, earned_text;
    private void Awake()
    {
        instace = this;
    }
}
