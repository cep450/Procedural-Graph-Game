using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Vertex vertexPrefab;
    [SerializeField] Edge edgePrefab;
    [SerializeField] Player playerPrefab;

    [SerializeField] float minDistBetweenPts, maxDistBetweenPts;

    Graph graph;


    // Start is called before the first frame update
    void Start()
    {
        //spawn player at 0,0 
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        graph = new Graph();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize() {
        
        //start with 3 points (smallest hull)
        //with one always starting at 0,0, where the player spawns
        //spawn these hardcoded cause subsequent functions rely on a hull existing

        float max = maxDistBetweenPts / 2;
        float min = -max;
        
        //Vector2 randPos1 = new Vector2(Random.Range(min, max), Random.Range(min, max));
        //Vector2 randPos2 = new Vector2(Random.Range(min, max), Random.Range(min, max));

        Vertex v1 = Instantiate(vertexPrefab, Vector3.zero, Quaternion.identity);
        Vertex v2 = Instantiate(vertexPrefab, new Vector3(-3, 4, 0), Quaternion.identity);
        Vertex v3 = Instantiate(vertexPrefab, new Vector3(3, 4, 0), Quaternion.identity);

        //connect them as a single cycle 
        AddEdgeOnVertices(v1, v2);
        AddEdgeOnVertices(v2, v3);
        AddEdgeOnVertices(v3, v1);

        //add to graph 
        graph.AddVertex(v1);
        graph.AddVertex(v2);
        graph.AddVertex(v3);

    }

    //place a new point outside the hull
    void GenerateVertex() {

        //pick a point on the hull to generate from 

        //try different verts til succeed 
        //TODO safety for excessive # of iterations 
        bool success = false;
        while(!success) {

            //TODO select a vert from part of the hull that makes sense to generate from 
            Vertex v = null;

            success = GenerateVertexNear(v);
        }
        
    }

    bool GenerateVertexNear(Vertex v) {

        //pick a distance, max and min 

        //pick an angle that takes outside of hull angles PLUS angle that would take too close to points connected 
        ///use connecting points to get angle they make with the center 
        ///use the obtuse angle- will be outside the hull 
        ///try a point, check it's not too close to either other vert 
            //if fail, return false
        //can use trig to find what remains in the hull, insert the new point 


        //connect to rest of graph with 
        //GenerateEdgeOnVertex(v);
        return true;

    }

    void GenerateEdgeOnGraph() {

        //find a vertex to generate on
        //GenerateEdgeOnVertex();

    }

    void GenerateEdgeOnVertex(Vertex v) {

        //find a vertex to connect to within range 
        //AddEdgeOnVertices(v, other);

    }

    void AddEdgeOnVertices(Vertex v1, Vertex v2) {
        Edge e = Instantiate(edgePrefab, Vector3.zero, Quaternion.identity);
        e.AddToGraph(v1, v2);
    }
}
