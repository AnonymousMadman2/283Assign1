using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGB283Transform : MonoBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public static Vector3[] rotate(Vector3[] vertices, float angle) {

        // Get the rotation matrix
        Matrix3x3 R = rotateMatrix(angle * Time.deltaTime);

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] = R.MultiplyPoint(vertices[i]);
        }
        return vertices;
    }

    public static Vector3[] translate(Vector3[] vertices, Vector3 point) {

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] += point;
        }
        return vertices;
    }

    static Matrix3x3 rotateMatrix(float angle) {

        Matrix3x3 matrix = new Matrix3x3();

        matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        matrix.SetRow(1, new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0.0f));
        matrix.SetRow(2, new Vector3(0.0f, 0.0f, 1.0f));

        return matrix;
    }
}
