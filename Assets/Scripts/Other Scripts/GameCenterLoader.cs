using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System;
//using System.Runtime.InteropServices;
public class GameCenterLoader : MonoBehaviour
{

    //------------------------ New Json Work-----------------------
    public string jsonStr;

    static SJTDataObject SJTDataObject;
    // Import the emscripten_asm_const_int function from the Emscripten runtime
    //[DllImport("__Internal")]
    //private static extern int emscripten_asm_const_int(string code);

    Stack<int> prevIndex = new Stack<int>();
    int currentIndex = 0;

    string nextTargetId;

    [SerializeField]
    AnimationsToGameCenter animationPanel;

    [SerializeField]
    RawImage background;

    [SerializeField]
    Texture2D testTexture;
    public enum PanelType { NONE, MESSAGE, OPTIONS, ANIMATION };

    ////PanelType prevPanelType;


    [Header("For Animation")]
    public float frameRate = 10f;

    static Dictionary<string, Texture2D> frames = new Dictionary<string, Texture2D>();
    //private int currentFrameIndex;
    private float timeSinceLastFrame;
    bool animationFramesFinished = false;

    private List<Texture2D> animationFrames;


    public Dictionary<string, SpriteRenderer> animationSpriteRenderers = new Dictionary<string, SpriteRenderer>();


    // Dictionary to store animation frames for each URL
    public Dictionary<string, List<Texture2D>> animationFramesMap = new Dictionary<string, List<Texture2D>>();

    // Dictionary to store the current frame index for each URL
    private Dictionary<string, int> animationIndices = new Dictionary<string, int>();

    [HideInInspector]
    public bool isPlaying = false;

    // Create dictionaries to map animation URLs to sprite renderers
    public Dictionary<string, SpriteRenderer> spriteRendererMap;
    public List<string> Getname;

    public string activeAnimationURL;



    private void Awake()
    {
//        currentFrameIndex = 0;
//        animationFrames = new List<Texture2D>();


//#if UNITY_WEBGL && !UNITY_EDITOR
        
//        var dataURL = CFactor.Get<string>("dataURL");
//        Debug.Log("web get url for json : " + dataURL);    
//        StartCoroutine(InitDataURL(dataURL));
//#else
//        SJTDataObject = JsonConvert.DeserializeObject<SJTDataObject>(jsonStr);


//        Debug.Log("My Json : " + jsonStr);

//        //StartCoroutine(DownloadImages());


//        spriteRendererMap = new Dictionary<string, SpriteRenderer>();


//#endif
    }


    void Update()
    {

        if (isPlaying)
        {
            UpdateAnimation();
        }
    }

    void Setup()
    {
        Play();
    }

    void Play()
    {
        string panelType = SJTDataObject.data[currentIndex].interactivePanel.type;

        animationPanel.gameObject.SetActive(false);
        if (panelType == "animations")
        {

            animationPanel.gameObject.SetActive(true);
            StartCoroutine(DownloadImages());

            //prevPanelType = PanelType.ANIMATION;
            prevIndex.Clear();

        }
    }
    IEnumerator InitDataURL(string url)
    {
        using UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        Debug.Log("get url for json : " + url);
        if (www.result == UnityWebRequest.Result.Success)
        {
            var jsonStr = www.downloadHandler.text;
            if (jsonStr != string.Empty)
                SJTDataObject = JsonConvert.DeserializeObject<SJTDataObject>(jsonStr);


            //Setup();
            Debug.Log("Get JSon : " + SJTDataObject);
            //StartCoroutine(DownloadImages());
        }
        else
        {
            Debug.Log("Error downloading " + url);
            Debug.Log("Json not found");
        }
    }
    public void StartAnimation()
    {
        //isPlaying = true;
    }


