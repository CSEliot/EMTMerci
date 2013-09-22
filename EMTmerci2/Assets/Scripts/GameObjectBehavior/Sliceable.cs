using UnityEngine;
using System.Collections;

public class Sliceable : MonoBehaviour {

    public Transform slicePrefab;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    public void SliceMesh(Vector3 sliceStart, Vector3 sliceEnd)
    {
        Vector3 slicePlusZ = new Vector3(sliceEnd.x, sliceEnd.y, sliceEnd.z + 1000);
        Mesh initialMesh = GetComponent<MeshFilter>().mesh;

        Vector3[] vertices = initialMesh.vertices;

        Transform clone = (Transform)Instantiate(transform, transform.position + (new Vector3(.5f, 0, 0)), Quaternion.identity);
        Mesh cloneMesh = clone.GetComponent<MeshFilter>().mesh;
        Vector3[] cloneVertices = cloneMesh.vertices;



        Plane slicePlane = new Plane(sliceStart, sliceEnd, slicePlusZ);
        
        

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = transform.TransformPoint(vertices[i]);
            Vector3 cloneVertex = transform.TransformPoint(cloneVertices[i]);

            if (slicePlane.GetSide(vertex))
            {
                vertices[i] = transform.InverseTransformPoint(new Vector3(vertex.x, vertex.y - (slicePlane.GetDistanceToPoint(vertex)), vertex.z));
                cloneVertices[i] = transform.InverseTransformPoint(new Vector3(vertex.x, vertex.y, vertex.z));
            }
            else
            {
                cloneVertices[i] = transform.InverseTransformPoint(new Vector3(vertex.x, vertex.y - (slicePlane.GetDistanceToPoint(vertex)), vertex.z));
            }
        }

        initialMesh.vertices = vertices;
        initialMesh.RecalculateBounds();

        cloneMesh.vertices = cloneVertices;
        cloneMesh.RecalculateBounds();
        
    }
}
