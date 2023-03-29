using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class Lobby_ui : MonoBehaviour
{
    public static Lobby_ui instance { set; get; }
    public Animator First_toch_anm;
    bool Entered;

    public GameObject Shoping_screen,main_screen;
    public RectTransform Shopping_items_bg_parent;
    public TextMeshProUGUI Doller_text,exp_text,diamond_text;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if (ES3.KeyExists("EXP"))
        {
            Doller_text.text = "/" + ES3.Load<float>("Doller");
            
          
        }
        if (ES3.KeyExists("EXP"))
        {
            exp_text.text = "/" + ES3.Load<float>("EXP");
        }
        if (ES3.KeyExists("DIAMOND"))
        {
            diamond_text.text = "/" + ES3.Load<float>("DIAMOND");
        }
     
    }

    // Update is called once per frame
    void Update()
    {
    if (Input.GetKeyDown(KeyCode.Mouse0) && !Entered)
    {
        Entered = true;
          //  First_toch_anm.SetTrigger("Enter");
        
   
    }
      
    }
    public void Open_SmallBox(RectTransform Obj)
    {

        Obj.gameObject.SetActive(true);
        Obj.DOLocalMoveY(0, 0.1f).SetEase(Ease.InFlash);
    }
    public void Close_SmallBox(RectTransform Obj)
    {
        Obj.DOLocalMoveY(-100, 0.1f).SetEase(Ease.OutFlash);
        StartCoroutine(Close(Obj));
    }
    IEnumerator Close(RectTransform Obj)
    {
        yield return new WaitForSeconds(0.1f);

        Obj.gameObject.SetActive(false);
    }
    public void Open_shop()
    {
        main_screen.SetActive(false);
        Shoping_screen.SetActive(true);
        Shopping_items_bg_parent.DOLocalMoveY(-24, 0.2f).SetEase(Ease.Linear);
    }
    public void Close_Shop()
    {
        Invoke(nameof(Close_shop_invoke), 0.2f);
        Shopping_items_bg_parent.DOLocalMoveY(-250, 0.2f).SetEase(Ease.InQuint);
    }
    void Close_shop_invoke()
    {
        main_screen.SetActive(true);
        Shoping_screen.SetActive(false);
    }
}
