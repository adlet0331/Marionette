using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacter : MovingObject 
{
    private void OnCollision2D(Collision collision) {
        Debug.Log("Collision Entered");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("OnTrigger Entered");
    }
}
