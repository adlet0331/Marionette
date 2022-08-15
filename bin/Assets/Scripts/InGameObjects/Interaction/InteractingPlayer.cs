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
        [SerializeField] private List<InteractingObject> scriptableObjList;
        private int beforeScriptableObjListLegnth;
        
        private void Start()
        {
            beforeScriptableObjListLegnth = 0;
            scriptableObjList = new List<InteractingObject>();
        }
        private void updateFstIdx()
        {
            if (scriptableObjList.Count == 0)
            {
                this.currentInteractable = false;
            }
            else
            {
                float angle = 180.0f, cnt;
                InteractingObject currentFirstObj = scriptableObjList[0];
                var pointerWorldVec2 = InputManager.Instance.GetWorldPointerVec2();

                foreach (InteractingObject interObj in scriptableObjList)
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
                foreach (InteractingObject interObj in scriptableObjList)
                {
                    interObj.GetComponent<InteractionObject>().SetSelecting(interObj.Idx == currentFirstObj.Idx);
                }
            }
        }
        private void Update()
        {
            if (this.isBlocked)
                return;

            if (beforeScriptableObjListLegnth != scriptableObjList.Count)
            {
                beforeScriptableObjListLegnth = scriptableObjList.Count;
                updateFstIdx();
            }
            
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
            this.isBlocked = true;
            WindowManager.Instance.interactableWindow.CloseWindow();
        }
        public void UnblockInteract()
        {
            this.isBlocked = false;
        }
        public void ClearScriptableObjList()
        {
            scriptableObjList.Clear();
        }
        public void SetFstInteractObj(InteractingObject interactingObject)
        {
            scriptableObjList[0] = interactingObject;
        }
        public InteractingObject GetFstInteractObj()
        {
            if (scriptableObjList.Count == 0)
                return null;

            else
            {
                updateFstIdx();
                return currentInteractObj;
            }
        }
        public void AddInteractionList(InteractingObject interactionObj)
        {
            scriptableObjList.Add(interactionObj);
        }

        public void RemoveInteractionObj(InteractingObject interactionObj)
        {
            interactionObj.GetComponent<InteractionObject>().SetSelecting(false);
            scriptableObjList.Remove(interactionObj);
        }
    }
}
