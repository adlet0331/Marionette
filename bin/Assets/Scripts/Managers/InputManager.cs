using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using InGameObjects.Interaction;
using InGameObjects.Object;
using UI;
using UnityEngine;

/* 
 * 플레이어의 KeyBoard 입력을 받아서 처리하는 매니저
 * 
 */
namespace Managers
{
    public enum DirectionArrowType
    {
        Left = 1, 
        Right = 2,
        Up = 3, 
        Down = 4
    }
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

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void FixedUpdate()
        {
            // Move
            if (!playerMovingComponent)
                return;

            if (isMoveable) {
                moveH = (int)Input.GetAxisRaw("Horizontal");
                moveV = (int)Input.GetAxisRaw("Vertical");
                isRun = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                isSlowWalk = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);

                playerMovingComponent.Move(moveH, moveV, isRun, isSlowWalk);
            
                playerMovingComponent.UpdateHandLightRotate(Vector2.SignedAngle(new Vector2(0, 1), GetWorldPointerVec2()));
            }
            else
            {
                playerMovingComponent.Move(0, 0, false, false);
            }
        }

        [SuppressMessage("ReSharper", "Unity.PerformanceCriticalCodeInvocation")]
        private void Update()
        {
            if (!isInputAvaliable)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.DollTalkWindow":
                        WindowManager.Instance.dollTalkWindow.Interact(InputType.Space);
                        return;
                    default:
                        Interact().Forget();
                        return;
                }
            }
            
            if (Input.GetKeyDown((KeyCode.C)))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.DollTalkWindow":
                        WindowManager.Instance.dollTalkWindow.Interact(InputType.C);
                        return;
                    default:
                        WindowManager.Instance.dollTalkWindow.Activate();
                        return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.SettingWindow":
                        WindowManager.Instance.settingWindow.CloseWindow();
                        return;
                    default:
                        WindowManager.Instance.settingWindow.Activate();
                        return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.SettingWindow":
                        WindowManager.Instance.settingWindow.tabInput();
                        return;
                    default:
                        return;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.DollTalkWindow":
                        WindowManager.Instance.dollTalkWindow.Interact(InputType.Up);
                        return;
                    default:
                        return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.DollTalkWindow":
                        WindowManager.Instance.dollTalkWindow.Interact(InputType.Down);
                        return;
                    default:
                        return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.DollTalkWindow":
                        WindowManager.Instance.dollTalkWindow.Interact(InputType.Left);
                        return;
                    default:
                        return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                switch (WindowManager.Instance.CurrentOpenWindowTypeString)
                {
                    case "UI.DollTalkWindow":
                        WindowManager.Instance.dollTalkWindow.Interact(InputType.Right);
                        return;
                    default:
                        return;
                }
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private async UniTask Interact()
        {
            if (!isInteractable)
                return;
            
            var obj = PlayerManager.Instance.interactingPlayer.GetFstInteractObj();
            
            if (obj == null)
                return;
            
            bool interactionEnd = await obj.InteractAsync();
            
            if (obj == null)                              
                return;
            
            if (interactionEnd)
            {
                obj.SetActiveNotify(!obj.DisableAfterInteract);
                
                SLManager.Instance.OnNotify(!obj.DisableAfterInteract, obj.Idx);
                PlayerManager.Instance.interactingPlayer.ClearScriptableObjList();
            }
        }
    }
}
