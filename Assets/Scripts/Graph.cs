using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{

    public List<Vertex> vertices {get; private set;}
    public List<Vertex> hull {get; private set;}

    //for convex hull 
    //https://docs.unity3d.com/ScriptReference/PolygonCollider2D.SetPath.html
    //generate stuff outside this, then add the new vertex 
    //but theyre ORDERED so 
    //have to do hull algo i guess 
    //though, don't if using trig in other areas 

    public Graph() {
        vertices = new List<Vertex>();
        hull = new List<Vertex>();
    }

    public void AddVertex(Vertex v) {
        vertices.Add(v);
        /*if(vertices.Count > 3) {
            UpdateHull(v);
        } else {
            hull.Add(v);
        }*/
    }

    void UpdateHull(Vertex newVert) {

        //sort the new vert in 

        //run hull algo 

    }
}
