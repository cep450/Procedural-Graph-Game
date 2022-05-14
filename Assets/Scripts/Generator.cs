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

    public static Graph graph;

    ContactFilter2D vertexFilter = new ContactFilter2D();


    // Start is called before the first frame update
    void Start()
    {

        //vertices are on Z=10 
        vertexFilter.useDepth = true;
        vertexFilter.maxDepth = 11;
        vertexFilter.minDepth = 9;

        //spawn player at 0,0 
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);

        graph = new Graph();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
            GenerateVertex();
        }


        //if graph density low- not many edges compared to verts- generate edges 
        //if graph density high- lots of edges, few verts- generate new vert 
    }

    void Initialize() {
        
        //start with 3 points (smallest hull)
        //with one always starting at 0,0, where the player spawns
        //spawn these hardcoded cause subsequent functions rely on a hull existing

        float max = maxDistBetweenPts / 2;
        float min = -max;
        
        Vertex v1 = InstantiateVertex(Vector2.zero);
        Vertex v2 = InstantiateVertex(new Vector2(-3, 4));
        Vertex v3 = InstantiateVertex(new Vector2(3, 4));

        //connect them as a single cycle 
        InstantiateEdgeOnVertices(v1, v2);
        InstantiateEdgeOnVertices(v2, v3);
        InstantiateEdgeOnVertices(v3, v1);

        //add to graph 
        graph.AddVertex(v1, 0);
        graph.AddVertex(v2, 0);
        graph.AddVertex(v3, 0);

    }

    //place a new point outside the hull
    void GenerateVertex() {

        //pick a point on the hull to generate from

        //TODO do this intelligently 
        //just picks one randomly for testing purposes. 
        Debug.Log(Random.Range(0, graph.hull.Count));
        Vertex v = graph.hull[Random.Range(0, graph.hull.Count)];
        
        GenerateVertexNear(v);

    }

    void GenerateVertexNear(Vertex v) {

        //pick a distance, max and min 
        float dist = Random.Range(minDistBetweenPts, maxDistBetweenPts);


        //use the angle adjacent hull verts va, v, and vb make, as well as the angle between v, va/vb, and a normal out from va/vb of dist length, to determine the 
        //range of angles we can go out from v 
        //since we know the obtuse angle faces outside the hull and vice versa 

        //adjacent verts in hull 
        Debug.Log(graph.hull.IndexOf(v)); 
        int hullIndex = graph.hull.IndexOf(v);
        
        Debug.Log((int)(Mathf.Repeat(hullIndex - 1, graph.hull.Count)));
        Debug.Log((int)(Mathf.Repeat(hullIndex + 1, graph.hull.Count)));
        Vertex va = graph.hull[(int)(Mathf.Repeat(hullIndex - 1, graph.hull.Count))];
        Vertex vb = graph.hull[(int)(Mathf.Repeat(hullIndex + 1, graph.hull.Count))];

        float distVA = Mathf.Abs((v.transform.position - va.transform.position).magnitude);
        float distVB = Mathf.Abs((v.transform.position - vb.transform.position).magnitude);
        float distAB = Mathf.Abs((va.transform.position - vb.transform.position).magnitude);
        float distABhalf = distAB / 2f;
        
        //acute angle 
        float angleAVB = Mathf.Asin((distABhalf / distVA)) + Mathf.Asin((distABhalf / distVB));

        //min angles towards each point- more could get closer than minimum to the other points 
        float angleMinA = Mathf.Atan(minDistBetweenPts / distVA);
        float angleMinB = Mathf.Atan(minDistBetweenPts / distVB);

        //all returned by Mathf are in radians- 6.28 is 360deg
        float angleRange = 6.28f - angleAVB - angleMinA - angleMinB;

        Vector3 aRelativeToV = va.transform.position - v.transform.position;
        float angleAVOrigin = Mathf.Atan2(aRelativeToV.x, aRelativeToV.y);

        Vector3 bRelativeToV = vb.transform.position - v.transform.position;
        float angleBVOrigin = Mathf.Atan2(bRelativeToV.x, bRelativeToV.y);

        float angleStartFrom = 0;
        if(Mathf.Abs(angleAVOrigin - angleBVOrigin) > Mathf.Abs(angleBVOrigin - angleAVOrigin)) {
            //clockwise start from A
            angleStartFrom = angleAVOrigin; 
        } else {
            //clockwise start from B
            angleStartFrom = angleBVOrigin;
        }

        float angle = angleStartFrom + Random.Range(0f, angleRange);
        
        //finally, use angle, distance from v to get new point 
        Vector3 newPointPos = Quaternion.AngleAxis(Mathf.Rad2Deg * angle, new Vector3(0, 0, 1)) * Vector2.right * dist;

        //connect to rest of graph with 
        Vertex newVert = InstantiateVertex(newPointPos);
        graph.AddVertex(newVert, hullIndex);
        InstantiateEdgeOnVertices(v, newVert);

    }

    void GenerateEdgeOnGraph() {

        //find a vertex to generate on
        //GenerateEdgeOnVertex();

    }

    
    List<Collider2D> collidersInRange; //what should the scope of this be for best memory use? 
    //make a new edge on a given vertex, to a vertex in range 
    bool GenerateEdgeOnVertex(Vertex v) {

        //find a vertex to connect to within range 
        Physics2D.OverlapCircle(v.transform.position, maxDistBetweenPts, vertexFilter, collidersInRange);

        if(collidersInRange.Count < 1) {
            //no vertices in range!
            Debug.Log("No vertices in range!");
            return false; 
        }

        //pick a rand. if this isnt a vertex, go to next in list, with wrapping. if we check everything in list, no verts in range 
        int rand = Random.Range(0, collidersInRange.Count);
        Vertex otherVertex = null;
        for(int i = 0; i < collidersInRange.Count; i++) {
            otherVertex = collidersInRange[(int)(Mathf.Repeat(rand + i, collidersInRange.Count))].gameObject.GetComponent<Vertex>();
            if(otherVertex != null) {
                if(!otherVertex.connectedVertices.Contains(v)) {
                    break;
                } else {
                    otherVertex = null;
                }
            }
        }

        if(otherVertex == null) {
            Debug.Log("No vertices in range available to connect!");
            return false;
        }
        
        InstantiateEdgeOnVertices(v, otherVertex);
        
        return true;
    }

    //instantiates edge object, adds to graph
    Edge InstantiateEdgeOnVertices(Vertex v1, Vertex v2) {
        Edge e = Instantiate(edgePrefab, edgePrefab.transform.position, Quaternion.identity);
        e.AddToGraph(v1, v2);
        return e;
    }

    //instantiates vertex object, adds to graph 
    Vertex InstantiateVertex(Vector2 pos) {
        Vertex v = Instantiate(vertexPrefab, new Vector3(pos.x, pos.y, vertexPrefab.transform.position.z), Quaternion.identity);
        return v;
    }
}
