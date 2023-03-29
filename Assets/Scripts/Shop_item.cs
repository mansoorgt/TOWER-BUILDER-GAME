using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;
public class Shop_item : MonoBehaviour
{
    public string Name_of_container;
    public enum Coin {Doller,Diamond,Exp };
    public Coin Coin_state;
    public float Amount;

    public TextMeshProUGUI front_amount_text;
    public Image Front_coin_Img;

    public GameObject Full_preview;
    public GameObject Ins_preview;
    public Objects_Container Container;
    

    public bool Item_is_opened,new_opend;
    public int Opend_int;
    private void Start()
    {

        

        front_amount_text.text = Amount.ToString();
        switch (Coin_state)
        {
            case Coin.Doller:
                Front_coin_Img.sprite = Shop.instance.Doller_sprite;

                break;
                
            case Coin.Exp:
                Front_coin_Img.sprite = Shop.instance.Exp_sprite;
                break;
            case Coin.Diamond:
                Front_coin_Img.sprite = Shop.instance.Diamond_sprite;
                break;
        }

        for (int i = 0; i < Lobby_play_button.instace.Inventory.Count; i++)
        {
            if (Container == Lobby_play_button.instace.Inventory[i])
            {
                Debug.Log("this on buyed");

                front_amount_text.text = "Buyed";
                front_amount_text.rectTransform.localPosition = new Vector3(0, front_amount_text.rectTransform.localPosition.y, front_amount_text.rectTransform.localPosition.z);
                Front_coin_Img.gameObject.SetActive(false);
            }

        }
    }
    public void Opened()
    {
        for (int i = 0; i < Shop.instance.Content.childCount; i++)
        {
            Shop.instance.Content.GetChild(i).GetComponent<Button>().interactable = true;
        }

        Destroy_all_preview_items();

        if (Shop.instance.opend_preview)
        {
            Destroy(Shop.instance.opend_preview);
           // Destroy_all_preview_items();
            
        }

        Ins_preview = Instantiate(Full_preview, Shop.instance.Full_view_prt);
        Add_items_tab();
        Shop.instance.opend_preview = Ins_preview;
        gameObject.GetComponent<Button>().interactable = false;

        Shop.instance.Buy_button.onClick.RemoveAllListeners();

        Shop.instance.Buy_button.gameObject.SetActive(true);
        Check_is_Buyed();

        Shop.instance.Buy_button.onClick.AddListener(Buy);
    }
    void Check_is_Buyed()
    {
       for(int i = 0; i < Lobby_play_button.instace.Inventory.Count;i++)
       {
            if (Container == Lobby_play_button.instace.Inventory[i])
            {
                Debug.Log("this on buyed");
                Shop.instance.Buy_button.gameObject.SetActive(false);

            }  
            
       }
    }
    void Destroy_all_preview_items()
    {
      
        Shop.instance.Close_all_trai_items();

        for (int i = 0; i < Shop.instance.Single_box_pr.childCount; i++)
        {
            Destroy(Shop.instance.Single_box_pr.GetChild(i).gameObject);
     
        }
        for (int i = 0; i < Shop.instance.single_top_pr.childCount; i++)
        {
            Destroy(Shop.instance.single_top_pr.GetChild(i).gameObject);

        }
        for (int i = 0; i < Shop.instance.Three_box_pr.childCount; i++)
        {
            Destroy(Shop.instance.Three_box_pr.GetChild(i).gameObject);

        }
        for (int i = 0; i < Shop.instance.threeTop_box_pr.childCount; i++)
        {
            Destroy(Shop.instance.threeTop_box_pr.GetChild(i).gameObject);

        }

    }
    void Add_items_tab()
    {
       
        //single box
        for(int i = 0; i < Container.SingleBox.Count; i++)
        {
            GameObject Ins_item = Instantiate(Shop.instance.Item_button_pre, Vector3.zero, new Quaternion(0, 0, 180, 0), Shop.instance.Single_box_pr);
            Ins_item.GetComponent<Image>().sprite = Container.SignleBox_sprite[i];
           
            Ins_item.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);

           

        }

        //single top box
        for (int i = 0; i < Container.TopSingleBox.Count; i++)
        {
            GameObject Ins_item = Instantiate(Shop.instance.Item_button_pre, Vector3.zero, new Quaternion(0, 0, 180, 0), Shop.instance.single_top_pr);
            Ins_item.GetComponent<Image>().sprite = Container.TopSingleBox_sprite[i];
            
            Ins_item.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }

        //three box
        for (int i = 0; i < Container.threeBox.Count; i++)
        {
            GameObject Ins_item = Instantiate(Shop.instance.Item_button_pre, Vector3.zero, new Quaternion(0,0,180,0), Shop.instance.Three_box_pr);
            Ins_item.GetComponent<Image>().sprite = Container.threeBox_sprite[i];
      
            Ins_item.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }

