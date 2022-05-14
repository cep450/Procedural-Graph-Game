using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{

    public List<Edge> edges {get; private set;}
    public List<Vertex> connectedVertices {get; private set;}

    void Awake()
    {
        edges = new List<Edge>();
        connectedVertices = new List<Vertex>();
    }

/*
    public void AddEdge(Edge e) {
        edges.Add(e);
        //TODO find connected vert and add 
    } */

    public void AddEdge(Edge e, Vertex connectedVertex) {
        edges.Add(e);
        connectedVertices.Add(connectedVertex);
    }

    public void RemoveEdge(Edge e) {
        int index = edges.IndexOf(e);
        edges.RemoveAt(index);
        connectedVertices.RemoveAt(index); //note: this means these have to be parallel arrays 
    }

    public void Delete() {

        //delete connected edges, they no longer go anywhere
        foreach(Edge e in edges) {
            e.Delete();
        }

        Destroy(this);
    }
}
