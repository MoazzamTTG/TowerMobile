using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using Newtonsoft.Json;

using System.Collections;
//using System.Collections.Generic;
using UnityEngine.SceneManagement;

//[System.Serializable]
//public class CompetencyCollectionItem
//{
//    public string name;
//    public Competency competency;
//}

//[System.Serializable]
//public class CompetencyCollection
//{
//    [SerializeField]
//    List<CompetencyCollectionItem> competencies;

//    public Competency Get(string key)
//    {
//        return competencies.Find(x => x.name == key).competency;
//    }
//}

public class CompetencyStateSelector : MonoBehaviour
{
    public List<GameObject> competencies;

    public GameCenterLoader gameCenterLoader;

    public static int firstLoad=0;
    //[SerializeField]
    //GameObject marker; // comment by my side

    /*  [SerializeField]
      TMP_Text label;*/

    public List<ComponentDataObject> components;
    public string completeStatus;
    ComponentDataObject currentComponent;
    public TextMeshProUGUI SendID;
    public TextMeshProUGUI testingText;
    public TextMeshProUGUI testingText1;

    public SpriteRenderer FirstBackBuildingVisible;
    public GameObject DefaultBuilding;
    //[SerializeField]
    //GameObject SpriteRendererDictonary;


    public List<string> minigameComponentIDs = new List<string>();



    
    void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
       
        components = CFactor.Get<List<ComponentDataObject>>("components");

       
        Debug.Log("Component get cfactor");
      
    
#else
        components = new List<ComponentDataObject>()
        {
            new ComponentDataObject()
            {
               id = "SampleR",
                type = "UnityMinigame",
                status = 0,
                title = "trafficcontrol",
                componentId = "amnesia"
},
             new ComponentDataObject()
            {
               id = "SampleR",
                type = "UnityMinigame",
                status = 0,
                title = "moneycount",
                componentId = "moneycount"
},
              new ComponentDataObject()
            {
               id = "SampleR",
                type = "SJT",
                status = 0,
                title = "strategic-thinking",
                componentId = "strategic-thinking"
},
               new ComponentDataObject()
            {
               id = "SampleR",
                type = "UnityMinigame",
                status = 0,
                title = "defectdetect",
                componentId = "defectdetect"
},

            new ComponentDataObject()
            {
               id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "boxsort",
                componentId = "boxsort"
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "colormatch ",
                componentId = "colormatch"
            },
            new ComponentDataObject()
            {
                id = "SampleR",
                type = "UnityMinigame",
                status = 0,
                title = "handemright",
                componentId = "handemright",
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "flipflop",
                componentId = "flipflop"
            },
            new ComponentDataObject()
            {
                id = "SampleR",
                type = "UnityMinigame",
                status = 0,
                title = "hydrohurdle",
                componentId = "hydrohurdle"
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "cypher",
                componentId = "cypher"
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "moneysort",
                componentId = "moneysort"
            },
            new ComponentDataObject()
            {
                id = "SampleR",
                type = "UnityMinigame",
                status = 0,
                title = "makeway",
                componentId = "makeway"
            },
          /*  new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "Traffic Control",
                componentId = "trafficcontrol"
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "SJT",
                status = 0,
                title = "drive-for-results",
                componentId = "drive-for-results"
            }
            ,
            new ComponentDataObject()
            {
                id = "SampleR",
                type = "SJT",
                status = 0,
                title = "develop-others",
                componentId = "develop-others",
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "numberrush",
                componentId = "numberrush"
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "Office Alert",
                componentId = "officealert"
            },
            new ComponentDataObject()
            {
                id = "SampleL",
                type = "UnityMinigame",
                status = 0,
                title = "parkit",
                componentId = "parkit"
            }
*/
        };
#endif




        components = components.FindAll((ComponentDataObject component) =>
        {
            return new string[] { "SJT", "UnitySJT", "Minigame", "UnityMinigame", "Aptitute" }.Contains(component.type) && component.title != "";
        });


        Debug.Log("competencies :: "+ components.Count);

