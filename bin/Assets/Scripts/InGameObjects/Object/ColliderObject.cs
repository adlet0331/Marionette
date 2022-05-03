using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * BoxCollider 가지고 있는 애 (Trigger 꺼져있는)
 * 부딛히면 작동
 * 
 */

public abstract class ColliderObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            InteractIn();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            InteractOut();
        }
    }

    public abstract void InteractIn();
    public abstract void InteractOut();
}
