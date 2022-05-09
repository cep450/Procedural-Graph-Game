using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{

    public List<Edge> edges {get; private set;}

    void Awake()
    {
        edges = new List<Edge>();
    }

    public void AddEdge(Edge e) {
        edges.Add(e);
    }

    public void RemoveEdge(Edge e) {
        edges.Remove(e);
    }

    public void Delete() {

        //delete connected edges, they no longer go anywhere
        foreach(Edge e in edges) {
            e.Delete();
        }

        Destroy(this);
    }
}
