using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
public class Loading_manager : MonoBehaviour
{
    public static Loading_manager instace { set; get; }
    public GameObject LoadingScreen,MainScreen,Loading_Sheet;
    public TextMeshProUGUI Progress_text;
    public Animator Anim;
    float Progress;
    public bool Loading,Org_start;

    [Serializable]
    public class datas {

        public Sprite Image;
        public Color[] Colors = new Color[4];
    }
    

    public List<datas> Image_contantes;

    float m_Progress;
    float First_Current_time=0,Seconf_current_Time=0;

    private void Awake()
    {
       
        
        if (instace == null)
        {
            instace = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(loadAsynchronously(sceneIndex));

        for (int i = 0; i < Loading_Sheet.transform.childCount; i++)
        {
           
            if (sceneIndex == (int)MyScenes.Riyad)
            {
                if (i == Loading_Sheet.transform.childCount-1)
                {
                    Loading_Sheet.transform.GetChild(i).GetComponent<Image>().sprite = Image_contantes[0].Image;
                    return;
                }
                Loading_Sheet.transform.GetChild(i).GetComponent<Image>().color = Image_contantes[0].Colors[i];
               // Debug.Log(i+Loading_Sheet.transform.GetChild(i).name);
            }

            if (sceneIndex == (int)MyScenes.uae)
            {
                if (i == Loading_Sheet.transform.childCount - 1)
                {
                    Loading_Sheet.transform.GetChild(i).GetComponent<Image>().sprite = Image_contantes[1].Image;
                    return;
                }
                Loading_Sheet.transform.GetChild(i).GetComponent<Image>().color = Image_contantes[1].Colors[i];
              //  Debug.Log(i + Loading_Sheet.transform.GetChild(i).name);
            }

            if (sceneIndex == (int)MyScenes.quator)
            {
                if (i == Loading_Sheet.transform.childCount - 1)
                {
                    Loading_Sheet.transform.GetChild(i).GetComponent<Image>().sprite = Image_contantes[2].Image;
                    return;
                }
                Loading_Sheet.transform.GetChild(i).GetComponent<Image>().color = Image_contantes[2].Colors[i];
                //  Debug.Log(i + Loading_Sheet.transform.GetChild(i).name);
            }
            if (sceneIndex == (int)MyScenes.lobby)
            {
                if (i == Loading_Sheet.transform.childCount - 1)
                {
                    Loading_Sheet.transform.GetChild(i).GetComponent<Image>().sprite = Image_contantes[3].Image;
                    return;
                }
                Loading_Sheet.transform.GetChild(i).GetComponent<Image>().color = Image_contantes[3].Colors[i];
                //  Debug.Log(i + Loading_Sheet.transform.GetChild(i).name);
            }

        }
    }
    private void Update()
    {
        if (Loading)
        {
            if (!Org_start)
            {
                if (First_Current_time <= 6)
                {
                    First_Current_time += Time.deltaTime;
                    m_Progress = (int)Mathf.Lerp(0, 20f, First_Current_time/6);
                    Progress_text.text = m_Progress + "%";
                }
             

            }
            else
            {
                if (Seconf_current_Time <= 4)
                {
                    Seconf_current_Time += Time.deltaTime;
                    m_Progress= (int)Mathf.Lerp(20, 90f, Seconf_current_Time / 4);
                    Progress_text.text = m_Progress + "%";
                }
               
            }
        }
    }
    IEnumerator loadAsynchronously(int sceneIndex)
    {
        m_Progress = 0;
        First_Current_time = 0;
        Seconf_current_Time = 0;
        Loading = true;

        Org_start = false;



        Anim.SetBool("loading_Start", true);

   
        yield return new WaitForSeconds(1f);

      //  MainScreen.SetActive(false);
        LoadingScreen.SetActive(true);

        yield return new WaitForSeconds(6f);

        Org_start = true;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {

        

            Progress = Mathf.Clamp01(operation.progress / .9f);

            if (Mathf.Approximately(operation.progress,0.9f)&&m_Progress>=90)
            {
                Progress_text.text = 100 + "%";
                Anim.SetBool("loading_Start", false);
                operation.allowSceneActivation = true;

            }

            yield return null;
        }
       
        Loading = false;
        Debug.Log("Loading_end");
    }
}
