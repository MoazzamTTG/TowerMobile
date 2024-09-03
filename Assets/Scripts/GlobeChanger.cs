using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GlobeChanger : MonoBehaviour
{
    // public List<Sprite> GlobeImages;

    public SpriteRenderer WolrdImage;
    [SerializeField]
    Animator BackbuildingAnim;

    [SerializeField]
    GameCenterLoader SpriteRendererDictonary;

    bool animPlay;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "ChangableGlobe")
        {

            for (int i = 0; i < DataControll.instance.AllBackBuildings.Count; i++)
            {
                if (DataControll.instance.BackBuildingName == DataControll.instance.AllBackBuildings[i].name)
                {
                    WolrdImage.sprite = DataControll.instance.AllBackBuildings[i];
                    Debug.Log("World image :" + WolrdImage.sprite.name + ".png");
#if UNITY_EDITOR
                    // Only for unity editor

                    ////////////////  Change mini game building animation
                    if (DataControll.instance.BtnType == "Minigame" || DataControll.instance.BtnType == "UnityMinigame")
                    {
                        Debug.Log("Before clear render dictonary ");

                        for (int j = 0; j < DataControll.instance.MiniGame.Length; j++)
                        {
                            Debug.Log("animator name : " + DataControll.instance.MiniGame[j].name);

                            if (DataControll.instance.BackBuildingName == DataControll.instance.MiniGame[j].name)
                            {
                                // Debug.Log("WolrdImage name "+ WolrdImage.sprite.name +" " +"miniGame "+ DataControll.instance.MiniGame[j].GetComponent<Animator>().name);

                                Animator animator = DataControll.instance.MiniGame[j].GetComponent<Animator>();
                                Debug.Log("animator name : " + animator.name);
                                WolrdImage.gameObject.GetComponent<Animator>().runtimeAnimatorController = animator.runtimeAnimatorController;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Not running on WebGL platform.");
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



#endif

                    // For webgl Work
                    if (Application.platform == RuntimePlatform.WebGLPlayer)
                    {
                        Debug.Log("Running on a WebGL platform.");
                        ////////////////  Change mini game building animation
                        if (DataControll.instance.BtnType == "Minigame" || DataControll.instance.BtnType == "UnityMinigame")
                        {
                            Debug.Log("Before clear render dictonary ");

                            for (int j = 0; j < DataControll.instance.MiniGame.Length; j++)
                            {
                                Debug.Log("animator name : " + DataControll.instance.MiniGame[j].name);

                                if (DataControll.instance.BackBuildingName == DataControll.instance.MiniGame[j].name)
                                {
                                    // Debug.Log("WolrdImage name "+ WolrdImage.sprite.name +" " +"miniGame "+ DataControll.instance.MiniGame[j].GetComponent<Animator>().name);

                                    Animator animator = DataControll.instance.MiniGame[j].GetComponent<Animator>();
                                    Debug.Log("animator name : " + animator.name);
                                    WolrdImage.gameObject.GetComponent<Animator>().runtimeAnimatorController = animator.runtimeAnimatorController;
                                }
                            }
                        }
                        else
                        {
                            Debug.Log("Not running on WebGL platform.");
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

                    }
                    else
                    {
                        SpriteRendererDictonary.isPlaying = false;
                        
                    }
                }
            }
        }
        if (other.gameObject.tag == "AnimatorOff")
        {
            WolrdImage.gameObject.GetComponent<Animator>().runtimeAnimatorController = DataControll.instance.DefualtAnimator.runtimeAnimatorController;
            WolrdImage.sprite = DataControll.instance.DefaultImg;

        }
    }

    private IEnumerator AddSpritesCoroutine(string activeAnimationURL, List<string> getnameCopy)
    {
        bool spriteAdded = false;  // Track if at least one sprite was added
        //Debug.Log("add sprite method ");
        for (int j = 0; j < getnameCopy.Count; j++)
        {
            if (DataControll.instance.BackBuildingName + ".png" == getnameCopy[j] + ".png")
            {
                // Debug.Log("is added " + DataControll.instance.BackBuildingName);
                Debug.Log("Getname " + getnameCopy[j]);
                SpriteRendererDictonary.isPlaying = true;
                SpriteRendererDictonary.spriteRendererMap.Add(getnameCopy[j] + ".png", WolrdImage);
                SpriteRendererDictonary.animationSpriteRenderers.Add(getnameCopy[j] + ".png", WolrdImage);
                // Debug.Log("after RenderDictonary Count is : " + SpriteRendererDictonary.spriteRendererMap.Count);
                SpriteRendererDictonary.SetActiveAnimation(activeAnimationURL);

                spriteAdded = true;  // Set this flag to true when a sprite is added
            }

            // Yielding here allows the frame to be rendered before continuing the loop
            yield return null;
        }

        if (!spriteAdded)
        {
            // Handle the case where no sprite was added
            // Debug.LogWarning("No sprite was added.");
        }
    }


}
