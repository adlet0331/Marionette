using System;
using InGameObjects.Interaction;
using InGameObjects.SceneObject.Player;
using UnityEngine;

/* 플레이어 관리
 * 
 * 범위내에 있는 Scriptable Object를 List로 가지고 있음 
 * -> 가장 가까운 애가 Scriptable Window로 뜸
 * 
 */
namespace Managers
{
    [Serializable]
    public class CharacterManager : AGameManager
    {
        [SerializeField] private GameObject moveablePlayerObject;

        public Vector3 MoveablePlayerObjectPosition
        {
            get
            {
                if (moveablePlayerObject == null) return Vector3.zero;
                
                return moveablePlayerObject.transform.position;
            }
        }

        [SerializeField] private InteractingPlayer interactingPlayer;
        public InteractingObject CurrentInteractObj => interactingPlayer.CurrentInteractObj;
        public void AddInteractingObject(InteractingObject interactingObject) => interactingPlayer.AddInteractingObject(interactingObject);
        public void RemoveInteractionObject(InteractingObject interactingObject) => interactingPlayer.RemoveInteractionObject(interactingObject);
        public void ClearInteractingObjList() => interactingPlayer.ClearInteractingObjList();
        public void BlockInteract() => interactingPlayer.BlockInteract();
        public void UnBlockInteract() => interactingPlayer.UnBlockInteract();
        
        [SerializeField] private float stress = 0;
        public float Stress => stress;

        public float AddStress(float val)
        {
            stress += val;
            return stress;
        }

        public void MoveCharacterPosition(Vector3 position)
        {
            if (!moveablePlayerObject) return;

            moveablePlayerObject.transform.position = position;
        }

        public void SetCharacterObject(GameObject characterGameObject)
        {
            moveablePlayerObject = characterGameObject;
            var vec = GamePlayManager.Instance.PlayerStartVector;
            if (vec.x != 0 && vec.y != 0)
                moveablePlayerObject.transform.localPosition = vec;
            interactingPlayer = characterGameObject.GetComponent<InteractingPlayer>();
            GamePlayManager.Instance.SetPlayerControlComponent(characterGameObject.GetComponent<PlayerWithHandLight>());
        }
        public override void Start()
        {
            
        }
    }
}
