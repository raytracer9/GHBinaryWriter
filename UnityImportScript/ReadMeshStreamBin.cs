using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadMeshStreamBin : MonoBehaviour
{
    // Start is called before the first frame update

    string triPathBin = "M:/vertexInt.bin";
    string vertPathBin = "M:/vertexFloat.bin";

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<MeshFilter>().mesh = ConstructMesh(ConvertTriangleToInt(triPathBin), ConvertVertexToV3Array(vertPathBin));
        //Debug.Log(Time.deltaTime);
    }

    int[] ConvertTriangleToInt(string triPath)
    {
        using (FileStream intArrayFile = File.Open(triPath, FileMode.Open, FileAccess.Read, FileShare.Read ))
        {
            using (BinaryReader reader = new BinaryReader(intArrayFile))
            {
                //Debug.Log(intArrayFile.Length.ToString());
                int[] returnArray = new int[intArrayFile.Length/ 4];
                for (int i = 0; i < intArrayFile.Length / 4; i++)
                {
                    returnArray[i] = reader.ReadInt32();
                    //Debug.Log(returnArray[i]);
                }
                return returnArray;
            }
        }
    }

    Vector3[] ConvertVertexToV3Array(string vertPath)
    {
        using (FileStream floatArrayFile = File.Open(vertPath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (BinaryReader reader = new BinaryReader(floatArrayFile))
            {
                //Debug.Log(floatArrayFile.Length.ToString());
                Vector3[] returnArray = new Vector3[floatArrayFile.Length/4/3  ];
                for (int i = 0; i < floatArrayFile.Length/4/3; i++)
                {
                    returnArray[i].x = reader.ReadSingle();
                    returnArray[i].z = reader.ReadSingle();
                    returnArray[i].y = reader.ReadSingle();
                    //Debug.Log(returnArray[i]);
                }
                return returnArray;
            }
        }
    }

    Mesh ConstructMesh(int[] triangles, Vector3[] vertices)
    {
        Mesh genMesh = new Mesh();
        genMesh.vertices = vertices;
        genMesh.triangles = triangles;
        genMesh.RecalculateNormals();
        return genMesh;
    }
}
