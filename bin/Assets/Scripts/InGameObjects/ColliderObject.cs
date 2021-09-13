using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderObject : InteractionObject
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.AddInteractionList(this);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.RemoveInteractionObj(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.AddInteractionList(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.RemoveInteractionObj(this);
        }
    }

    public override void Interact() 
    {
        throw new System.NotImplementedException();
        
    }
}
