using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Util : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    //Returns the angle these 3 points make, in radians, with pt2 being the center vert.
    public static float AnglePointsMake(Vector2 pt1, Vector2 pt2, Vector2 pt3) {

        //TODO

        return 0f;
    }
}
