using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTriangle : MonoBehaviour {

    public float angle = 1;

    public GameObject point1;
    public GameObject point2;

    public Vector3 scaleVector;
    private Vector3 currentScaleVector;

    public Vector3 point;
    public static Material material;
    public static Mesh mesh;
    Vector3[] triangleVertecies = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0), };

    // Use this for initialization
    void Start() {
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
        mesh.colors = new Color[] { new Color(0.8f, 0.3f, 0.3f, 1.0f), new Color(0.8f, 0.3f, 0.3f, 1.0f), new Color(0.8f, 0.3f, 0.3f, 1.0f) };

        // Set vertex indicies
        mesh.triangles = new int[] { 0, 1, 2 };

        scaleVector = new Vector3(1f, 1f, 1f);
        currentScaleVector = scaleVector;
    }

    // Update is called once per frame
    void Update() {

        mesh.vertices = IGB283Transform.translate(mesh.vertices, point);

        mesh.vertices = IGB283Transform.rotate(mesh.vertices, angle * (Mathf.PI / 180f));
        /*
        if (scaleVector.x == 0 || scaleVector.x == null) {
            scaleVector = new Vector3(1, scaleVector.y, scaleVector.z);
        }
        if (scaleVector.y == 0 || scaleVector.y == null) {
            scaleVector = new Vector3(scaleVector.x, 1, scaleVector.z);
        }

        if (scaleVector.x != currentScaleVector.x) {
            mesh.vertices = IGB283Transform.scale(mesh.vertices, new Vector3(1f / currentScaleVector.x, 1f,1f) , currentScaleVector);
            mesh.vertices = IGB283Transform.scale(mesh.vertices, scaleVector, currentScaleVector);
            currentScaleVector = new Vector3(scaleVector.x, currentScaleVector.y, 0);
        }
        if (scaleVector.y != currentScaleVector.y) {
            mesh.vertices = IGB283Transform.scale(mesh.vertices, new Vector3(1f, 1f / currentScaleVector.y, 1f), currentScaleVector);
            mesh.vertices = IGB283Transform.scale(mesh.vertices, scaleVector, currentScaleVector);
            currentScaleVector = new Vector3(currentScaleVector.x, scaleVector.y, 0);
        }*/

        bouncy();



        mesh.RecalculateBounds();
    }

    bool bouncy() {
        for (int i = 0; i < mesh.vertices.Length; i++) {
            for (int j = 0; j < mesh.vertices.Length; j++) {
                if (mesh.vertices[i].x <= point1.GetComponent<MeshFilter>().mesh.vertices[j].x || mesh.vertices[i].x >= point2.GetComponent<MeshFilter>().mesh.vertices[j].x) {
                    point = new Vector3(point.x * -1, point.y, point.z);
                    angle = angle * -1;
                    return true;
                }
            }
        }
        return false;
    }
}
