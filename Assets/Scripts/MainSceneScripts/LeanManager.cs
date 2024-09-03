using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanManager : MonoBehaviour
{
    public static LeanManager instance;
    public GameObject SingleBuilding;

    [SerializeField]
    GameObject ChangableBuilding;

    ////[SerializeField]
    ////DataControll dataControll;

    public GameObject PlayBtn;
    public GameObject TextLogo;
    public GameObject Sun;


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

        LeanTween.moveLocalY(TextLogo, -80f, 1f).setEaseInOutBounce();

        StartCoroutine(waitForSun());
    }

    IEnumerator waitForSun()
    {
        yield return new WaitForSeconds(2.2f);
        LeanTween.scale(Sun, new Vector2(0.08f, .08f), 2.5f).setLoopPingPong();

    }
    public void MoveableObj(GameObject moveObj)
    {
        LeanTween.moveLocalY(moveObj, 30f, .7f).setEaseInOutBounce();
    }



    public void ChangeBuildingAnimation(GameObject SingleBuilding)
    {
        LeanTween.scaleY(SingleBuilding, 1f, 1f).setDelay(.5f);


    }

    public void ChangeBuildingAnimationOut(int bObj)
    {

        //for (int i = 0; i < DataControll.instance.SingleChangableBuildings.Count; i++)
        //{
        //    DataControll.instance.SingleChangableBuildings[i].SetActive(false);
        //    //Debug.Log("new building down " + i);
        //}

    }

}
