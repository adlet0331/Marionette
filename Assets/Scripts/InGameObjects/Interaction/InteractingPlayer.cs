using System.Collections.Generic;
using Managers;
using UnityEngine;

/*
 * InteractAsync 하는 주체 (주로 플레이어)
 * 
 */
namespace InGameObjects.Interaction
{
    public class InteractingPlayer : MonoBehaviour
    {
        [SerializeField] private bool isBlocked = false;
        [SerializeField] private bool currentInteractable;
        [SerializeField] private InteractingObject currentInteractObj;

        public InteractingObject CurrentInteractObj
        {
            get
            {
                if (interactionObjList.Count == 0)
                {
                    return null;
                }
                else
                {
                    return currentInteractObj;
                }
            }
        }

        [SerializeField] private List<InteractingObject> interactionObjList;
        [Header("Update 주기")]
        [SerializeField] private int updateFrameCount;

        private void Start()
        {
            interactionObjList = new List<InteractingObject>();
        }

        private void UpdateInteractableWindow(bool isInteractable)
        {
            if (isInteractable && !GamePlayManager.Instance.WindowsInstances.interactableWindow.IsOpened)
                GamePlayManager.Instance.WindowsInstances.interactableWindow.Activate();
            else if (!isInteractable && GamePlayManager.Instance.WindowsInstances.interactableWindow.IsOpened)
                GamePlayManager.Instance.WindowsInstances.interactableWindow.CloseWindow();
        }
        
        private void UpdateInteractionQueue()
        {
            if (interactionObjList.Count == 0 || isBlocked)
            {
                currentInteractable = false;
                UpdateInteractableWindow(currentInteractable);
            }
            else
            {
                // Check Unreachable Interacting Objects
                var removeList = new List<InteractingObject>();

                foreach (InteractingObject interObj in interactionObjList)
                {
                    if (!interObj.gameObject || !interObj.gameObject.activeSelf || !interObj.GetComponent<InteractionObject>())
                    {
                        removeList.Add(interObj);
                    }
                }

                foreach (InteractingObject interObj in removeList)
                {
                    interactionObjList.Remove(interObj);
                }

                // 다시 남아있는지 확인
                if (interactionObjList.Count == 0)
                {
                    currentInteractable = false;
                    currentInteractObj = null;
                    UpdateInteractableWindow(currentInteractable);
                    return;
                }

                // 가장 마지막으로 들어온 애를 설정
                // 우선순위가 생긴다면 currentInteractObj 선정하는 로직이 들어갈 자리
                currentInteractObj = interactionObjList[^1];
                currentInteractable = true;
                foreach (InteractingObject interObj in interactionObjList)
                {
                    interObj.GetComponent<InteractionObject>()?.SetSelecting(interObj.Idx == currentInteractObj.Idx);
                }
                UpdateInteractableWindow(currentInteractable);
            }
        }
        public void BlockInteract()
        {
            isBlocked = true;
            GamePlayManager.Instance.WindowsInstances.interactableWindow.CloseWindow();
            foreach (InteractingObject interObj in interactionObjList)
            {
                interObj.GetComponent<InteractionObject>()?.SetSelecting(false);
            }
        }
        public void UnBlockInteract()
        {
            this.isBlocked = false;
        }
        public void ClearInteractingObjList()
        {
            interactionObjList.Clear();
            UpdateInteractionQueue();
        }
        public void AddInteractingObject(InteractingObject interactingObject)
        {
            interactionObjList.Add(interactingObject);
            UpdateInteractionQueue();
        }

        public void RemoveInteractionObject(InteractingObject interactionObj)
        {
            interactionObj.GetComponent<InteractionObject>().SetSelecting(false);
            interactionObjList.Remove(interactionObj);
            UpdateInteractionQueue();
        }
    }
}
