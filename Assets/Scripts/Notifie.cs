using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class Notifie : MonoBehaviour
{
   public static Notifie instace { set; get; }
    public Image Background;
    public TextMeshProUGUI Field;
    public Button ok_button;
    public GameObject Loading_pre;
    public Image MAin_avathar;
    private void Awake()
    {
        instace = this;
    }
    public void OpenSimpleNotifie(string Contant,string ColorSting)
    {
        if (ES3.KeyExists("Main_avathar"))
        {
            MAin_avathar.sprite = ES3.Load<Sprite>("Main_avathar");
        }
        
        ChangeBackGoroundColor(ColorSting);
        Loading_pre.SetActive(false);
        Field.gameObject.SetActive(true);
        ok_button.gameObject.SetActive(true);

        Field.text = Contant;
        ok_button.onClick.AddListener(CloseNotifie);
        OpenNotifie();
    }
   
    public void OpenSimpleLoading(GameObject ToDisable,string ColorString)
    {
        ChangeBackGoroundColor(ColorString);
        Loading_pre.SetActive(true);
        Field.gameObject.SetActive(false);
        ok_button.gameObject.SetActive(false);
        
        StartCoroutine(cloaseLoading(ToDisable));
        gameObject.transform.DOLocalMoveY(-1900, 0.5f).SetEase(Ease.OutBounce);

    }
    IEnumerator cloaseLoading(GameObject ToDiable)
    {
        yield return new WaitForSeconds(10);
        ToDiable.SetActive(false);
        gameObject.transform.DOLocalMoveY(-4000, 0.3f).SetEase(Ease.InCirc);
    }
   
    void ChangeBackGoroundColor(string ColorString)
    {
        Color color1;
        string ColorStr = "#" + ColorString;
        ColorUtility.TryParseHtmlString(ColorStr, out color1);
        Background.color = color1;
    }
    void OpenNotifie()
    {
        gameObject.transform.DOLocalMoveY(-1900, 0.5f).SetEase(Ease.OutBounce);
    }
    void CloseNotifie()
    {
        gameObject.transform.DOLocalMoveY(-4000, 0.3f).SetEase(Ease.InCirc);
    }


}
