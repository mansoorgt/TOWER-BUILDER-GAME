using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class LineBox_ui : MonoBehaviour
{
    public int listNumber;




    //contract input avatar puting right there this avatar the image obj
    public Image Avatar;
    public Contract P_contract;
    public RectTransform parent_paper;

    public GameObject paper_intiate;

    public Image BackGorund_img;
    public Sprite new_Contract_img, slected_contract_img, Done_contract_img;

    //if under this linebox contract aceptedl
    public bool Is_accepted;
    public bool is_finished;
    private void Start()
    {
        Avatar.sprite = P_contract.Avatar;
        if (Is_accepted)
        {
            //selected_img.SetActive(true);
            BackGorund_img.sprite = slected_contract_img;
        }
        if (is_finished)
        {
            BackGorund_img.sprite = Done_contract_img;
        }
    }
    public void open_contract_paper()
    {

        paper_intiate=Instantiate(P_contract.gameObject, parent_paper);
        paper_intiate.GetComponent<Contract>().c_LineBox_ui = GetComponent<LineBox_ui>();
        paper_intiate.GetComponent<Contract>().Open_animator.SetTrigger("Open");

        if (Is_accepted)
        {

            paper_intiate.GetComponent<Contract>().Accept_Bu.SetActive(false);
            paper_intiate.GetComponent<Contract>().later_bu_text.text = "Back";

        }
       // parent_paper.DOScale(new Vector3(1, 1, 1), 0.2f).SetEase(Ease.InFlash);
    }
    public void Close_contractpaper()
    {
        //  parent_paper.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f).SetEase(Ease.InBack);

        // Destroy(, 0.2f);
        paper_intiate.GetComponent<Contract>().Open_animator.SetTrigger("Close");
        Debug.Log("destroyed");
    }


}
