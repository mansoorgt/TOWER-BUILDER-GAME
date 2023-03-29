using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Name_Input_page : MonoBehaviour
{
    public Toggle Male_toggle, female_toggle;

    public TMP_InputField InputField;
    public bool Male;
    public TMP_Dropdown Age_dropDown;
    public Sprite Male_sprite, female_sprite;
    public Image Profile;
    private void Awake()
    {
        if (ES3.KeyExists("name_input_page_finish"))
        {
            if (ES3.Load<bool>("name_input_page_finish"))
            {
                gameObject.SetActive(false);
            }

        }
     
    }
    public void OnChangeGenderToggle(bool Male)
    {
        if (Male)
        {
            if (Male_toggle.isOn)
            {
                female_toggle.isOn = false;
                Profile.sprite = Male_sprite;
            }
            else
            {
                female_toggle.isOn = true;
                Profile.sprite = female_sprite;
            }
            
        }
        else
        {

            if (female_toggle.isOn)
            {
                Male_toggle.isOn = false;
                Profile.sprite = female_sprite;
            }
            else
            {
                Male_toggle.isOn = true;
                Profile.sprite = Male_sprite;
            }
            
        }
        

    }

    public void OnSave()
    {
        

        if (Male_toggle.isOn == false&&female_toggle.isOn==false)
        {
            Notifie.instace.OpenSimpleNotifie("Please Select Gender Type To continue", "6B82C6");
            return;
        }
        if (string.IsNullOrEmpty(InputField.text))
        {
            Notifie.instace.OpenSimpleNotifie("Enter your name the field above to continue", "6B82C6");
            return;

        }


        //if male is true is male. if male flase is female
        if (Male_toggle.isOn)
        {
            ES3.Save<bool>("Gender", true);
            Debug.Log("Gedner=male");
        }
        else
        {
            ES3.Save<bool>("Gender", false);
            Debug.Log("Gedner=Female");
        }

        ES3.Save<string>("Player_Name", InputField.text);
        ES3.Save<int>("Age", (Age_dropDown.value + 5));

        ES3.Save<bool>("name_input_page_finish", true);
        ES3.Save<Sprite>("Main_avathar", Profile.sprite);
        Debug.Log("Name=" + InputField.text);
        Debug.Log("Age=" + (Age_dropDown.value+5));


        Notifie.instace.OpenSimpleLoading(gameObject, "6B82C6");
    }
}
