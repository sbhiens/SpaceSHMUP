/*** Using Namespaces ***/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    /*** VARIABLES ***/
    [Header("Inscribed")]
    public float radius = 4f;
    public bool keepOnScreen = true;

    [Header("Dynamic")]
    public bool isOnScreen = true; //is the object on screen 
    [HideInInspector]
    public bool offLeft, offRight, offUp, offDown; //checks for where the object is off screen
    [HideInInspector]
    public float camWidth; //gets the width of the camera
    [HideInInspector]
    public float camHeight; //gets the height of the camera


    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        camHeight = Camera.main.orthographicSize; 
        camWidth = camHeight * Camera.main.aspect;  
    }//end Awake()


    // LateUpdate is called after all Update functions have been called
    void LateUpdate()
    {
        Vector3 pos = transform.position; //vector 3 position 
        isOnScreen = true;

        if(pos.x > camWidth - radius)
        {   pos.x = camWidth - radius;
            offRight = true;
        }

        if (pos.x < -camWidth + radius) 
        {   pos.x = -camWidth + radius;
            offLeft = true;
        }

        if (pos.y > camHeight - radius) 
        {   pos.y = camHeight - radius;
            offUp = true;
        }

        if (pos.y < -camHeight + radius) 
        {   pos.y = -camHeight + radius;
            offDown = true;
        }

        //is the object on screen, depends if any one of the off bools are true, there by making isOnScreen false
        isOnScreen = !(offRight || offLeft || offUp || offDown);

        //if the object is to stay on screen but has moved off screen, move it back
        if(keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false; // reset the off bools to false, when object is meant to stay on screen
        }

    }//end LateUpdate

    
    //Draw the bounds in the scene pane




}
