using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Vertex vertexPrefab;
    [SerializeField] Edge edgePrefab;
    [SerializeField] Player playerPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //spawn player at 0,0 
        Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
