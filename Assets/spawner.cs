using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class spawner : MonoBehaviour
{
    public class ObjData
    {
        public Vector3 pos;
        public Quaternion rot;
        public Vector3 scale;

        public Matrix4x4 matrix
        {
            get
            {
                return Matrix4x4.TRS(pos, rot, scale);
            }
        }

        public ObjData(Vector3 _pos, Vector3 _scale, Quaternion _rot)
        {
            pos = _pos;
            scale = _scale;
            rot = _rot;
        }
    }


    public int instanceNum;

    public Vector3 maxPos;
    public Mesh objMesh;
    public Material objMat;

    private List<List<ObjData>> batches = new List<List<ObjData>>();

    void Start()
    {
        int batchIndexNum = 0;
        List<ObjData> currBatch = new List<ObjData>();

        for (int i = 0; i < instanceNum; i++)
        {
            AddObj(currBatch, i);
            batchIndexNum++;
            if (batchIndexNum >= 1000)
            {
                batches.Add(currBatch);
                currBatch = BuildNewBatch();
                batchIndexNum = 0;
            }
        }
    }

    void AddObj(List<ObjData> _currBatch, int _i)
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-maxPos.x, maxPos.x),
                                                            UnityEngine.Random.Range(-maxPos.y, maxPos.y),
                                                            UnityEngine.Random.Range(-maxPos.z, maxPos.z)
                                                            );
        _currBatch.Add(new ObjData(position, new Vector3(2, 2, 2), Quaternion.identity));
    }

    private List<ObjData> BuildNewBatch()
    {
        return new List<ObjData>();
    }

    void Update()
    {
        RenderBatch();
    }

    private void RenderBatch()
    {
        foreach (var batch in batches)
        {
            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.matrix).ToList());
        }
    }

}
