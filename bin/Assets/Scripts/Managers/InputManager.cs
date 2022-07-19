using System;
using System.Collections.Generic;
using InGameObjects.Object;
using UI;
using UnityEngine;

/* 
 * 플레이어의 KeyBoard 입력을 받아서 처리하는 매니저
 * 
 */
namespace Managers
{
    public class InputManager : Singleton<InputManager>
    {
        // 움직임, Keyboard Input
        [SerializeField] private bool isMoveable;
        [SerializeField] private bool isInputAvaliable;
        
        [SerializeField] private bool isInteractable = true;
        
        [SerializeField] private bool isInventoryWindowOn = false;
        [SerializeField] private bool isItemSelectionPannelOn = false;
        [SerializeField] private int moveH, moveV;
        [SerializeField] private bool isRun, isSlowWalk;
        [SerializeField] private MovingObject playerMovingComponent;

        [SerializeField] private float mouseCursorWorldX;
        [SerializeField] private float mouseCursorWorldY;
    
        [SerializeField] private float characterWorldX;
        [SerializeField] private float characterWorldY;
        
        public void SetOptions(bool isMv, bool isAv)
        {
            isMoveable = isMv;
            isInputAvaliable = isAv;
        }
        public void SetInventWind(bool tf)
        {
            isInventoryWindowOn = tf;
        }
        public void SetItemSelectionPannel(bool tf)
        {
            isItemSelectionPannelOn = tf;
        }
        public void SetMovingComponent(MovingObject mo)
        {
            playerMovingComponent = mo;
        }

        public Vector2 GetWorldPointerVec2()
        {
            var mouseCursorWorldPos = CameraManager.Instance.GetMouseCursorWorldPointVec2();
            var characterTransform = playerMovingComponent.transform;
            var characterWorldPos = characterTransform.localPosition;

            mouseCursorWorldX = mouseCursorWorldPos.x;
            mouseCursorWorldY = mouseCursorWorldPos.y;
        
            characterWorldX = characterWorldPos.x;
            characterWorldY = characterWorldPos.y;
        
            return new Vector2(mouseCursorWorldPos.x - characterWorldPos.x, mouseCursorWorldPos.y - characterWorldPos.y);
        }
    
        private Dictionary<KeyCode, Action> keyDictionary;
        private void Start() {
            isInputAvaliable = true;
            keyDictionary = new Dictionary<KeyCode, Action> {
                { KeyCode.Space, Interact },
                { KeyCode.Escape, OpenSettings },
                { KeyCode.C, TalkWithDoll }, 
                { KeyCode.Tab, SettingTabChange},
            };
        }

        private void Update() {
            // Doll Window
            if (Input.GetKeyDown(KeyCode.Space) && 
                WindowManager.Instance.CurrentOpenWindowTypeString == "UI.DollTalkWindow")
            {
                bool isEnd = WindowManager.Instance.dollTalkWindow.PressSpace();
                if (isEnd)
                {
                    WindowManager.Instance.dollTalkSelectionWindow.Activate();
                }
                Debug.Log("UI.DollTalkWindow");
                return;
            }
            // Doll Talk Selection window
            if (WindowManager.Instance.CurrentOpenWindowTypeString == "UI.DollTalkSelectionWindow")
            {
                Debug.Log("UI.DollTalkSelectionWindow");
                return;
            }
            // 인벤토리 판넬
            if (WindowManager.Instance.CurrentOpenWindowTypeString == "UI.ItemSelectionPannel")
            {
                int moveInt = 0;
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    moveInt -= 1;
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    moveInt += 1;

                WindowManager.Instance.inventoryWindow.MoveEquipWindowIdx(moveInt);
                return;
            }
            // 인벤토리
            if (WindowManager.Instance.CurrentOpenWindowTypeString == "UI.InventoryWindow")
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                    WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(1);
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(-1);
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                    WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(-2);
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(2);
                return;
            }
            
            if (isInputAvaliable && Input.anyKeyDown) {
                foreach (var dic in keyDictionary) {
                    if (Input.GetKeyDown(dic.Key))
                        dic.Value();
                }
            }
            
            // Move
            if (!playerMovingComponent)
                return;

            if (isMoveable) {
                moveH = (int)Input.GetAxisRaw("Horizontal");
                moveV = (int)Input.GetAxisRaw("Vertical");
                isRun = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
                isSlowWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

                playerMovingComponent.Move(moveH, moveV, isRun, isSlowWalk);
            
                playerMovingComponent.UpdateHandLightRotate(Vector2.SignedAngle(new Vector2(0, 1), GetWorldPointerVec2()));
            }
            else
            {
                playerMovingComponent.Move(0, 0, false, false);
            }
        }

        private void OpenSettings() {
            WindowManager.Instance.settingWindow.Activate();
        }
        private void Interact()
        {
            if (!isInteractable)
                return;
            
            var obj = PlayerManager.Instance.interactingPlayer.GetFstInteractObj();

            if (obj == null)
                return;
            
            bool interactionEnd = obj.Interact();
            if (interactionEnd)
            {
                PlayerManager.Instance.interactingPlayer.UnblockInteract();
                PlayerManager.Instance.interactingPlayer.ClearScriptableObjList();
            }
            else
            {
                PlayerManager.Instance.interactingPlayer.BlockInteract();
            }
        }
        private void TalkWithDoll()
        {
            WindowManager.Instance.dollTalkWindow.Activate();
        }

        private void SettingTabChange()
        {
            if (WindowManager.Instance.settingWindow.gameObject.activeSelf)
            {
                WindowManager.Instance.settingWindow.tabInput();
            }
        }
    }
}
