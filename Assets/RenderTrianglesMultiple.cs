using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTrianglesMultiple : MonoBehaviour {

    public float angle;

    public GameObject point1;
    public GameObject point2;

    public Vector3 scaleVector;
    private Vector3 currentScaleVector;

    public Color color = new Vector4(0.2F, 0.3F, 0.4F, 0.5F);

    public Vector3 point;
    public  Material material;
    public  Mesh mesh;
    Vector3[] triangleVertecies = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0) };

    // Use this for initialization
    void Start() {
        // Add a MeshFilter and MeshRenderer to the Empty GameObject
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();

        // Get the Mesh from the MeshFilter
        mesh = GetComponent<MeshFilter>().mesh;

        // Set the material to the material we have selected
        material = GetComponent< MeshRenderer > ().material;

        // Clear all vertex and index data from the mesh
        mesh.Clear();
        // Create a triangle with points at (0, 0, 0), (0, 1, 0) and(1, 1, 0)
        mesh.vertices = triangleVertecies;

        calcColor();
        // Set vertex indicies
        mesh.triangles = new int[] { 0, 1, 2 };

        scaleVector = new Vector3(1f, 1f, 1f);
        currentScaleVector = scaleVector;
    }

    // Update is called once per frame
    void Update() {

        mesh.vertices = IGB283Transform.translate(mesh.vertices, this.point);

        mesh.vertices = IGB283Transform.rotate(mesh.vertices, this.angle * (Mathf.PI / 180f));
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

        if (isMouseClick(0)) {
            point = point / 2;
        }
        
        if (isMouseClick(1)) {
            point = point * 2;
        }

        calcColor();

        mesh.RecalculateBounds();
    }

    private void calcColor() {        mesh.colors = new Color[] { color, color, color };
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

    private bool isMouseClick(int btn) {
        if (Input.GetMouseButtonDown(btn)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float left = mesh.vertices[1].x;
            float right = mesh.vertices[1].x;
            float top = mesh.vertices[1].y;
            float bottom = mesh.vertices[1].y;

            //find highest, lowest, leftmost and rightmost points

            for (int i = 0; i < mesh.vertices.Length; i++) {
                if (mesh.vertices[i].x <= left) {
                    left = mesh.vertices[i].x;
                    print(left+ "   " + mousePos);
                } else if(mesh.vertices[i].x >= right) {
                    right = mesh.vertices[i].x;
                }

                if (mesh.vertices[i].y <= bottom) {
                    bottom = mesh.vertices[i].y;
                } else if (mesh.vertices[i].y >= top) {
                    top = mesh.vertices[i].y;
                }
            }




            if (mousePos.x <= right && mousePos.x >= left && mousePos.y <= top && mousePos.y >= bottom) {
                return true;
            } else {
                return false;
            }
        }
        return false;
    }

}
