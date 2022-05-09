using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{

    public Vertex v1 { get; private set; }
    public Vertex v2 { get; private set; }
    private float length; 
    private float weightMultiplier = 1f;
    public float weight { get; private set; } //TODO set this to return length * weightMultiplier


    public void AddToGraph(Vertex v, Vertex w) {

        v1 = v;
        v2 = w;

        v1.AddEdge(this);
        v2.AddEdge(this);

        //position of center 
        transform.position = (v1.transform.position + v2.transform.position) / 2f;

        //scale out for length
        length = Mathf.Abs((v1.transform.position - v2.transform.position).magnitude);
        transform.localScale = new Vector3(length, 1, 1);

        //rotation  
        transform.LookAt(v1.transform);
        transform.Rotate(0f, 90f, 0f);
        
    }

    public void Delete() {

        //remove ourselves from our connected verts 
        v1.RemoveEdge(this);
        v2.RemoveEdge(this);
        
        Destroy(this);

    }
}
