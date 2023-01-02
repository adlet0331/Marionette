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
        [SerializeField] private List<InteractingObject> interactionObjList;
        [Header("Update 주기")]
        [SerializeField] private int updateFrameCount;

        private void Start()
        {
            interactionObjList = new List<InteractingObject>();
        }
        private void updateFstIdx()
        {
            if (interactionObjList.Count == 0 || isBlocked)
            {
                currentInteractable = false;
            }
            else
            {
                float angle = 180.0f, cnt;
                InteractingObject currentFirstObj = interactionObjList[0];
                var pointerWorldVec2 = InputManager.Instance.GetWorldPointerVec2();

                var removeList = new List<InteractingObject>();
                
                foreach (InteractingObject interObj in interactionObjList)
                {
                    if (!interObj.gameObject.activeSelf || !interObj.GetComponent<InteractionObject>())
                    {
                        removeList.Add(interObj);
                    }
                }
                
                foreach (InteractingObject interObj in removeList)
                {
                    interactionObjList.Remove(interObj);
                }

                foreach (InteractingObject interObj in interactionObjList)
                {
                    var objectWorldPosition = interObj.transform.localToWorldMatrix * interObj.transform.localPosition - transform.localToWorldMatrix * transform.localPosition;
                    var objectWorldVec2 = new Vector2(objectWorldPosition.x, objectWorldPosition.y);
                    cnt = Vector2.Angle(objectWorldVec2, pointerWorldVec2);
                    if (cnt < angle)
                    {
                        angle = cnt;
                        currentFirstObj = interObj;
                    }
                }
                currentInteractObj = currentFirstObj;
                currentInteractable = true;
                foreach (InteractingObject interObj in interactionObjList)
                {
                    interObj.GetComponent<InteractionObject>()?.SetSelecting(interObj.Idx == currentFirstObj.Idx);
                }
            }
        }
        private void Update()
        {
            if (this.isBlocked)
                return;

            updateFrameCount++;
            if (updateFrameCount < 10)
                return;
            updateFrameCount = 0;

            updateFstIdx();
            
            if (!currentInteractable)
            {
                if (WindowManager.Instance.interactableWindow.IsOpened)
                    WindowManager.Instance.interactableWindow.CloseWindow();
            }
            else
            {
                if (!WindowManager.Instance.interactableWindow.IsOpened)
                    WindowManager.Instance.interactableWindow.Activate();
            }
        }
        public void BlockInteract()
        {
            isBlocked = true;
            WindowManager.Instance.interactableWindow.CloseWindow();
            foreach (InteractingObject interObj in interactionObjList)
            {
                interObj.GetComponent<InteractionObject>()?.SetSelecting(false);
            }
        }
        public void UnblockInteract()
        {
            this.isBlocked = false;
        }
        public void ClearScriptableObjList()
        {
            interactionObjList.Clear();
        }
        public void SetFstInteractObj(InteractingObject interactingObject)
        {
            currentInteractObj = interactingObject;
            var list = new List<InteractingObject>();
            list.Add(interactingObject);
            foreach (var var in interactionObjList)
            {
                list.Add(var);
            }
            interactionObjList = list;
        }

        public void ChangeFstInteractObj(InteractingObject interactingObject)
        {
            interactionObjList[0] = interactingObject;
        }
        public InteractingObject GetFstInteractObj()
        {
            if (interactionObjList.Count == 0)
            {
                return null;
            }
            else
            {
                updateFstIdx();
                return currentInteractObj;
            }
        }
        public void AddInteractionList(InteractingObject interactionObj)
        {
            interactionObjList.Add(interactionObj);
        }

        public void RemoveInteractionObj(InteractingObject interactionObj)
        {
            interactionObj.GetComponent<InteractionObject>().SetSelecting(false);
            interactionObjList.Remove(interactionObj);
        }
    }
}
