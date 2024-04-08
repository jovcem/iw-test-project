using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Mesh;

public class TerrainGenerator : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 10;
    public int zSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    private void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        string vertsStr = "";

        int i = 0;
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, 0, z);
                vertsStr += $"{vertices[i][0]} {vertices[i][1]} {vertices[i][2]} \n";
                i++; 
            }
        }

        triangles = new int[xSize * zSize * 6];
        string trisStr = "";

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        { 
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                trisStr += $"{triangles[tris + 0]}/{triangles[tris + 1]}/{triangles[tris + 2]} ";
                trisStr += $"{triangles[tris + 3]}/{triangles[tris + 4]}/{triangles[tris + 5]} ";
                vert++;
                tris += 6;
            }
            vert++;
        }

        string path = @"C:\\Users\\jovce\\Documents\\code\\iw-test-project\\api\\model2.obj";
        File.WriteAllText(path, vertsStr + trisStr, Encoding.UTF8);
    }
 

    // Update is called once per frame
    void Update()
    {

    }
}
