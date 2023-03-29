using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{

    public RectTransform F_Arrwo;
    public RectTransform Side_lable_panel;
    public RectTransform BackGround, BackGround_border;
    float Lenght_of_inventoryPage = 300;
    //to move out side of side labl panel for when update objectainer list dont animate this one
    public RectTransform prt_obj_contrainer;
    public RectTransform Invetory_Main_parent;
    public RectTransform Parents;

    bool InvetoryOpend;

    public GameObject Button_prefab;

    public Button Slected_button_singleBox;
    public Button Slected_button_TopsingleBox;
    public Button Slected_button_TreeBox;
    public Button Slected_button_TopTreeBox;
    public List<Button> Selected_button_list;

    public RectTransform parent_SingleBox;
    public RectTransform parent_topsingleBox;
    public RectTransform parent_Treebox;
    public RectTransform parent_topTreeBox;
    public RectTransform Parent_obj_container;

    public List<Objects_Container> Obj_container_list;
    public Objects_Container Selected_Container;

    public List<GameObject> temp_SingleBox;
    public List<GameObject> temp_TopSingleBox;
    public List<GameObject> temp_threeBox;
    public List<GameObject> temp_TopthreeBox;
    public List<Objects_Container> temp_ObjContainer;

    public List<GameObject> Button_SingleBox;
    public List<GameObject> Button_TopSingleBox;
    public List<GameObject> Button_threeBox;
    public List<GameObject> Button_TopthreeBox;
    public List<GameObject> Button_obj_containers;

    [Header("AnimationFloats")]
    public float SidePanleRight = -430;

    int lastSlectedInt = 1;

    private void Awake()
    {
        InventoryArrow();
        InventoryArrow();
    }
    private void Start()
    {
        Selected_button_list.Add(Slected_button_singleBox);
        Selected_button_list.Add(Slected_button_TopsingleBox);
        Selected_button_list.Add(Slected_button_TreeBox);
        Selected_button_list.Add(Slected_button_TopTreeBox);

        update_container_List();
        _Starting();



    }

    void update_container_List()
    {


        //getSingleBox
        foreach (GameObject Obj in Selected_Container.SingleBox)
        {
            temp_SingleBox.Add(Obj);
        }
        //getTopSingleBox
        foreach (GameObject Obj in Selected_Container.TopSingleBox)
        {
            temp_TopSingleBox.Add(Obj);
        }
        //gettreeBox
        foreach (GameObject Obj in Selected_Container.threeBox)
        {
            temp_threeBox.Add(Obj);
        }
        //getTopThreeBox
        foreach (GameObject Obj in Selected_Container.TopthreeBox)
        {
            temp_TopthreeBox.Add(Obj);
        }
        foreach (Objects_Container obj in Obj_container_list)
        {
            temp_ObjContainer.Add(obj);
        }


        //Remove child Under Parent
        foreach (Transform child in parent_SingleBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in parent_topsingleBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in parent_topTreeBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in parent_Treebox.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in Parent_obj_container.transform)
        {
            Destroy(child.gameObject);
        }


        //add currect Amount Of items

        for (int i = 0; i < temp_SingleBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_SingleBox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.SignleBox_sprite[i];

            int x = i;
            Button_SingleBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_SingleBox, Button_SingleBox, Slected_button_singleBox));


        }
        for (int i = 0; i < temp_TopSingleBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_topsingleBox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.TopSingleBox_sprite[i];

            int x = i;

            Button_TopSingleBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_TopSingleBox, Button_TopSingleBox, Slected_button_TopsingleBox));

        }
        for (int i = 0; i < temp_threeBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_Treebox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.threeBox_sprite[i];

            int x = i;

            Button_threeBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_threeBox, Button_threeBox, Slected_button_TreeBox));

        }
        for (int i = 0; i < temp_TopthreeBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_topTreeBox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.TopthreeBox_sprite[i];

            int x = i;

            Button_TopthreeBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_TopthreeBox, Button_TopthreeBox, Slected_button_TopTreeBox));
        }

        for (int i = 0; i < temp_ObjContainer.Count; i++)
        {
            GameObject newGo = Instantiate(Button_prefab, Parent_obj_container);
            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = temp_ObjContainer[i].GetComponent<Objects_Container>().container_Sprite;
            Button_obj_containers.Add(newGo);
            int x = i;
            newGo.GetComponent<Button>().onClick.AddListener(() => obj_contrainer_button(x));
           
        }
    }
    void update_invetory_List()
    {


        //getSingleBox
        temp_SingleBox.Clear();
        foreach (GameObject Obj in Selected_Container.SingleBox)
        {
            temp_SingleBox.Add(Obj);
        }
        //getTopSingleBox
        temp_TopSingleBox.Clear();
        foreach (GameObject Obj in Selected_Container.TopSingleBox)
        {
            temp_TopSingleBox.Add(Obj);
        }
        //gettreeBox
        temp_threeBox.Clear();
        foreach (GameObject Obj in Selected_Container.threeBox)
        {
            temp_threeBox.Add(Obj);
        }
        //getTopThreeBox
        temp_TopthreeBox.Clear();
        foreach (GameObject Obj in Selected_Container.TopthreeBox)
        {
            temp_TopthreeBox.Add(Obj);
        }
     //  foreach (Objects_Container obj in Obj_container_list)
     //  {
     //      temp_ObjContainer.Add(obj);
     //  }


        //Remove child Under Parent
        foreach (Transform child in parent_SingleBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in parent_topsingleBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in parent_topTreeBox.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in parent_Treebox.transform)
        {
            Destroy(child.gameObject);
        }



        //add currect Amount Of items

        for (int i = 0; i < temp_SingleBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_SingleBox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.SignleBox_sprite[i];

            int x = i;
            Button_SingleBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_SingleBox, Button_SingleBox, Slected_button_singleBox));


        }
        for (int i = 0; i < temp_TopSingleBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_topsingleBox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.TopSingleBox_sprite[i];

            int x = i;

            Button_TopSingleBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_TopSingleBox, Button_TopSingleBox, Slected_button_TopsingleBox));

        }
        for (int i = 0; i < temp_threeBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_Treebox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.threeBox_sprite[i];

            int x = i;

            Button_threeBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_threeBox, Button_threeBox, Slected_button_TreeBox));

        }
        for (int i = 0; i < temp_TopthreeBox.Count; i++)
        {

            GameObject newGo = Instantiate(Button_prefab, parent_topTreeBox);

            newGo.GetComponent<Invertory_button_sc>().Icon_sprite = Selected_Container.TopthreeBox_sprite[i];

            int x = i;

            Button_TopthreeBox.Add(newGo);
            newGo.GetComponent<Button>().onClick.AddListener(() => Button_Numeber(x, temp_TopthreeBox, Button_TopthreeBox, Slected_button_TopTreeBox));
        }


      // for (int i = 0; i < temp_ObjContainer.Count; i++)
      // {
      //     GameObject newGo = Instantiate(Button_prefab, Parent_obj_container);
      //     newGo.GetComponent<Invertory_button_sc>().Icon_sprite = temp_ObjContainer[i].GetComponent<Objects_Container>().container_Sprite;
      //     Button_obj_containers.Add(newGo);
      //     int x = i;
      //     newGo.GetComponent<Button>().onClick.AddListener(() => obj_contrainer_button(x));
      //
      // }
    }
    public void obj_contrainer_button(int number)
    {
        
        prt_obj_contrainer.parent = Invetory_Main_parent;
        prt_obj_contrainer.SetSiblingIndex(Invetory_Main_parent.childCount - 2);
        Side_lable_panel.DOSizeDelta(new Vector2(-SidePanleRight, Side_lable_panel.sizeDelta.y), 0.3f, true);

        Parents.localPosition = new Vector3(300, Parents.localPosition.y, Parents.localPosition.z);
        StartCoroutine(Obj_container_invoke(number));


       
    }
    IEnumerator Obj_container_invoke(int number)
    {
        yield return new WaitForSeconds(0.4f);

        Parents.DOLocalMoveX(1000, 0.1f);
        Side_lable_panel.DOSizeDelta(new Vector2(SidePanleRight, Side_lable_panel.sizeDelta.y), 0.3f, true);
        Button_SingleBox.Clear();
        Button_TopSingleBox.Clear();
        Button_TopthreeBox.Clear();
        Button_threeBox.Clear();

        Selected_Container = Obj_container_list[number];

        update_invetory_List();
        _Starting();
        Invoke(nameof(invoker), 0.3f);
       
    }
    void invoker()
    {
        //this function do the parnet moving to the orginal pos after animation done 
       // Parents.DOLocalMoveX(300, 0.1f);
        prt_obj_contrainer.parent = Parents;
    }
    
    public void InventoryArrow()
    {



        if (!InvetoryOpend)
        {
            //ToOpen
            BackGround.GetComponent<Image>().DOFade(1, 0.3f);
            BackGround_border.GetComponent<Image>().DOFade(1, 0.3f);
            F_Arrwo.DOLocalRotate(new Vector3(0, 0, 90), 0.5f);
            Side_lable_panel.DOSizeDelta(new Vector2(SidePanleRight, Side_lable_panel.sizeDelta.y), 0.3f, true);
            InvetoryOpend = true;
        }
        else
        {
            //ToClose
            BackGround.GetComponent<Image>().DOFade(0, 0.3f);
            BackGround_border.GetComponent<Image>().DOFade(0, 0.3f);
            F_Arrwo.DOLocalRotate(new Vector3(0, 0, -90), 0.5f);
            Side_lable_panel.DOSizeDelta(new Vector2(-SidePanleRight, Side_lable_panel.sizeDelta.y), 0.3f, true);
            InvetoryOpend = false;
        }
       
    }
    void Button_Numeber(int number,List<GameObject> temp_list,List<GameObject>buttonList,Button SlectedButton)
    {
       
        //chamge ivevotry button color
        foreach(GameObject button_List in buttonList)
        {
            
            button_List.GetComponent<Image>().sprite = button_List.GetComponent<Invertory_button_sc>().Normal_Plane;
        }

      
        buttonList[number].GetComponent<Image>().sprite = buttonList[number].GetComponent<Invertory_button_sc>().sleceted_plane;

        //SideButton Change color
        // foreach(Button Bu in Selected_button_list)
        // {
        //     Bu.GetComponent<Image>().color = Color.red;
        // }
        //
        // SlectedButton.GetComponent<Image>().color = Color.yellow;

        if (SlectedButton.GetComponent<Selected_button_Sc>().Is_Selcted)
        {
            Crain.instace.Pre_Box = temp_list[number];
        }
            
        //change slected side lable button onclick listener
        SlectedButton.onClick.RemoveAllListeners();
        SlectedButton.onClick.AddListener(() => Sleceted_button(number,temp_list,SlectedButton));


        
    }
    public void Sleceted_button(int Number,List<GameObject> Temp_list,Button selectedButton)
    {
        Debug.Log("Seted" + Number);
        
        foreach (Button Bu in Selected_button_list)
        {
            Bu.GetComponent<Selected_button_Sc>().Is_Selcted = false;

            Bu.GetComponent<Image>().sprite=Bu.GetComponent<Selected_button_Sc>().normalimge;
           // Bu.GetComponent<Image>().color = Color.white;
            //Bu.GetComponent<Image>().color = Color.red;
        }
        selectedButton.GetComponent<Selected_button_Sc>().Is_Selcted = true;

        selectedButton.GetComponent<Image>().sprite = selectedButton.GetComponent<Selected_button_Sc>().SelctedImage;
        //  selectedButton.GetComponent<Image>().color = Color.yellow;
        Crain.instace.Pre_Box = Temp_list[Number];
          
       
    }
    void _Starting()
    {



        //last used button

        //sigleBox

       

        int Selected_item_singleBox = 0;
        Button_SingleBox[Selected_item_singleBox].GetComponent<Image>().sprite = Button_SingleBox[Selected_item_singleBox].GetComponent<Invertory_button_sc>().sleceted_plane;
        if (lastSlectedInt == 1)
        {
           
            Sleceted_button(Selected_item_singleBox,temp_SingleBox, Slected_button_singleBox);
            Crain.instace.Pre_Box = temp_SingleBox[Selected_item_singleBox];
           
        }

        //TopSinglebox
        int Selected_item_TopSingleBox = 0;
        Button_TopSingleBox[Selected_item_TopSingleBox].GetComponent<Image>().sprite = Button_TopSingleBox[Selected_item_TopSingleBox].GetComponent<Invertory_button_sc>().sleceted_plane; 
        if (lastSlectedInt == 2)
        {
           
            Sleceted_button(Selected_item_TopSingleBox, temp_TopSingleBox, Slected_button_TopsingleBox);
            Crain.instace.Pre_Box = temp_TopSingleBox[Selected_item_TopSingleBox];
           

        }


        //treeBox
        int Selected_item_treeBox = 0;
        Button_threeBox[Selected_item_treeBox].GetComponent<Image>().sprite = Button_threeBox[Selected_item_treeBox].GetComponent<Invertory_button_sc>().sleceted_plane;
        if (lastSlectedInt == 3)
        {
           
            Sleceted_button(Selected_item_treeBox, temp_threeBox, Slected_button_TreeBox);
            Crain.instace.Pre_Box = temp_threeBox[Selected_item_treeBox];
          
        }

        //TopTreeBox
        int Selected_item_TopTreeBox = 0;
        Button_TopthreeBox[Selected_item_TopTreeBox].GetComponent<Image>().sprite = Button_TopthreeBox[Selected_item_TopTreeBox].GetComponent<Invertory_button_sc>().sleceted_plane;
        if (lastSlectedInt == 4)
        {
            
            Sleceted_button(Selected_item_TopTreeBox, temp_TopthreeBox, Slected_button_TopTreeBox);
            Crain.instace.Pre_Box = temp_TopthreeBox[Selected_item_TopTreeBox];
            
        }

        //slecting firt button/
        
        foreach (Button Bu in Selected_button_list)
        {
            Bu.GetComponent<Image>().sprite = Bu.GetComponent<Selected_button_Sc>().normalimge;
            //  Bu.GetComponent<Image>().color = Color.red;

        }
      //  Selected_button_list[lastSlectedInt-1].GetComponent<Image>().color = Color.yellow;
        Selected_button_list[lastSlectedInt - 1].GetComponent<Image>().sprite= Selected_button_list[lastSlectedInt-1].GetComponent<Selected_button_Sc>().SelctedImage;

        //adding lisener to buttons
        Slected_button_singleBox.onClick.AddListener(() => Sleceted_button(0, temp_SingleBox, Slected_button_singleBox));
        Slected_button_TopsingleBox.onClick.AddListener(() => Sleceted_button(0, temp_TopSingleBox, Slected_button_TopsingleBox));
        Slected_button_TreeBox.onClick.AddListener(() => Sleceted_button(0, temp_threeBox, Slected_button_TreeBox));
        Slected_button_TopTreeBox.onClick.AddListener(() => Sleceted_button(0, temp_TopthreeBox, Slected_button_TopTreeBox));

    }
}
