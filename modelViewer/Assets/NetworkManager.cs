using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

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

[Serializable]
public class MeshData{
    // public int[][] vertices;
    // public int[][] triangles;

    public List<List<float>> vertices;
    public List<List<int>> triangles;
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
        StartCoroutine(GetRequest("http://localhost:80", (UnityWebRequest req) =>
        {
            if (req.isNetworkError || req.isHttpError)
            {
                Debug.Log($"{req.error}: {req.downloadHandler.text}");
            } else
            {
                Debug.Log(req.downloadHandler.text);
                MeshData meshdata = JsonUtility.FromJson<MeshData>(req.downloadHandler.text);
                Debug.Log(meshdata.triangles);
                // GeneratePlane(meshdata.data);
            }
        }));

        // StartCoroutine(DownloadFile());
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

    Vector3[] ParseVertices(List<List<float>> vertices){
        Vector3[] res = new Vector3[vertices.Count];

        for (int i = 0; i < vertices.Count; i++)
        {
            res[i] = new Vector3(vertices[i][0],vertices[i][1],vertices[i][2]);
        }

        return res;
    }

    void GeneratePlane(MeshData data)
    {
        Mesh mesh = new Mesh();

        // Debug.Log(data.vertices.Length);
        Vector3[] vertices =  ParseVertices(data.vertices);

        int[] triangles = new int[]
        {
            0, 2, 1, // triangle 1 (bottom-right-top-right)
            0, 3, 2  // triangle 2 (bottom-left-top-right)
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GameObject plane = new GameObject("My Plane");
        MeshFilter meshFilter = plane.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        plane.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        // Optionally, add a MeshRenderer component to the GameObject
        plane.AddComponent<MeshRenderer>();
    }
}