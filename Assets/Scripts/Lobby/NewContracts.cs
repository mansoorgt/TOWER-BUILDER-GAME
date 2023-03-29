using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
public class NewContracts : MonoBehaviour
{
    public static NewContracts instace;

    public List<Contract> m_Contracts;
  
    public TextMeshProUGUI New_msg_text;

    public int Finshed_Contracts;

    public RectTransform Contens;
    public RectTransform paprer_parent;
    public GameObject lineBox_ui_prefab; //massagebox line Box prefab

    public RectTransform NewContractScreen;
    public ScrollRect m_Scrollrect;

    public List<Contract> Load_list;
    //for when add new contract to save other wise not save
    public bool Adding_new_contract;

    public  int Recived_list_count = 0;

    public bool Allow_newContract;

    public int add_newContract; 
    public void Start()
    {
        //  DontDestroyOnLoad(gameObject);
        instace = this;
        //if (instace == null)
        //{
        //    
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}



        Allow_newContract = true;

        if (ES3.KeyExists("Allow_new_contract"))
            Allow_newContract = ES3.Load<bool>("Allow_new_contract");

        if (ES3.KeyExists("Recived_list_count"))
        {
            Recived_list_count = ES3.Load<int>("Recived_list_count");
        }

       
       

        GetnewCotntracts(Recived_list_count);
        New_msg_text.text = Recived_list_count.ToString();
        Adding_new_contract = false;
     



        //Whem Game Newly Started Giving 5 Contracts

    }

    public void add_buton_test()
    {
        Adding_new_contract = true;
        GetnewCotntracts(1);
        

        // Add_new(1, 3);
    }
                                              //which contract to new to add , when adding more than one contract give start point off new contract 
    public void Add_new(int How_much_contract,int New_contract_start)
    {


        Adding_new_contract = true;
     //   GetnewCotntracts(add_newContract);
     
        for(int i = 0; i < How_much_contract; i++)
        {
            if (!ES3.KeyExists("Recived_list") || Adding_new_contract == true)
            {

                Recived_contract.instance.m_Recived_contract.Add(m_Contracts[Recived_list_count]);
                //   ES3.Save<Contract>("Recived_contracts"+Recived_list_count, m_Contracts[i]);
                ES3.Save<Recived_contract>("Recived_list", Recived_contract.instance);
            }
            else
            {
                Debug.LogWarning("KeyExists=Recived_contracts" + Recived_list_count);
            }

            GameObject LineBox_ui_prefab = Instantiate(lineBox_ui_prefab, Contens.transform);
            LineBox_ui_prefab.transform.SetAsFirstSibling();
          //  LineBox_ui_prefab.GetComponent<LineBox_ui>().Tick.isOn = false;
            // m_Contracts[i].GetComponent<Contract>().c_LineBox_ui = LineBox_ui_prefab.GetComponent<LineBox_ui>();
            lineBox_ui_prefab.GetComponent<LineBox_ui>().P_contract = m_Contracts[add_newContract+i].GetComponent<Contract>();
            lineBox_ui_prefab.GetComponent<LineBox_ui>().parent_paper = paprer_parent;

           
            //when finsh one contract need to remove is_accepted bool for the previous contract 
            LineBox_ui_prefab.GetComponent<LineBox_ui>().listNumber = add_newContract+i;

            ES3.Save<int>("Recived_list_count", Recived_contract.instance.m_Recived_contract.Count);
        }

        Recived_list_count = Recived_contract.instance.m_Recived_contract.Count;
        New_msg_text.text = Recived_contract.instance.m_Recived_contract.Count.ToString();

    }
    void GetnewCotntracts(int Count_of_new)
    {
       
        
        for (int i = 0; i < Count_of_new; i++)
        {
            if (!ES3.KeyExists("Recived_list")||Adding_new_contract==true)
            {
                Recived_contract.instance.Finished_boolList.Add(false);
                Recived_contract.instance.m_Recived_contract.Add(m_Contracts[Recived_list_count]);
             //   ES3.Save<Contract>("Recived_contracts"+Recived_list_count, m_Contracts[i]);
                ES3.Save<Recived_contract>("Recived_list", Recived_contract.instance);
               
            }
            else
            {
                Debug.LogWarning("KeyExists=Recived_contracts" + Recived_list_count);
            }

            GameObject LineBox_ui_prefab = Instantiate(lineBox_ui_prefab, Contens.transform);
            LineBox_ui_prefab.transform.SetAsFirstSibling();
           
         //   LineBox_ui_prefab.GetComponent<LineBox_ui>().Tick.isOn = false; 
           // m_Contracts[i].GetComponent<Contract>().c_LineBox_ui = LineBox_ui_prefab.GetComponent<LineBox_ui>();
            lineBox_ui_prefab.GetComponent<LineBox_ui>().P_contract = m_Contracts[i].GetComponent<Contract>();
            lineBox_ui_prefab.GetComponent<LineBox_ui>().parent_paper = paprer_parent;

          

            //when finsh one contract need to remove is_accepted bool for the previous contract 
            LineBox_ui_prefab.GetComponent<LineBox_ui>().listNumber = i;
            LineBox_ui_prefab.GetComponent<LineBox_ui>().BackGorund_img.sprite = LineBox_ui_prefab.GetComponent<LineBox_ui>().new_Contract_img;

        }

        Recived_list_count = Recived_contract.instance.m_Recived_contract.Count;
        New_msg_text.text = Recived_contract.instance.m_Recived_contract.Count.ToString();

        ES3.Save<int>("Recived_list_count", Recived_contract.instance.m_Recived_contract.Count);
        
    }

    Vector3 newContractFirtPostiuon; // get the postion of opening 
    Vector3 newContractFirtscale;
    public void OpenNewContractScreen()
    {
        foreach(Image img in NewContractScreen.GetComponentsInChildren<Image>())
        {
           // img.DOFade(1, 0.1f);
        }

      //  newContractFirtPostiuon = NewContractScreen.localPosition;
      //  newContractFirtscale = NewContractScreen.localScale;
        NewContractScreen.gameObject.SetActive(true);

        Contens.localPosition = Vector3.zero;
      //  NewContractScreen.DOLocalMove(Vector3.zero, 0.2f);
      //  NewContractScreen.DOScale(new Vector3(1, 1, 1), 0.2f);

        
    }
    public void closeNewContractScreen()
    {
        foreach (Image img in NewContractScreen.GetComponentsInChildren<Image>())
        {
           // img.DOFade(0, 0.1f);
        }
        NewContractScreen.gameObject.SetActive(false);
        //  NewContractScreen.DOLocalMove(newContractFirtPostiuon, 0.2f);
        ///  NewContractScreen.DOScale(newContractFirtscale, 0.2f);

        Invoke(nameof(setActiveNewContract), 0.2f);
    }
    void setActiveNewContract()
    {
        NewContractScreen.gameObject.SetActive(false);
    }

    void setUniqeNumberForLinebox()
    {
     //  ES3.Load<int>("LineBox_count")
     //
     //  int number = 
    }
}
