using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Trail_Placement_editor : EditorWindow
{

    public GameObject[] Array;
    public List<GameObject> Spwand_objects;
    public Transform Trailobj_parents;
    public Transform levels_tr;

    public int Selcted_Count;

    Vector3 nextPos;
    GameObject Spawed_obj;
    [MenuItem("MyEditors/Trail_editor")]
    public static void ShowWindow()
    {
        GetWindow<Trail_Placement_editor>("TrailPlacement");
    }
    private void OnGUI()
    {
        ScriptableObject ScriptableObj = this;
        SerializedObject SeialObj = new SerializedObject(ScriptableObj);
        SerializedProperty SerilProp = SeialObj.FindProperty("Array");
        SerializedProperty ParentProp = SeialObj.FindProperty("Trailobj_parents");
        SerializedProperty SelectedCountProp = SeialObj.FindProperty("Selcted_Count");
        SerializedProperty Spwand_Prop = SeialObj.FindProperty("Spwand_objects");
        SerializedProperty Levle_tr = SeialObj.FindProperty("levels_tr");



        EditorGUILayout.PropertyField(Levle_tr, true);
        EditorGUILayout.PropertyField(ParentProp, true);
        EditorGUILayout.PropertyField(SerilProp, true);
        EditorGUILayout.PropertyField(SelectedCountProp, true);

        SeialObj.ApplyModifiedProperties();

        ////

        Event e = Event.current;
        
       
        
        switch (e.type)
        {
            case EventType.KeyDown:
                {
                    if (Event.current.keyCode == KeyCode.Space)
                    {
                        Debug.Log(nextPos);
                        if (Trailobj_parents == null)
                        {
                            GameObject new_trailBox = new GameObject("Trail_set_" +1);
                            new_trailBox.transform.parent = levels_tr;
                            Trailobj_parents = new_trailBox.transform;
                        }
                        Spawed_obj=Instantiate(Array[Selcted_Count].gameObject,nextPos,Quaternion.identity,Trailobj_parents);
                        if (Trailobj_parents.GetComponent<TrailManeger>() == null)
                        {
                            Trailobj_parents.gameObject.AddComponent<TrailManeger>();
                        }
                        
                        Spawed_obj.GetComponentInChildren<TrailTile>().Parent_trailManeger = Trailobj_parents.GetComponent<TrailManeger>();

                        Spawed_obj.transform.localPosition = nextPos;
                        if (Spwand_objects.Count == 0)
                        {
                            Spawed_obj.transform.localPosition =new Vector3(0,0,0);
                            Debug.Log("new");
                        }
                        
                        Spwand_objects.Add(Spawed_obj);
                    }


                    if (Event.current.keyCode == KeyCode.W)
                    {

                        Spawed_obj.transform.localPosition += new Vector3(0, 2, 0);
                        nextPos = Spawed_obj.transform.localPosition + new Vector3(0, 2, 0);
                    

                    }
                    if (Event.current.keyCode == KeyCode.S)
                    {

                        Spawed_obj.transform.localPosition += new Vector3(0, -2, 0);
                        nextPos = Spawed_obj.transform.localPosition + new Vector3(0, 2, 0);


                    }
                    if (Event.current.keyCode == KeyCode.A)
                    {

                        Spawed_obj.transform.localPosition += new Vector3(-2,0 , 0);
                        nextPos = Spawed_obj.transform.localPosition + new Vector3(0, 2, 0);


                    }
                    if (Event.current.keyCode == KeyCode.D)
                    {

                        Spawed_obj.transform.localPosition += new Vector3(2, 0, 0);
                        nextPos = Spawed_obj.transform.localPosition + new Vector3(0, 2, 0);


                    }

                }



                break;

        }





        GUI.backgroundColor = Color.blue;
        if (GUILayout.Button("singleBox"))
        {
            Selcted_Count = 0;
        }
        if (GUILayout.Button("TopSingle"))
        {
            Selcted_Count = 1;
        }
        if (GUILayout.Button("Trail_box(3x1)"))
        {
            Selcted_Count = 2;
        }
        if (GUILayout.Button("Trail_box(3x1)_Top"))
        {
            Selcted_Count = 3;
        }
        

        GUILayout.Space(5);


        
        GUI.backgroundColor = Color.red;
        EditorGUILayout.PropertyField(Spwand_Prop, true);

        GUI.backgroundColor = Color.red;
        Rect Remove_rect = new Rect(0, 300, 50, 50);
        if (GUI.Button(Remove_rect,"Remove"))
        {
            

            DestroyImmediate(Spwand_objects[Spwand_objects.Count - 1]);
            Spwand_objects.Remove(Spwand_objects[Spwand_objects.Count - 1]);
            
           
        }
        GUI.backgroundColor = Color.blue;
        Rect Clear_rect = new Rect(50, 300, 50, 50);
        if (GUI.Button(Clear_rect,"Clear"))
        {
            
            Spwand_objects.Clear();

           
        }
        GUI.backgroundColor = Color.green;
        Rect Fresh_rect = new Rect(100, 300, 50, 50);
        if (GUI.Button(Fresh_rect,"Fresh"))
        {
            for(int i = 0; i < Spwand_objects.Count - 1; i++)
            {
                DestroyImmediate(Spwand_objects[i]);
                Spwand_objects.Remove(Spwand_objects[i]);
            }
            nextPos = new Vector3(0, 0, 0);
            
        }
        GUI.backgroundColor = Color.yellow;
        Rect Save_rect = new Rect(150, 300, 100,50);
        if (GUI.Button(Save_rect,"To_save_count"))
        {
          // if (Trailobj_parents.GetComponent<TrailManeger>() == null)
          // {
          //     Trailobj_parents.gameObject.AddComponent<TrailManeger>();
          // }
            Trailobj_parents.GetComponent<TrailManeger>().Max_trailCount = Spwand_objects.Count;
            Trailobj_parents.gameObject.SetActive(false);
            levels_tr.GetComponent<Levels>().levles.Add(Trailobj_parents.transform);
            Spwand_objects.Clear();
            int count_of_levels = levels_tr.childCount+1; 
            GameObject new_trailBox = new GameObject("Trail_set_"+count_of_levels);
            new_trailBox.transform.parent = levels_tr;
            Trailobj_parents = new_trailBox.transform;


            GameObject Pre = new GameObject();

            Pre.name = SceneManager.GetActiveScene().name+"_Trail_";

            //not for nessory just for to create the variable
            string LocalPath = "Assets/Other_prefabs/Contracts/Riyad/";

            if (SceneManager.GetActiveScene().buildIndex == (int)MyScenes.Riyad)
            {
                
                Pre.AddComponent<TrailSet_info>().Scene_int = (int)MyScenes.Riyad;
                LocalPath = "Assets/Other_prefabs/Contracts/Riyad/" + Pre.name + (count_of_levels-1) + ".prefab"; 


            }
            if (SceneManager.GetActiveScene().buildIndex == (int)MyScenes.uae)
            {
                Pre.AddComponent<TrailSet_info>().Scene_int = (int)MyScenes.uae;
                LocalPath = "Assets/Other_prefabs/Contracts/UAE/" + Pre.name + (count_of_levels - 1) + ".prefab"; 
            }
            if (SceneManager.GetActiveScene().buildIndex == (int)MyScenes.quator)
            {
                Pre.AddComponent<TrailSet_info>().Scene_int = (int)MyScenes.quator;
                LocalPath = "Assets/Other_prefabs/Contracts/Quator/" + Pre.name + (count_of_levels - 1) + ".prefab"; 
            }

            AssetDatabase.GenerateUniqueAssetPath(LocalPath);

            PrefabUtility.SaveAsPrefabAssetAndConnect(Pre, LocalPath, InteractionMode.UserAction);

            DestroyImmediate(Pre);

       //
       //    Object prefab =AssetDatabase.LoadAssetAtPath("Assets/Other_prefabs/Contracts/Trail_Set_Contract_1.prefab", typeof(GameObject));
       //  Debug.Log(prefab.name);
       // MonoBehaviour monoBev = (MonoBehaviour)prefab;
       // TrailSet_info Info = monoBev.GetComponent<TrailSet_info>();
       //// Info.Scene_int = 2;
       //  AssetDatabase.CreateAsset(prefab, "Assets/Other_prefabs/Contracts/");
       //     Debug.Log(count_of_levels);
        }
        
       
    }
}
