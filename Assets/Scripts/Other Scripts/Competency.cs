using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Competency : MonoBehaviour
{
  
    [SerializeField]
  
    public CompetencyStateSelector competencyState;
    public ButtonData buttonData;

    public void setStatus(bool complete)
    {
        if (complete)
        {
           
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<ButtonData>().myStatus = 1;
           
        }
        else
        {
            //car.SetActive(false);
           // gameObject.SetActive(false);
           // gameObject.SetActive(true);
        }
    }

    public void makeThisAsCurrentCompetency(Transform marker)
    {
        gameObject.SetActive(true);
      
    }
}
