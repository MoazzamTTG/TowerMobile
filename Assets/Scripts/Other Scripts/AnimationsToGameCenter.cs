using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class AnimationsToGameCenter : MonoBehaviour
{
    public UnityEvent<string, int, Analytic, bool> nextCallback = new UnityEvent<string, int, Analytic, bool>();

    //[SerializeField]
    //Button prevButton;

    public void Show(SceneDataObject sceneDataObject, bool enablePrevButton)
    {
        //animator.Play("Idle Animation");
      //  prevButton.gameObject.SetActive(enablePrevButton);

        nextCallback.Invoke(sceneDataObject.interactivePanel.targetId, 0, null, false);
    }
}
