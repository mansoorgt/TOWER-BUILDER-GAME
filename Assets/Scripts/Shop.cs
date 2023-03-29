using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;
public class Shop : MonoBehaviour,IDragHandler
{
    public static Shop instance { set; get; }
    public List<RectTransform> Items;
    public RectTransform Content;

    public RectTransform Full_view_prt;

    public GameObject opend_preview;
    public Shop_item Pre_Opend_item;

    float x,y;
   
    public Transform Single_box_pr, Three_box_pr, threeTop_box_pr, single_top_pr;
            
    public GameObject Item_button_pre;
    public Button Buy_button;
    public TextMeshProUGUI Doller_text,Diamond_text,Exp_text;


    public RectTransform Buy_confirmation_obj;
    public Image Coin_image_1,coin_image_2;
    public TextMeshProUGUI Amout_text,Information_text;
    public Button Pay_Button;
    public Sprite Doller_sprite, Diamond_sprite, Exp_sprite;


   
    private void Awake()
    {
        instance = this;

        for(int i=0;i<Content.childCount;i++)
        {
            Items.Add(Content.GetChild(i).GetComponent<RectTransform>());
        }


        Update_Amoun_texts();
    }
    public void Update_Amoun_texts()
    {
        if (ES3.KeyExists("EXP"))
        {
            Doller_text.text = "/" + ES3.Load<float>("Doller");
            Exp_text.text = "/" + ES3.Load<float>("EXP");
            Debug.Log("Updated");
            //diamond
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit Hit;
        Physics.Raycast(ray, out Hit);
        if (Hit.transform != null)
        {
            x += 2 * Input.GetAxis("Mouse X");
            y -= 2 * Input.GetAxis("Mouse Y");
            
            x = Mathf.Clamp(x, -30, 30);
            y = Mathf.Clamp(y, -30, 30);
            opend_preview.transform.eulerAngles = new Vector3(y, x, 0);
        }
        
    }

    //aninmation not finshed Need to FInshe it; turn of content size componat and aniamte the size of parent items
    bool Is_open_single_Box;
    GameObject singleBox_Item_list_pr;
    public void Open_SingleBox_Items_trai(GameObject Items_List_pr)
    {
      
        singleBox_Item_list_pr = Items_List_pr;
        if (!Is_open_single_Box)
        {
            Items_List_pr.SetActive(false);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount,0);
            Items_List_pr.SetActive(true);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(0, 1);
            Is_open_single_Box = true;

            StartCoroutine(Invoke_active(Items_List_pr, true));
        }
        else
        {
            

            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(Items_List_pr,false));
            Is_open_single_Box = false;

        }
       

    }

    bool Is_open_Top_single_Box;
    GameObject singleTopBox_Item_list_pr;
    public void Open_Top_SingleBox_Items_trai(GameObject Items_List_pr)
    {
        singleTopBox_Item_list_pr = Items_List_pr;
        if (!Is_open_Top_single_Box)
        {
            Items_List_pr.SetActive(false);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount, 0);
            Items_List_pr.SetActive(true);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(0, 1);
            Is_open_Top_single_Box = true;

            StartCoroutine(Invoke_active(Items_List_pr, true));
        }
        else
        {
            

            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(Items_List_pr, false));
            Is_open_Top_single_Box = false;

        }


    }
    bool Is_open_Top_three_Box;
    GameObject TopthreeBox_Item_list_pr;
    public void Open_Top_Three_Items_trai(GameObject Items_List_pr)
    {
        TopthreeBox_Item_list_pr = Items_List_pr;
        if (!Is_open_Top_three_Box)
        {
            Items_List_pr.SetActive(false);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount, 0);
            Items_List_pr.SetActive(true);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(0, 1);
            Is_open_Top_three_Box = true;

            StartCoroutine(Invoke_active(Items_List_pr, true));
        }
        else
        {


            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(Items_List_pr, false));
            Is_open_Top_three_Box = false;

        }


    }

    bool Is_open_three_Box;
    GameObject threeBox_Item_list_pr;
    public void Open_Three_Items_trai(GameObject Items_List_pr)
    {
        threeBox_Item_list_pr = Items_List_pr;
        if (!Is_open_three_Box)
        {
            Items_List_pr.SetActive(false);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount, 0);
            Items_List_pr.SetActive(true);
            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(0, 1);
            Is_open_three_Box = true;

            StartCoroutine(Invoke_active(Items_List_pr, true));
        }
        else
        {
            

            Items_List_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * Items_List_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(Items_List_pr, false));
            Is_open_three_Box = false;

        }


    }

    public void Close_all_trai_items()
    {


         Is_open_single_Box = false;
         Is_open_three_Box=false;
         Is_open_Top_single_Box=false;
         Is_open_Top_three_Box = false;

        if (singleBox_Item_list_pr != null)
        {
            singleBox_Item_list_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * singleBox_Item_list_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(singleBox_Item_list_pr,false));
        }
        if (singleTopBox_Item_list_pr != null)
        {
            singleTopBox_Item_list_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * singleTopBox_Item_list_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(singleTopBox_Item_list_pr, false));
        }
        if (threeBox_Item_list_pr != null)
        {
            threeBox_Item_list_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * threeBox_Item_list_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(threeBox_Item_list_pr, false));
        }
        if (TopthreeBox_Item_list_pr != null)
        {
            TopthreeBox_Item_list_pr.GetComponent<RectTransform>().DOLocalMoveY(-100 * TopthreeBox_Item_list_pr.transform.childCount, 1);
            StartCoroutine(Invoke_active(TopthreeBox_Item_list_pr, false));
        }

    }
    IEnumerator Invoke_active(GameObject Go,bool Active)
    {
        yield return new WaitForSeconds(1);
        Go.SetActive(Active);
       // Go.GetComponent<ContentSizeFitter>().enabled = true;
    }
    float Doller;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Cheated");
           
            if (ES3.KeyExists("Doller"))
            {
              Doller  = ES3.Load<float>("Doller");
             
            }
            Doller += 500;
            ES3.Save<float>("Doller", Doller);

            ES3.Save<float>("DIAMOND", 500);
            ES3.Save<float>("EXP", 560);
        }
    }
}
