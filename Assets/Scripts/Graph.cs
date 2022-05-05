using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{

    List<Vertex> vertices;

    //for convex hull 
    //https://docs.unity3d.com/ScriptReference/PolygonCollider2D.SetPath.html
    //generate stuff outside this, then add the new vertex 
    //but theyre ORDERED so 
    //have to do hull algo i guess 

    // Start is called before the first frame update
    void Start()
    {
        vertices = new List<Vertex>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void UpdateHull() {

    }
}
