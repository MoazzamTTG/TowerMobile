using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFactDataCollector : MonoBehaviour
{
   public  SJTDataObject dataObject = new SJTDataObject();
    private void Start()
    {
       // Debug.Log("Show Data : "+dataObject);
    }
}

[SerializeField]
public class SJTDataObject
{
    public List<ImageDataObject> images = new List<ImageDataObject>();
  //  public List<SoundDataObject> sounds = new List<SoundDataObject>();
    public List<SceneDataObject> data = new List<SceneDataObject>();
    public List<AnimationDataObject> animations = new List<AnimationDataObject>();
   
}
[SerializeField]
public class AnimationDataObject
{
    public string id = "";
    public string url = "";
    public int numColumns; // Number of columns in the spritesheet
    public int numRows;    // Number of rows in the spritesheet
    public int rowIndex;   // Row index of the sprite in the spritesheet
    public int columnIndex; // Column index of the sprite in the spritesheet
}


[SerializeField]
public class ImageDataObject
{
    public string id = "";
    public string url = "";
}
[SerializeField]
public class SceneDataObject
{
    public string id = "";
    public string title = "";
    public string imageId = "";
    public string imageIdHorizontal = "";
    public int timer = 0;
    public string type = null;
    public string aptitudeImage = null;
    public InteractivePanelDataObject interactivePanel = new InteractivePanelDataObject();

}

public class InteractivePanelDataObject
{
    public string type = "";
  //  public List<OptionDataObject> options = new List<OptionDataObject>();
    public string title = null;
    public string text = null;
    public string imageId = null;
    public string targetId = "";
    public string audioId = "";
}
public class Analytic
{
    public string questionId { get; set; }
    public string questionText { get; set; }
    public string selectedOption { get; set; }
    public string selectedOption2 { get; set; }
    public int timeToAnswer { get; set; }
    public int totalTime { get; set; }
    public int score { get; set; }
    public string next_targetId { get; set; }
}   


