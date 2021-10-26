using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BoxCollider2D (Trigger X) ºÙ¾îÀÖ´Â ¾Öµé
 * 
 * ºÎµóÈ÷¸é InteractionList¿¡ ³Ö¾îÁà¼­ InteractionWindow ¶ç¿öÁÜ
 */
public class ScriptableObject : InteractionObject
{
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

    public override void Interact() {
        throw new System.NotImplementedException();
    }
}
