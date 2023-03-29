using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Levels : MonoBehaviour
{
    public List<Transform> levles;
    public string Scene_Name;
    public int Next_count;
    private void Awake()
    {


        Scene_Name = SceneManager.GetActiveScene().name;

        if (!ES3.KeyExists(Scene_Name + "next_Count"))
        {
            Next_count = 0;
        }
        else
        {
            Next_count= ES3.Load<int>(Scene_Name + "next_Count");
        }


        if (ES3.KeyExists("Is_repeat"))
        {
            if (ES3.Load<bool>("Is_repeat", true))
            {
                Next_count--;
                ES3.Save<bool>("Is_repeat",false);
            }
        }
    }
    public void One_level_finished()
    {
        Next_count++;
        ES3.Save<int>(Scene_Name + "next_Count", Next_count);
    }
    public void tst_finished(int next)
    {
        next++;
        ES3.Save<int>(Scene_Name + "next_Count", next);
    }
    public void repeat()
    {
    
    }
}
