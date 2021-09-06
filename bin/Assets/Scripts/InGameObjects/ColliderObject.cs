using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObject : InteractionObject
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject == PlayerManager.Instance.moveablePlayerObject)
        {
            PlayerManager.Instance.AddInteractionList(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject == PlayerManager.Instance.moveablePlayerObject)
        {
            PlayerManager.Instance.RemoveInteractionObj(this.gameObject);
        }
    }

    private void Update() 
    {
        
    }
    public override void Interact() 
    {
        throw new System.NotImplementedException();
        
    }
}
