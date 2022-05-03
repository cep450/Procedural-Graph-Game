using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex : MonoBehaviour
{

    public List<Edge> edges {get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete() {

        //delete connected edges, they no longer go anywhere
        foreach(Edge e in edges) {
            e.Delete();
        }

        Destroy(this);
    }
}
