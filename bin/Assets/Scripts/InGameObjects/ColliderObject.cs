using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BoxCollider ������ �ִ� �� (Trigger �����ִ�)
 * �ε����� �۵�
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
