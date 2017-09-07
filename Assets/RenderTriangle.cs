using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTriangle : MonoBehaviour {

    public float angle = 1;
    public Vector3 point;
    public static Material material;
    public static Mesh mesh;
    Vector3[] triangleVertecies = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0),};

    // Use this for initialization
    void Start () {
        // Add a MeshFilter and MeshRenderer to the Empty GameObject
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        // Get the Mesh from the MeshFilter
        mesh = GetComponent<MeshFilter>().mesh;

        // Set the material to the material we have selected
        GetComponent<MeshRenderer>().material = material;

        // Clear all vertex and index data from the mesh
        mesh.Clear();

        // Create a triangle with points at (0, 0, 0), (0, 1, 0) and(1, 1, 0)
        mesh.vertices = triangleVertecies;

        // Set the colour of the triangle
        mesh.colors = new Color[] { new Color(0.8f, 0.3f, 0.3f, 1.0f), new Color(0.8f, 0.3f, 0.3f, 1.0f), new Color(0.8f, 0.3f, 0.3f, 1.0f)};

        // Set vertex indicies
        mesh.triangles = new int[] { 0, 1, 2};
        }
	
	// Update is called once per frame
	void Update () {
        mesh.vertices = IGB283Transform.translate(mesh.vertices, point);
        
        mesh.vertices = IGB283Transform.rotate(mesh.vertices, angle);
        
        mesh.RecalculateBounds();
    }
}
