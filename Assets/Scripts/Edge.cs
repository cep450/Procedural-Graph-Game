using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{

    public Vertex v1 { get; private set; }
    public Vertex v2 { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEnds(Vertex v, Vertex w) {

        v1 = v;
        v2 = w;

        //set position, scaling 

    }

    public void Delete() {

        //remove ourselves from our connected verts 
        v1.edges.Remove(this);
        v2.edges.Remove(this);

        Destroy(this);

    }
}
