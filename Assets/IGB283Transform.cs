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
        Matrix3x3 R = rotateMatrix(angle);

        Vector3 origin = calcOrigin(vertices);

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] = R.MultiplyPoint(vertices[i] - origin) + origin;
        }
        
        return vertices;
    }

    public static Vector3 calcOrigin(Vector3[] vertices) {
        Vector3 origin = new Vector3(0f, 0f, 0f);
        for (int i = 0; i < vertices.Length; i++) {
            origin += vertices[i];
        }
        //force to 0 in a very preofessional and not at all ad hoc manner
        origin = new Vector3(origin.x, origin.y, 0);
        origin /= vertices.Length;

        return origin;
    }

    public static Vector3[] translate(Vector3[] vertices, Vector3 point) {

        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] += point;
        }
        return vertices;
    }


    ///v′  =  v  +  (k – 1)(v•n)n
    public static Vector3[] scale(Vector3[] vertices, Vector3 scaleVector, Vector3 currentScaleVector) {

        float scaleFactor;

        Vector3 origin = calcOrigin(vertices);

        if(currentScaleVector.x != scaleVector.x) {
            scaleFactor = scaleVector.x;
            scaleVector = new Vector3(1f, 0f, 0f);
        }else if(currentScaleVector.y != scaleVector.y) {
            scaleFactor = scaleVector.y;
            scaleVector = new Vector3(0f, 1f, 0f);
        } else {
            scaleFactor = 1;
        }
        
        Matrix3x3 S = scaleMatrix(scaleFactor, scaleVector);


        for (int i = 0; i < vertices.Length; i++) {
            vertices[i] = S.MultiplyPoint(vertices[i]);
            /*
            if (vertices[i].x < origin.x) {
                vertices[i] -= new Vector3(-scaleFactor.x,0,0);
            }else if (vertices[i].x > origin.x) {
                vertices[i] -= new Vector3(scaleFactor.x, 0, 0);
            }

            if (vertices[i].y < origin.y) {
                vertices[i] -= new Vector3(0, -scaleFactor.y, 0);
            } else if (vertices[i].y > origin.y) {
                vertices[i] -= new Vector3(0, scaleFactor.y, 0);
            }
            */

        }
        return vertices;
    }

    static Matrix3x3 rotateMatrix(float angle) {
        

        Matrix3x3 matrix = new Matrix3x3();

        matrix.SetRow(0, new Vector3(Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f));
        matrix.SetRow(1, new Vector3(Mathf.Sin(angle),  Mathf.Cos(angle), 0.0f));
        matrix.SetRow(2, new Vector3(0f, 0f, 1.0f));

        return matrix;
    }

    static Matrix3x3 scaleMatrix(float scaleFactor, Vector3 scaleVector) {
        Matrix3x3 matrix = new Matrix3x3();

        matrix.SetRow(0, new Vector3(1 + (scaleFactor - 1) * scaleVector.x * scaleVector.x, (scaleFactor - 1) * scaleVector.x * scaleVector.y, 0f));
        matrix.SetRow(1, new Vector3((scaleFactor - 1) * scaleVector.x * scaleVector.y, 1 + (scaleFactor - 1) * scaleVector.y * scaleVector.y, 0f));
        matrix.SetRow(2, new Vector3(0f, 0f, 1f));

        return matrix;
    }
}
