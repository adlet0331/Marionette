using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BoxCollider2D (Trigger X) 붙어있는 애들
 * 
 * 부딛히면 InteractionList에 넣어줘서 InteractionWindow 띄워줌
 */
public class ScriptableObject : InteractionObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerInteractObject.AddInteractionList(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerInteractObject.RemoveInteractionObj(this);
        }
    }

    public override void Interact() {
        WindowManager.Instance.scriptWindow.Activate();
    }
}
