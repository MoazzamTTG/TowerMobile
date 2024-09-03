using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ReferenceLoader : MonoBehaviour
{
  //  public AssetReference[] textureReferences;
    

    private void Start()
    {
       // LoadTextures();
    }

    //private async void LoadTextures()
    //{
    //    //foreach (var reference in textureReferences)
    //    //{
    //    //    AsyncOperationHandle<Texture2D> handle = Addressables.LoadAssetAsync<Texture2D>(reference);
    //    //    await handle.Task;

    //    //    if (handle.Status == AsyncOperationStatus.Succeeded)
    //    //    {
    //    //        Texture2D texture = handle.Result;
    //    //        Use the loaded texture in your scene
    //    //    }
    //    //    else
    //    //    {
    //    //        Debug.Log("not load");
    //    //    }
    //    //}


    //}
}
