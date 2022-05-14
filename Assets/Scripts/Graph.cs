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
        hull = new List<Vertex>();          //hull is ordered 
    }

    //only adds to graph, not the hull. 
    public void AddVertex(Vertex newVertex) {
        vertices.Add(newVertex);
    }

    //also adds a vert to the hull. 
    public void AddVertex(Vertex newVertex, int hullIndex) {
        UpdateHullAdd(newVertex, hullIndex);
        AddVertex(newVertex);
    }

    void UpdateHullAdd(Vertex newVertex, int index) {

        if(hull.Count <= 3) {
            hull.Add(newVertex);
        }
        

        //sort the new vert in 

        //run hull algo just on local stuff- do we need to remove any verts from the hull? 
        //(do they make the wrong type of angle?)

    }

    void UpdateHullRemove(Vertex removedVert) {

        int hullIndex = hull.IndexOf(removedVert);

        if(hullIndex < 0) {
            return; //not in hull, no change needed 
        }

        //this ones harder cause need to update the hull w removal 
        //and this could mean that a previously non-hull vert gets added to the hull 
        //so might need to run full hull algo, unless angle from some original point is stored somewhere and 
        //points are ordered by that angle even non-hull 
    }

    void DeleteVertex(Vertex v) {

        //remove from vertices 
        vertices.Remove(v);

        //if in hull, remove from hull, and update hull 
        UpdateHullRemove(v);

        v.Delete();
    }
}