        for (var i = 0; i < components.Count; i++)
        {


            if (i >= competencies.Count)
                break;
            var component = components[i];
            Debug.Log("Total count of Competencies is : "+competencies.Count);
          // Debug.Log("component id name :"+components[i].componentId + " component id status : " + components[i].status);
            competencies[i].GetComponent<Competency>().setStatus(component.status.Value == 1);
            if (i <= components.Count)
            {
                competencies[i].gameObject.SetActive(true); // button active on list number

                competencies[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = components[i].title; // set button name with title
                competencies[i].gameObject.GetComponent<ButtonData>().ListId = components[i].componentId;
                competencies[i].gameObject.GetComponent<ButtonData>().componentType = components[i].type;
                competencies[i].gameObject.GetComponent<ButtonData>().MyTitle = components[i].title;
                if(competencies[i].gameObject.GetComponent<ButtonData>().componentType== "UnityMinigame" || competencies[i].gameObject.GetComponent<ButtonData>().componentType == "Minigame")
                {
                    
                    Debug.Log("Animation name : "+ components[i].componentId);
                    DataControll.instance.ComponentsForAnimation.Add(components[i].componentId);
                }
            }
            minigameComponentIDs.Add(components[i].componentId);
        }


        // assigning componentid on top button id to mycurrent id


        SetDatatoFirstTimePlaybtnPress();




        if (competencies[0].gameObject.GetComponent<ButtonData>().componentType == "UnityMinigame" || competencies[0].gameObject.GetComponent<ButtonData>().componentType == "Minigame")
        {
            //Debug.Log("into equal tpye");
            for (int i = 0; i < DataControll.instance.AllBackBuildings.Count; i++)
            {
                if (competencies[0].gameObject.GetComponent<ButtonData>().ListId == DataControll.instance.AllBackBuildings[i].name)
                {
                    FirstBackBuildingVisible.sprite = DataControll.instance.AllBackBuildings[i];

                    Debug.Log("Competency State: " + DataControll.instance.AllBackBuildings[i]);
                }
            }
        }
        else if (competencies[0].gameObject.GetComponent<ButtonData>().componentType == "UnitySJT" || competencies[0].gameObject.GetComponent<ButtonData>().componentType == "SJT")
        {
            //  Debug.Log("Competency State: " + components[0].componentId);
          //  Debug.Log("back building start");

            for (int i = 0; i < DataControll.instance.AllBackBuildings.Count; i++)
            {
                if (competencies[0].gameObject.GetComponent<ButtonData>().ListId == DataControll.instance.AllBackBuildings[i].name)
                {
                    Debug.Log("ListId : "+ competencies[0].gameObject.GetComponent<ButtonData>().ListId);
                    FirstBackBuildingVisible.sprite = DataControll.instance.AllBackBuildings[i];
                   // Debug.Log("DataControll.instance.AllBackBuildings[i].name : " + DataControll.instance.AllBackBuildings[i].name);
                }
                // Debug.Log("Competency State: "+ components[0].componentId);
            }



            for (int i = 0; i < DataControll.instance.AllPrefabsBuildings.Count; i++)
            {
                
                if (competencies[0].gameObject.GetComponent<ButtonData>().ListId == DataControll.instance.AllPrefabsBuildings[i].name)
               
                {

                  //  Debug.Log("DataControll.instance.buildingAnim  : " + DataControll.instance.buildingAnim.name);
                    DataControll.instance.spawnBuilding = Instantiate(DataControll.instance.AllPrefabsBuildings[i], DataControll.instance.buildingPlace.transform.position, DataControll.instance.buildingPlace.transform.rotation, DataControll.instance.buildingParent.transform);
                    DataControll.instance.spawnBuilding.SetActive(true);
                    DataControll.instance.buildingAnim = DataControll.instance.spawnBuilding.GetComponent<Animator>();
                    //Debug.Log("after DataControll.instance.buildingAnim  : " + DataControll.instance.buildingAnim.name);
                    DataControll.instance.buildingAnim.SetBool("BUp", true);
                     //Debug.Log("its work");


                }
                else
                {
                     Debug.Log("Sjt id not found");
                }

            }



        }

        else 
        { 
        DefaultBuilding.SetActive(true);

        }



#if UNITY_WEBGL && !UNITY_EDITOR
        CFactor.Loaded();
#endif
    }

    // Assigning and Set Data to First Time Play btn Press on start Game Center Screen
    public void SetDatatoFirstTimePlaybtnPress()
    {
        for (int i = 0; i < competencies.Count; i++)
        {
            if (competencies[i].gameObject.GetComponent<ButtonData>().myStatus == 0)
            {
               
                DataControll.instance.myCurrentId = competencies[i].gameObject.GetComponent<ButtonData>().ListId;
                Debug.Log(" After set myCurrentId : " + DataControll.instance.myCurrentId);


                /// Assign first button to component title and if unity mini game assign the btn image with thier animation
                DataControll.instance.MainTitle.text = competencies[i].gameObject.GetComponent<ButtonData>().MyTitle;
               // Debug.Log("My Current Tittle according to thier component ID : "+ competencies[i].gameObject.GetComponent<ButtonData>().MyTitle);
                break;
            }
        }


    }

    public void PlayClick()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
     //   CFactor.OnSelected(currentComponent.componentId); // Changes by me
     //SendID.text = DataControll.instance.myCurrentId;  
     
     
     Debug.Log("My Current Id is : " + DataControll.instance.myCurrentId);
        for (int i = 0; i < competencies.Count; i++)
        {
            if (DataControll.instance.myCurrentId == competencies[i].gameObject.GetComponent<ButtonData>().ListId)
            {
                competencies[i].gameObject.GetComponent<ButtonData>().myStatus = 1;
                Debug.Log("Current id Status : " + competencies[i].gameObject.GetComponent<ButtonData>().myStatus);
            }
        }

     CFactor.OnSelected(DataControll.instance.myCurrentId);

        
#endif
        Debug.Log("My Current Id is : " + DataControll.instance.myCurrentId);
       
    }


    IEnumerator callApiAfter()
    {
        yield return new WaitForSeconds(30);


#if UNITY_WEBGL && !UNITY_EDITOR
components = CFactor.Get<List<ComponentDataObject>>("components"); 

 string debugjson2 = JsonConvert.SerializeObject(components);
#endif
        string debugjson = JsonConvert.SerializeObject(components);
        Debug.Log("after 30 sec: " + debugjson);

    }


}
