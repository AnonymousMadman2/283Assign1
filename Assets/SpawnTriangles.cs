using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTriangles : MonoBehaviour {

    public int numOfTriangles;
    public GameObject Triangle;
    public GameObject point1;
    public GameObject point2;

    // Use this for initialization
    void Start () {
        for (int i = 0; i <= numOfTriangles; i++) {
            GameObject tri = Instantiate(Triangle);
            float triNum = i+1;
            tri.GetComponent<RenderTrianglesMultiple>().angle = triNum*10;
            tri.GetComponent<RenderTrianglesMultiple>().point1 = point1;
            tri.GetComponent<RenderTrianglesMultiple>().point2 = point2;
            tri.GetComponent<RenderTrianglesMultiple>().point = new Vector3 (triNum/10, tri.GetComponent<RenderTrianglesMultiple>().point.y, 0);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