    public IEnumerator DownloadImages()
    {
        if (SJTDataObject.data[currentIndex].interactivePanel.type == "animations")
        {
            Debug.Log("inside download images if condition");

            foreach (AnimationDataObject animations in SJTDataObject.animations)
            {
                var folderURL = GetFolderURL();
                //Debug.Log("animation name : " + animations.id);


                //UnityWebRequest www = UnityWebRequestTexture.GetTexture(folderURL + animations.url);

                //animationFramesMap[animations.url] = new List<Texture2D>();
                //animationIndices[animations.url] = 0;

                //UnityWebRequestAsyncOperation requestOperation = www.SendWebRequest();

                //while (!requestOperation.isDone)
                //{
                //    yield return null;
                //}
                Debug.Log("for each condition");

                for (int i = 0; i < DataControll.instance.ComponentsForAnimation.Count; i++)
                {
                    if (animations.id == DataControll.instance.ComponentsForAnimation[i])
                    {
                        Debug.Log("inside neew if conditon");
                        Debug.Log("animation name : " + animations.id);


                        UnityWebRequest www = UnityWebRequestTexture.GetTexture(folderURL + animations.url);

                        animationFramesMap[animations.url] = new List<Texture2D>();
                        animationIndices[animations.url] = 0;

                        UnityWebRequestAsyncOperation requestOperation = www.SendWebRequest();

                        while (!requestOperation.isDone)
                        {
                            yield return null;
                        }

                        if (www.result == UnityWebRequest.Result.Success)
                        {
                            Debug.Log("images result success");

                            Texture2D texture = DownloadHandlerTexture.GetContent(www);
                            frames[animations.url] = texture;
                            Getname.Add(animations.id);
                            // Create a dictionary to map URLs to actions
                            Dictionary<string, Action> urlToAction = new Dictionary<string, Action>
                {
                    { "handemright.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "flipflop.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "boxsort.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "makeway.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "lightsout.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "colormatch.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "binder.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "moneysort.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "moneycount.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "numberrush.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "parkit.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "amnesia.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "cypher.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) },
                    { "jumpingjak.png", () => SliceSpritesheet(animations.url, animations.numRows, animations.numColumns) }
                };

                            // Check if the URL is in the dictionary and execute the corresponding action
                            if (urlToAction.TryGetValue(animations.url, out Action action))
                            {
                                action.Invoke();
                            }
                            else
                            {
                                Debug.LogWarning("No action defined for URL: " + animations.url);
                            }
                        }
                        else
                        {
                            Debug.LogError("Error downloading " + animations.url);
                        }

                        // Start playing the animation for the corresponding SpriteRenderer
                        //currentFrameIndex = 0;
                        timeSinceLastFrame = 0f;

                        animationPanel.gameObject.SetActive(true);
                        animationPanel.Show(SJTDataObject.data[currentIndex], false);
                        Debug.Log("unload Unused assets");

                        // After assets are downloaded and processed
                        Resources.UnloadUnusedAssets();
                        Debug.Log("before start animation call");

                        StartAnimation(); // Start playing the animations after downloading
                    }
                }
            }



        }
    }


    void SliceSpritesheet(string animationsId, int numRows, int numColumns)
    {
        int spriteWidth = frames[animationsId].width / numColumns;
        int spriteHeight = frames[animationsId].height / numRows;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numColumns; col++)
            {
                Texture2D spriteTexture = new Texture2D(spriteWidth, spriteHeight, TextureFormat.RGBA32, false);
                spriteTexture.filterMode = FilterMode.Point;
                spriteTexture.wrapMode = TextureWrapMode.Clamp;
                spriteTexture.anisoLevel = 0;

                spriteTexture.SetPixels(frames[animationsId].GetPixels(
                    col * spriteWidth,
                    row * spriteHeight,
                    spriteWidth,
                    spriteHeight));
                spriteTexture.Apply();
                animationFrames.Add(spriteTexture);

                // Store the animation frames in the map
                if (!animationFramesMap.ContainsKey(animationsId))
                {
                    animationFramesMap[animationsId] = new List<Texture2D>();
                }
                animationFramesMap[animationsId].Add(spriteTexture);

                // Initialize the animation index if it doesn't exist
                if (!animationIndices.ContainsKey(animationsId))
                {
                    animationIndices[animationsId] = 0;
                }

            }
        }
    }

    void UpdateAnimation()
    {
        if (!animationFramesFinished)
        {
            timeSinceLastFrame += Time.deltaTime;
            float frameTime = 1f / frameRate;

            if (timeSinceLastFrame >= frameTime)
            {
                timeSinceLastFrame -= frameTime;

                // Check if the current animationURL matches the active animation URL
                if (animationFramesMap.TryGetValue(activeAnimationURL, out List<Texture2D> animationFrames))
                {
                    if (animationFrames.Count > 0)
                    {
                        // Retrieve the current frame index from the dictionary
                        int currentFrameIndex = animationIndices[activeAnimationURL];

                        // Ensure the current frame index is within bounds
                        if (currentFrameIndex >= 0 && currentFrameIndex < animationFrames.Count)
                        {
                            testTexture = animationFrames[currentFrameIndex];
                            Rect rect = new Rect(0, 0, testTexture.width, testTexture.height);

                            // Determine the sprite renderer based on the animation URL
                            if (spriteRendererMap.TryGetValue(activeAnimationURL, out SpriteRenderer spriteRenderer))
                            {
                                spriteRenderer.sprite = Sprite.Create(testTexture, rect, new Vector2(0.5f, 0.5f), 100, 1, SpriteMeshType.FullRect);
                            }

                            // Increment the frame index and loop back to 0 if it reaches the end
                            currentFrameIndex = (currentFrameIndex + 1) % animationFrames.Count;

                            // Store the updated current frame index back in the dictionary
                            animationIndices[activeAnimationURL] = currentFrameIndex;
                        }
                        else
                        {
                            Debug.LogWarning($"Invalid current frame index for animation: {activeAnimationURL}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning($"No frames found for animation: {activeAnimationURL}");
                    }
                }
            }
        }
    }

    // Public method to set the active animation URL
    public void SetActiveAnimation(string animationURL)
    {
        // Set the active animation URL here
        activeAnimationURL = animationURL;
    }


    static string GetFolderURL()
    {
        string folderURL = "http://localhost:8080/assets/images/ui/animations/";

#if UNITY_WEBGL && !UNITY_EDITOR
    folderURL = CFactor.Get<string>("folderURL");
     Debug.Log("My Folder : " + folderURL);
#endif
        return folderURL;
    }
}
