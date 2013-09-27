using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sliceable : MonoBehaviour {

    public Transform slicePrefab;
	public Transform bloodPrefab;
	
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
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        Transform clone = (Transform)Instantiate(transform, transform.position, Quaternion.identity);
        transform.position -= transform.InverseTransformPoint(transform.TransformPoint(new Vector3(.1f, 0, 0)));
        clone.rotation = transform.rotation;
        Mesh cloneMesh = clone.GetComponent<MeshFilter>().mesh;
        Vector3[] cloneVertices = cloneMesh.vertices;

        Vector3 armSpaceStart = transform.InverseTransformPoint(sliceStart);
        Vector3 armSpaceEnd = transform.InverseTransformPoint(sliceEnd);

        print("ARM START " + armSpaceStart.x + ", " + armSpaceStart.y + ", " + armSpaceStart.z);
        print("ARM END " + armSpaceEnd.x + ", " + armSpaceEnd.y + ", " + armSpaceEnd.z);

        Vector3 p1 = transform.TransformPoint(new Vector3(armSpaceStart.x, transform.collider.bounds.extents.y, transform.collider.bounds.extents.z));
        Vector3 p2 = transform.TransformPoint(new Vector3(armSpaceEnd.x, -transform.collider.bounds.extents.y, transform.collider.bounds.extents.z));
        Vector3 p3 = transform.TransformPoint(new Vector3(armSpaceEnd.x, -transform.collider.bounds.extents.y, -transform.collider.bounds.extents.z));

        print("P1 " + p1);
        print("P2 " + p2);
        print("P3 " + p3);

        Plane slicePlane = new Plane(p1, p2, p3);

        Vector3 planeCenter = new Vector3((p1.x + p3.x) / 2, (p1.y + p3.y) / 2, (p1.z + p3.z) / 2);
        print("CENTER OF PLANE " + planeCenter);
		

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            Vector3 cloneVertex = cloneVertices[i];

            if (slicePlane.GetSide(vertex))
            {
                vertices[i] = new Vector3(vertex.x - (slicePlane.GetDistanceToPoint(vertex)), 0, 0);
            }
            else
            {
                cloneVertices[i] = new Vector3(vertex.x - (slicePlane.GetDistanceToPoint(vertex)), 0, 0);
            }
        }

        mesh.vertices = vertices;
        mesh.RecalculateBounds();

        cloneMesh.vertices = cloneVertices;
        cloneMesh.RecalculateBounds();

        BoxCollider bc = GetComponent<BoxCollider>();
        bc.size = new Vector3(mesh.bounds.size.x * 0.8f, mesh.bounds.size.y * 0.8f, mesh.bounds.size.z * 0.8f); // reset size, bit smaller than real
        bc.center = mesh.bounds.center;


        BoxCollider cloneBc = clone.GetComponent<BoxCollider>();
        cloneBc.size = new Vector3(cloneMesh.bounds.size.x * 0.8f, cloneMesh.bounds.size.y * 0.8f, cloneMesh.bounds.size.z * 0.8f);
        cloneBc.center = cloneMesh.bounds.center;
		cloneBc.rigidbody.useGravity = true;
		cloneBc.isTrigger = false;
    }
}
