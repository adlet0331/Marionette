using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BoxCollider 가지고 있는 애 (Trigger 꺼져있는)
 * 부딛히면 작동
 * 
 */

public class ColliderObject : InteractionObject
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }

    public override void Interact() 
    {
        throw new System.NotImplementedException();
        
    }
}
