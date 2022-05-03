using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float speed = 3f;

    float testUnit = 0.001f;

    KeyCode upKey = KeyCode.W;
    KeyCode downKey = KeyCode.S;
    KeyCode leftKey = KeyCode.A;
    KeyCode rightKey = KeyCode.D;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 moveVector = Vector2.zero;

        if(Input.GetKey(upKey)) {
            moveVector += Vector2.up;
        } 
        if(Input.GetKey(downKey)) {
            moveVector += Vector2.down;
        } 
        if(Input.GetKey(leftKey)) {
            moveVector += Vector2.left;
        } 
        if(Input.GetKey(rightKey)) {
            moveVector += Vector2.right;
        } 

        if(moveVector.magnitude != 0) {
            moveVector.Normalize();
            moveVector = moveVector * speed * Time.deltaTime;
            TryMove(moveVector);
        }
    }

    void TryMove(Vector2 moveVector) {

        Vector2 startPoint = new Vector2(transform.position.x, transform.position.y);
        Vector2 endPoint = startPoint + moveVector;
        Vector2 actualMove = Vector2.zero;

        if(Physics2D.OverlapPoint(endPoint, Physics2D.DefaultRaycastLayers, -1) != null) {
            //there is a collider here, ok to move to 
            actualMove = moveVector;
        } else {
            //this would take us out of the trigger
            //test along the vector til we can move there
            for(float distToMove = moveVector.magnitude; distToMove > 0; distToMove -= testUnit) {
                if(Physics2D.OverlapPoint(startPoint + (moveVector.normalized * distToMove), Physics2D.DefaultRaycastLayers, -1) != null) {
                    //can move here
                    actualMove = moveVector.normalized * distToMove;
                    break;
                } 
            }
        }
        if(actualMove.magnitude != 0) {
            transform.Translate(actualMove);
        }
    }
}
