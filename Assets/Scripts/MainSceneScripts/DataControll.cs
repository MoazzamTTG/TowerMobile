using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DataControll : MonoBehaviour
{
    public static DataControll instance;


    public TextMeshProUGUI MainTitleFromFirstButton;
    public TextMeshProUGUI MainTitle;
    // public ButtonData buttonData;
    public CompetencyStateSelector stateSelector;
    public GameObject TraggerObj;
    [Header("--LISTS SECTION--")]
    public List<GameObject> LeftBtnsPanel;
    public List<GameObject> RightBtnsPanel;
    [SerializeField]
    List<GameObject> AllChangableBuildings;
    
   public List<GameObject> AllPrefabsBuildings;
    [SerializeField]
  //public List<GameObject> SingleChangableBuildings;
    public List<Sprite> AllBackBuildings;
    public string BackBuildingName;

    [Header("--ANIMATION SECTION--")]
    public Animator carAnim;
    public Animator buildingAnim;
    public Animator[] MiniGame;
   // public List<Animator> backAnimationlist;
   // public List<RuntimeAnimatorController> backAnimatorlist;

    [Header("--GLOBES SECTIONS--")]
   /* [SerializeField]
    GameObject Globe;*/
    [SerializeField]
    GameObject TestingGlobe;
    [Header("--SPRITES SECTION--")]
    public Sprite[] ButtonsMainSprites;
    public Sprite[] ButtonsCompleteSprites;
    public Sprite[] ButtonSelectedSprites;
    [Space]
    //   public SpriteRenderer ChangableSprite;
    // public Sprite StatuscompletedSprite;

    public Animator DefualtAnimator;

    [Space]
    public string myCurrentId;

    public int visibleBtnsNumber;
    public int getBuildingNumber;
    public int BuildingNum;
    public GameObject buildingPlace;
    public GameObject buildingParent;
    [HideInInspector]
    public GameObject spawnBuilding;

    public GameObject playButton;
    public Sprite DefaultImg;

    public string miniGameBackBuildingAnimation;
    public string BtnType;

    public List<string> ComponentsForAnimation = new List<string>();
    public bool ImageDowloading = false;
    public bool playbtnActive=false;

   
   

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }


    }
    void Start()
    {
          playButton.SetActive(false);              
    }
    public void RotateGlobe()
    {
        if(playbtnActive==false)
        {
           // Debug.Log("btn active or not :" + playbtnActive);
            playButton.SetActive(true);
        }
        carAnim.SetTrigger("IsCarMove");

        StartCoroutine(delayForRotation());

        StartCoroutine(StopCarAfterDelay());

        //ChangeBuildingAnimation();
    }

    IEnumerator delayForRotation()
    {
        yield return new WaitForSeconds(.5f);
      //  LeanTween.rotateAround(Globe, Vector3.forward, 180f, 3f);
        LeanTween.rotateAround(TestingGlobe, Vector3.forward, 90f, 1.7f);
    }   
    IEnumerator StopCarAfterDelay()
    {
        for (int i = 0; i < stateSelector.competencies.Count; i++)
        {
            stateSelector.competencies[i].gameObject.GetComponent<Button>().interactable = false;
        }


        playButton.GetComponent<Button>().interactable = false;
        carAnim.SetBool("CarStop", true);
     

        yield return new WaitForSeconds(2.3f);

        for (int j = 0; j < stateSelector.competencies.Count; j++)
        {
           if(stateSelector.competencies[j].gameObject.GetComponent<ButtonData>().myStatus==0)
            {

                stateSelector.competencies[j].gameObject.GetComponent<Button>().interactable = true;
               
            }
            for (int i = 0; i < ButtonSelectedSprites.Length; i++)
            {

            if(stateSelector.competencies[j].gameObject.GetComponent<Image>().sprite == ButtonSelectedSprites[i])
                {
                    stateSelector.competencies[j].gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
        yield return new WaitForSeconds(.3f);
        playButton.GetComponent<Button>().interactable = true;
        TraggerObj.SetActive(true); // trigger object for change building on press buttons
    }


    public void GetbuildingIdOnButtonPressed(int num)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            buildingAnim.SetBool("BUp", false);
            Destroy(spawnBuilding, .38f);

            //  getBuildingNumber = num;
            buildingAnim.SetBool("BDown", true);


            buildingAnim = AllPrefabsBuildings[num].GetComponent<Animator>();
        }
        else
        {
            Debug.Log("Not running on WebGL platform.");
            buildingAnim.SetBool("BUp", false);
            Destroy(spawnBuilding, .38f);

            //  getBuildingNumber = num;
            buildingAnim.SetBool("BDown", true);
        }

        // Check if the current device is a mobile device
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Debug.Log("Running on a mobile device.");
        }
        else
        {
            Debug.Log("Not running on a mobile device.");
        }
#if UNITY_EDITOR

            buildingAnim.SetBool("BUp", false);
            Destroy(spawnBuilding, .38f);

            getBuildingNumber = num;
            buildingAnim.SetBool("BDown", true);


            buildingAnim = AllPrefabsBuildings[num].GetComponent<Animator>();
#endif

    }


}