        //three Top box
        for (int i = 0; i < Container.TopthreeBox.Count; i++)
        {
            GameObject Ins_item = Instantiate(Shop.instance.Item_button_pre,Vector3.zero, new Quaternion(0, 0, 180, 0), Shop.instance.threeTop_box_pr);
            Ins_item.GetComponent<Image>().sprite = Container.TopthreeBox_sprite[i];
      
            Ins_item.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        }
        
    }

    float current_Doller_amount;
    float Current_Exp_amount;
    float current_Diamond_amount;
    public void Buy()
    {
        
        switch (Coin_state)
        {
            case Coin.Doller:
                Shop.instance.Coin_image_1.sprite = Shop.instance.Doller_sprite;
                Shop.instance.coin_image_2.sprite = Shop.instance.Doller_sprite;

                
                if (ES3.KeyExists("Doller"))
                {
                    current_Doller_amount = ES3.Load<float>("Doller");
                }
                else
                {
                    Notifie.instace.OpenSimpleNotifie("You Don't have enough Doller to Buy !", "828282");
                }
                
                

                if (current_Doller_amount >= Amount)
                {
                    Shop.instance.Information_text.text = "if you ready to pay then pay !";
                    Shop.instance.Pay_Button.gameObject.SetActive(true);
                    Lobby_ui.instance.Open_SmallBox(Shop.instance.Buy_confirmation_obj);
                }
                else
                {
                    Notifie.instace.OpenSimpleNotifie("You Don't have enough Doller to Buy !", "828282");
                    
                }
               

                break;
            case Coin.Diamond:
                Shop.instance.Coin_image_1.sprite = Shop.instance.Diamond_sprite;
                Shop.instance.coin_image_2.sprite = Shop.instance.Diamond_sprite;

                if (ES3.KeyExists("DIAMOND"))
                {
                    current_Diamond_amount = ES3.Load<float>("DIAMOND");
                }
                else
                {
                    Notifie.instace.OpenSimpleNotifie("You Don't have enough Doller to Buy !", "828282");
                }

                
                if (current_Diamond_amount >= Amount)
                {
                    Shop.instance.Information_text.text = "if you ready to pay then pay !";
                    Shop.instance.Pay_Button.gameObject.SetActive(true);
                    Lobby_ui.instance.Open_SmallBox(Shop.instance.Buy_confirmation_obj);
                }
                else
                {
                    Shop.instance.Information_text.text = "You Don't have enough diamond to Buy !";
                    
                }

                break;
            case Coin.Exp:
                Shop.instance.Coin_image_1.sprite = Shop.instance.Exp_sprite;
                Shop.instance.coin_image_2.sprite = Shop.instance.Exp_sprite;

                Current_Exp_amount = ES3.Load<float>("EXP");
                if (Current_Exp_amount >= Amount)
                {
                    Shop.instance.Information_text.text = "if you ready to pay then pay !";
                    Shop.instance.Pay_Button.gameObject.SetActive(true);
                    Lobby_ui.instance.Open_SmallBox(Shop.instance.Buy_confirmation_obj);
                }
                else
                {
                    Notifie.instace.OpenSimpleNotifie("You Don't have enough EXP to Buy !", "828282");
                    
                }


                break;

        }

        Shop.instance.Amout_text.text = Amount.ToString();
        Shop.instance.Pay_Button.onClick.RemoveAllListeners();
        Shop.instance.Pay_Button.onClick.AddListener(m_Pay);

        
    }
    public void m_Pay()
    {
        switch (Coin_state)
        {
            case Coin.Doller:
                float current_Doller_amount = ES3.Load<float>("Doller");
                if (current_Doller_amount >= Amount)
                {
                     current_Doller_amount -= Amount;
                     ES3.Save("Doller", current_Doller_amount);
                  
                   Shop.instance.Update_Amoun_texts();
                  
                   Shop.instance.Pay_Button.gameObject.SetActive(false);
                   Shop.instance.Information_text.text = "Congratulations You Buyed This Container";
                   Lobby_play_button.instace.Inventory.Add(Container);
                   ES3.Save<Lobby_play_button>("trailsets", Lobby_play_button.instace);
                }
                break;
           case Coin.Diamond:
               float current_Diamond_amount = ES3.Load<float>("DIAMOND");
               if (current_Diamond_amount >= Amount)
               {
                   current_Diamond_amount -= Amount;
                   ES3.Save("DIAMOND", current_Diamond_amount);
              
                   Shop.instance.Update_Amoun_texts();
              
                   Shop.instance.Pay_Button.gameObject.SetActive(false);
                   Shop.instance.Information_text.text = "Congratulations You Buyed This Container";
                   Lobby_play_button.instace.Inventory.Add(Container);
                   ES3.Save<Lobby_play_button>("trailsets", Lobby_play_button.instace);
               }
                break;
        case Coin.Exp:
              float current_Exp_amount = ES3.Load<float>("EXP");
              if (current_Exp_amount >= Amount)
              {
                  current_Exp_amount -= Amount;
                  ES3.Save("EXP", current_Exp_amount);
             
                  Shop.instance.Update_Amoun_texts();
             
                  Shop.instance.Pay_Button.gameObject.SetActive(false);
                  Shop.instance.Information_text.text = "Congratulations You Buyed This Container";
                  Lobby_play_button.instace.Inventory.Add(Container);
                  ES3.Save<Lobby_play_button>("trailsets", Lobby_play_button.instace);
                }
                break;



        }

        front_amount_text.text = "Buyed";
        front_amount_text.rectTransform.localPosition = new Vector3(0, front_amount_text.rectTransform.localPosition.y, front_amount_text.rectTransform.localPosition.z);
        Front_coin_Img.gameObject.SetActive(false);


    }
    
}
