using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrow : MonoBehaviour
{

    private void OnEnable()
    {
        ChangeBuildingAnimation();
    }
    private void OnDisable()
    {
       // Buildingdown();
    }
    public void ChangeBuildingAnimation()
    {
        LeanTween.scaleY(this.gameObject, 1f, .3f);
        //LeanTween.moveLocalY(ChangableBuilding, -0.88f, 1f).setDelay(.5f);

    } 
    
   
}
