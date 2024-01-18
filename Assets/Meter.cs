using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Meter : MonoBehaviour
{
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;
    Vector3[] normals;

    public GameObject sideWall;
    public GameObject slantWall;

    public float width;
    public float height;
    public float leftOffset;
    public float flip;

    public float maxVal;
    private float _value;
    void Start()
    {
        /*
        //Local points

        Vector3 sideWallTop = Vector3.Scale(sideWall.transform.localScale, new Vector3(-1f, 1f, 1f));
        Vector3 sideWallBottom = Vector3.Scale(sideWall.transform.localScale, new Vector3(-1f, 1f, -1f));
        Vector3 slantWallBottom = Vector3.Scale(slantWall.transform.localScale, new Vector3(-1f, 1f, -1f));

        //World points
        sideWallTop = sideWall.transform.TransformPoint(sideWallTop);
        sideWallBottom = sideWall.transform.TransformPoint(sideWallBottom);
        slantWallBottom = slantWall.transform.TransformPoint(slantWallBottom);
        */

        Vector3 sideWallTop = sideWall.transform.TransformPoint(flip * 0.5f, 0.52f, height - 0.5f);
        Vector3 sideWallBottom = sideWall.transform.TransformPoint(flip * 0.5f, 0.52f, -0.5f);
        Vector3 slantWallBottom = slantWall.transform.TransformPoint(flip * 0.5f, 0.52f, -0.5f);

        RaycastHit[] hits = Physics.RaycastAll(slantWall.transform.TransformPoint(flip * 0.5f, 0.49f, -0.5f), slantWall.transform.forward);
        Collider sideWallCollider = sideWall.GetComponent<Collider>();
        Vector3 intersectionPoint = Vector3.zero;
        bool intersects = false;
        foreach(RaycastHit hit in hits)
        {
            if(Collider.ReferenceEquals(hit.collider, sideWallCollider))
            {
                intersectionPoint = new Vector3(hit.point.x, slantWallBottom.y, hit.point.z);
                intersects = true;
            }
        }
        print(hits.Length);
        if (!intersects) throw new System.ArgumentException("Meter does not intersect with side wall", gameObject.name);
        newVertices = new Vector3[]
        {
            sideWallTop - flip * leftOffset * sideWall.transform.right,
            sideWallTop - flip * (width + leftOffset) * sideWall.transform.right,
            intersectionPoint - flip * leftOffset * sideWall.transform.right,
            sideWallBottom - flip * (width + leftOffset) * sideWall.transform.right,
            slantWallBottom - flip * leftOffset * sideWall.transform.right,
            slantWallBottom - flip * (width + leftOffset) * slantWall.transform.right
        };

        if(flip > 0.0f)
        {
            newTriangles = new int[]
            {
            1, 0, 2,
            2, 3, 1,
            3, 2, 4,
            4, 5, 3
            };
        }
        else
        {
            newTriangles = new int[]
{
            2, 0, 1,
            1, 3, 2,
            4, 2, 3,
            3, 5, 4
};
        }

        newUV = new Vector2[]
        {
            new Vector2(1f, 1f),
            new Vector2(0f, 1f),
            new Vector2(1f, 0.5f),
            new Vector2(0f, 0.5f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)
        };

        normals = new Vector3[]
        {
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 1f, 0f),
            new Vector3(0f, 1f, 0f)
        };
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = newVertices;
        mesh.uv = newUV;
        mesh.triangles = newTriangles;
        mesh.normals = normals;
    }

    public float Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = Mathf.Max(Mathf.Min(value, maxVal), 0);
            //Yep, as far as I'm aware Unity makes you do this.
            const string fillPropertyName = "Vector1_a6e3367f7e0141b48af9b737367b4a3d";
            GetComponent<Renderer>().material.SetFloat(fillPropertyName, value / maxVal);
        }
    }
}
