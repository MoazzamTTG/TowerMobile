using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class ButtonData : MonoBehaviour
{

    //public int btnId;
    public string ListId;
    public int myStatus;
    public string componentType;
    public string MyTitle;


    public bool _isSelected;
    public bool _isSelectionMode;


    public TextMeshProUGUI textLogo;
    public string sjtTypeTxt;

    bool getIt = false;
    private void Start()
    {
       
      
        if (myStatus == 0)
        {

            for (int i = 0; i <DataControll.instance.ButtonsMainSprites.Length; i++)
            {
                //if (MyTitle == ButtonsMainSprites[i].name)
                if (ListId == DataControll.instance.ButtonsMainSprites[i].name)
                {
                    gameObject.GetComponent<Image>().sprite = DataControll.instance.ButtonsMainSprites[i];
                }
            }
        }
        if (myStatus == 1)
        {
            gameObject.GetComponent<Button>().interactable = false;

            for (int i = 0; i < DataControll.instance.ButtonsMainSprites.Length; i++)
            {
              //  if (gameObject.GetComponent<Image>().sprite.name == DataControll.instance.ButtonsCompleteSprites[i].name)
                if (gameObject.GetComponent<ButtonData>().ListId== DataControll.instance.ButtonsCompleteSprites[i].name)
                {
                  //  Debug.Log("complete image name is : " + DataControll.instance.ButtonsCompleteSprites[i].name);
                    gameObject.GetComponent<Image>().sprite = DataControll.instance.ButtonsCompleteSprites[i];
                   // Debug.Log("Status complete image");
                }
            }

        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;
        }

    }
    private void OnEnable()
    {
        if (myStatus == 1)
        {
            gameObject.GetComponent<Button>().interactable = false;

        }
        else
        {
            gameObject.GetComponent<Button>().interactable = true;

        }

    }

    private void Update()
    {
        if (getIt == true)
        {

            if (myStatus == 0)
            {
              //  Debug.Log("in upated my status : " + myStatus + " button name is " + MyTitle);
                for (int i = 0; i < DataControll.instance.ButtonsMainSprites.Length; i++)
                {
                    //if (MyTitle == ButtonsMainSprites[i].name)
                    if (ListId == DataControll.instance.ButtonsMainSprites[i].name)
                    {
                        gameObject.GetComponent<Image>().sprite = DataControll.instance.ButtonsMainSprites[i];
                    }
                }
            }
            if (myStatus == 1)
            {
                gameObject.GetComponent<Button>().interactable = false;
                //Debug.Log("in upated my status : " +myStatus +" button name is "+MyTitle);


                for (int i = 0; i < DataControll.instance.ButtonsMainSprites.Length; i++)
                {
                    //  if (gameObject.GetComponent<Image>().sprite.name == DataControll.instance.ButtonsCompleteSprites[i].name)
                    if (gameObject.GetComponent<ButtonData>().ListId == DataControll.instance.ButtonsCompleteSprites[i].name)
                    {
                        //Debug.Log("in upated complete image name is : " + DataControll.instance.ButtonsCompleteSprites[i].name);
                        gameObject.GetComponent<Image>().sprite = DataControll.instance.ButtonsCompleteSprites[i];
                        //Debug.Log("Status complete image");
                    }
                }

            }
            else
            {
                gameObject.GetComponent<Button>().interactable = true;
            }

           
        }
    }

    public void GetDataID()
    {
        DataControll.instance.myCurrentId = ListId;
      //  Debug.Log("id ... " + DataControll.instance.myCurrentId);
        //textLogo.text=this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        textLogo.text = MyTitle;
        DataControll.instance.miniGameBackBuildingAnimation = ListId;
        DataControll.instance.BtnType = componentType;
        _isSelectionMode = true;
       // Debug.Log("Btn Name " + ListId + " Btn type " + componentType);

    }

    public void FrontBuildingSpawn()
    {
        //DataControll.instance.buildingAnim.SetBool("BUp", false);
        DataControll.instance.BackBuildingName = ListId;
        StartCoroutine(BuildingSpawnWithWait());


    }


    IEnumerator BuildingSpawnWithWait()
    {
        CheckBtnSelectedOrNot();
        BackBuildingsChangeOnComponentId();


        //   float timer = (gameObject.GetComponent<ButtonData>().componentType == "SJT") ? 2.2f : 1.0f;

        yield return new WaitForSeconds(2.2f);

      //  Debug.Log("Before check component type");
        //if (gameObject.GetComponent<ButtonData>().componentType == "SJT") {

        for (int i = 0; i < DataControll.instance.AllPrefabsBuildings.Count; i++)
        {
            // Debug.Log("for loop component type");

            if (gameObject.GetComponent<ButtonData>().ListId == DataControll.instance.AllPrefabsBuildings[i].name)
            //if (gameObject.GetComponent<ButtonData>().MyTitle == DataControll.instance.AllPrefabsBuildings[i].name)
            {
               // Debug.Log("component id equal");
                DataControll.instance.spawnBuilding = Instantiate(DataControll.instance.AllPrefabsBuildings[i], DataControll.instance.buildingPlace.transform.position, DataControll.instance.buildingPlace.transform.rotation, DataControll.instance.buildingParent.transform);
                DataControll.instance.spawnBuilding.SetActive(true);
                DataControll.instance.buildingAnim = DataControll.instance.spawnBuilding.GetComponent<Animator>();
                DataControll.instance.buildingAnim.SetBool("BUp", true);
               // Debug.Log("its work");


            }
            else
            {
              // Debug.Log("Sjt id not found");
            }

        }

        //}
    }

    public void BackBuildingsChangeOnComponentId()
    {
       // Debug.Log("before back change " + DataControll.instance.BackBuildingName);
        for (int i = 0; i < DataControll.instance.AllBackBuildings.Count; i++)
        {

            if (ListId == DataControll.instance.AllBackBuildings[i].name)
            {
                DataControll.instance.BackBuildingName = DataControll.instance.AllBackBuildings[i].name;
               // Debug.Log("Back change " + DataControll.instance.BackBuildingName);
                // StartCoroutine(waitToTriggerOff());


            }
        }
    }

    IEnumerator waitToTriggerOff()
    {
        yield return new WaitForSeconds(.1f);
        DataControll.instance.TraggerObj.SetActive(false);
        //Debug.Log("tragger : " + DataControll.instance.TraggerObj.activeInHierarchy);
    }


    /// <summary>
    /// for select button sprite change
    /// </summary>
    public void CheckBtnSelectedOrNot()
    {


      //  Debug.Log("sprite.name   ->  "+ gameObject.GetComponent<Image>().sprite.name);


        _isSelected = true; // only this button are select
                            // ALL SLECTED buttons are set to not select


       // Debug.Log("gameObject - Competencies - ");
            /*+ JsonConvert.SerializeObject(DataControll.instance.stateSelector.competencies));
*/
        for (int j = 0; j < DataControll.instance.stateSelector.competencies.Count; j++)
        {
           // Debug.Log("competencies-");


            // gameObject.GetComponent<Image>().sprite;
            if (DataControll.instance.stateSelector.competencies[j].GetComponent<ButtonData>().myStatus == 0)
            {

                DataControll.instance.stateSelector.competencies[j].GetComponent<ButtonData>()._isSelected = false;
            }
            /*if (gameObject.GetComponent<ButtonData>()._isSelected == false)
            {

            if (gameObject.GetComponent<Image>().sprite.name == ButtonsMainSprites[i].name)
            {
                gameObject.GetComponent<Image>().sprite = ButtonsMainSprites[i];
                Debug.Log("this btn is selected " + _isSelected);
                Debug.Log("ButtonSelectedSprites name " + ButtonsMainSprites[i].name);
            }
            Debug.Log("All Are False " + DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>()._isSelected);
            }*/



        }
        for (int i = 0; i < DataControll.instance.stateSelector.competencies.Count; i++) // button image change to main image
        {
           // Debug.Log("competencies-2");

            //Debug.Log(" ButtonsMainSprites - ");
                /*+ JsonConvert.SerializeObject(DataControll.instance.ButtonsMainSprites));
*/
            for (int a = 0; a < DataControll.instance.ButtonsMainSprites.Length; a++)
            {
               // Debug.Log("competencies-2-in " + (DataControll.instance.stateSelector.competencies[i] != null));


                if (DataControll.instance.stateSelector.competencies[i] != null)
                {
                    //Debug.Log("competencies not null");
                    if(DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>() != null)
                    {

                       // Debug.Log("competencies ButtonData not null");
                      //  Debug.Log(DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>().name);
                    }

                   
                }

                    if (DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>()._isSelected == false)
                {
                  //  Debug.Log("competencies-2-in2 " + DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>()._isSelected);



                    /*    Debug.Log("1: " + (DataControll.instance.stateSelector.competencies[i].GetComponent<Image>() != null));
                        Debug.Log("2: " + (DataControll.instance.stateSelector.competencies[i].GetComponent<Image>().sprite != null));
                        Debug.Log("3: " + (DataControll.instance.stateSelector.competencies[i].GetComponent<Image>().sprite.name != null));
                        Debug.Log("4: " + (DataControll.instance.ButtonsMainSprites[a] != null));
                        Debug.Log("5: " + (DataControll.instance.ButtonsMainSprites[a].name != null));
                        Debug.Log("6: " + (DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>() != null));
                        Debug.Log("7: " + (DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>().myStatus != null));*/
                    /*  if (DataControll.instance.stateSelector.competencies[i].activeInHierarchy==true)
                      {*/
                    if (DataControll.instance.stateSelector.competencies[i].GetComponent<Image>().sprite != null)
                    {
                      //  Debug.Log("is active -- " + DataControll.instance.stateSelector.competencies[i].name);
                        if (DataControll.instance.stateSelector.competencies[i].GetComponent<Image>().sprite.name == DataControll.instance.ButtonsMainSprites[a].name
                              && DataControll.instance.stateSelector.competencies[i].GetComponent<ButtonData>().myStatus == 0)
                        {
                          //  Debug.Log("competencies-2-in3");
                            DataControll.instance.stateSelector.competencies[i].GetComponent<Image>().sprite = DataControll.instance.ButtonsMainSprites[a];

                            //Debug.Log("ButtonSelected " + ButtonsMainSprites[a].name);
                        }
                    }
/*                          
                    }*/


                }
            }
        }


       // Debug.Log(" ButtonsMainSprites-2 - ");
        /*+ JsonConvert.SerializeObject(DataControll.instance.ButtonsMainSprites));
*/

        for (int k = 0; k < DataControll.instance.ButtonSelectedSprites.Length; k++)// only this button sprite change into yellow
        {

            /*Debug.Log(" ButtonsMainSprites - index - "+k+" --> " + JsonConvert.SerializeObject(DataControll.instance.ButtonSelectedSprites[k]));
           */
          //  Debug.Log(k + " - " + DataControll.instance.ButtonSelectedSprites[k].name);


           if (gameObject.GetComponent<Image>().sprite.name == DataControll.instance.ButtonSelectedSprites[k].name)
            {
                gameObject.GetComponent<Image>().sprite = DataControll.instance.ButtonSelectedSprites[k];
                // Debug.Log("this btn is selected " + _isSelected);
                // Debug.Log("ButtonSelectedSprites name " + DataControll.instance.ButtonSelectedSprites[k].name);
                //gameObject.GetComponent<Button>().interactable = false;
                //Debug.Log("button interactable off ");

            }
            else
            {
               // Debug.Log("selected button sprite not found " + gameObject.GetComponent<Image>().sprite.name);
            }
            
        }




    }
}
