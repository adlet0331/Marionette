using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObject : InteractionObject
{
    private void Start() {

    }
    private void Update() {
        if (CheckPlayerCollided()) {
            Debug.Log("moveablePlayerCollider Collided");
        }
    }
    public override void Interact() {
        throw new System.NotImplementedException();
        
    }
}
