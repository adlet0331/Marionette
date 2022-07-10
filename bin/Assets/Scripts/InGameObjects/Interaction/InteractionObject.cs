using UnityEngine;

namespace InGameObjects.Object
{
    public class InteractionObject : InteractingObject
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerManager.Instance.interactingPlayer.AddInteractionList(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                PlayerManager.Instance.interactingPlayer.RemoveInteractionObj(this);
            }
        }
    }
}