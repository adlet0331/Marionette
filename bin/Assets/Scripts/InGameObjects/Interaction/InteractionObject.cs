using Managers;
using UnityEngine;

namespace InGameObjects.Interaction
{
    public sealed class InteractionObject : InteractingObject
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerManager.Instance.interactingPlayer.AddInteractionList(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerManager.Instance.interactingPlayer.RemoveInteractionObj(this);
            }
        }
    }
}