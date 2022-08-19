using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;

/* 
 * BoxCollider 가지고 있는 애 (Trigger 꺼져있는)
 * 부딛히면 작동
 * 
 */

namespace InGameObjects.Interaction
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ColliderObject : InteractingObject
    {
        private async UniTask OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                PlayerManager.Instance.interactingPlayer.SetFstInteractObj(this);
                await InteractAsync();
            }
        }
    }
}
