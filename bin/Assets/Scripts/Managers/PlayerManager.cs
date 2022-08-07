using InGameObjects.Interaction;
using InGameObjects.Object;
using UnityEngine;
using UnityEngine.Serialization;

/* 플레이어 관리
 * 
 * 범위내에 있는 Scriptable Object를 List로 가지고 있음 
 * -> 가장 가까운 애가 Scriptable Window로 뜸
 * 
 */
namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public GameObject moveablePlayerObject;
        public BoxCollider2D moveablePlayerCollider;
        [FormerlySerializedAs("playerInteractObject")] public InteractingPlayer interactingPlayer;
        [SerializeField] private bool profileShowing = false;
        public void UpdatePlayerManager(GameObject mPO)
        {
            moveablePlayerObject = mPO;
            interactingPlayer = mPO.GetComponent<InteractingPlayer>();
            moveablePlayerCollider = moveablePlayerObject.GetComponent<BoxCollider2D>();
            InputManager.Instance.SetMovingComponent(mPO.GetComponent<MovingObject>());
        }
        public void SetProfileShowing(bool tf)
        {
            profileShowing = tf;
            WindowManager.Instance.profileWindow.gameObject.SetActive(profileShowing);
        }
    }
}
