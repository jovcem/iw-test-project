using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Post
{
    public int userId;
    public int id;
    public string title;
    public string body;
}

[Serializable]
public class Posts
{
    public Post[] posts;
}

public class NetworkManager : MonoBehaviour
{
    IEnumerator GetRequest(string url, Action<UnityWebRequest> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Send the request and wait for a response
            yield return request.SendWebRequest();
            callback(request);
        }
    }

    public void GetPosts()
    {
        // StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/posts", (UnityWebRequest req) =>
        // {
        //     if (req.isNetworkError || req.isHttpError)
        //     {
        //         Debug.Log($"{req.error}: {req.downloadHandler.text}");
        //     } else
        //     {
        //         Debug.Log(req.downloadHandler.text);
        //         Posts posts = new Posts();
        //         posts = JsonUtility.FromJson<Posts>( "{\"posts\":" + req.downloadHandler.text + "}" );

                
        //         foreach(Post post in posts.posts)
        //         {
        //             Debug.Log(post.title);
        //         }

        //         // 
        //         string savePath = @"C:\\Users\\jovce\\Documents\\todo\\file.png"
        //         // var uwr = new UnityWebRequest(url);
        //         // uwr.method = UnityWebRequest.kHttpVerbGET;
        //         var dh = new DownloadHandlerFile(savePath);
        //         dh.removeFileOnAbort = true;
        //         uwr.downloadHandler = dh;
        //         yield return uwr.SendWebRequest();
        //     }
        // }));

        StartCoroutine(DownloadFile());
    }

    IEnumerator DownloadFile()
    {

        string url= "http://localhost:8000/static/zebra.jpg";

        var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbGET);
        string savePath = @"C:\\Users\\jovce\\Documents\\file.jpg";

        uwr.downloadHandler = new DownloadHandlerFile(savePath);
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success){
            Debug.LogError(uwr.error);
        }else{
            Debug.Log("File successfully downloaded and saved to " + savePath);

            // LoadFBXFromFile(@"sample");
            LoadModel();
        }

    }

    void LoadFBXFromFile(string filePath)
    {
        // Load the FBX file from the specified path
        GameObject fbxPrefab = Resources.Load<GameObject>(filePath);

        if (fbxPrefab == null)
        {
            Debug.LogError("Failed to load FBX file from path: " + filePath);
            return;
        }

        // Instantiate the loaded FBX as a GameObject in the scene
        GameObject fbxInstance = Instantiate(fbxPrefab, Vector3.zero, Quaternion.identity);

        // Optional: Adjust position, rotation, and scale of the instantiated FBX
        // fbxInstance.transform.position = newPosition;
        // fbxInstance.transform.rotation = newRotation;
        // fbxInstance.transform.localScale = newScale;
    }

    public string modelPath = "Assets/models/sample.fbx"; // Path to the model prefab

    void LoadModel()
    {
#if UNITY_EDITOR
        // Load the model prefab from the specified path
        GameObject modelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(modelPath);

        if (modelPrefab == null)
        {
            Debug.LogError("Failed to load model from path: " + modelPath);
            return;
        }

        // Instantiate the loaded model as a GameObject in the scene
        GameObject modelInstance = Instantiate(modelPrefab, Vector3.zero, Quaternion.identity);
#else
        Debug.LogError("AssetDatabase.LoadAssetAtPath() can only be used in Editor mode.");
#endif
    }
}