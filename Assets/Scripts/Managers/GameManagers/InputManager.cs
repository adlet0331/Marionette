using System;
using Cysharp.Threading.Tasks;
using InGameObjects.SceneObject.Player;
using UI;
using UnityEngine;

/* 
 * 플레이어의 KeyBoard 입력을 받아서 처리하는 매니저
 * 
 */
namespace Managers
{
    [Serializable]
    public class InputManager : AGameManager
    {
        // 움직임, Keyboard Input
        [SerializeField] private bool isMoveable;
        [SerializeField] private bool isInputAvaliable;
        
        [SerializeField] private bool isInteractable = true;
        
        [SerializeField] private bool isInventoryWindowOn = false;
        [SerializeField] private bool isItemSelectionPannelOn = false;
        [SerializeField] private int moveH, moveV;
        [SerializeField] private bool isRun, isSlowWalk;
        [SerializeField] private PlayerWithHandLight playerMovingComponent;

        [SerializeField] private float mouseCursorWorldX;
        [SerializeField] private float mouseCursorWorldY;
    
        [SerializeField] private float characterWorldX;
        [SerializeField] private float characterWorldY;

        public void SetInputOptions(bool isMv, bool isAv)
        {
            isMoveable = isMv;
            isInputAvaliable = isAv;
        }
        public void SetPlayerControlComponent(PlayerWithHandLight mo)
        {
            playerMovingComponent = mo;
        }
        public Vector2 GetMousePointerInWorld()
        {
            var mouseCursorWorldPos = GamePlayManager.Instance.MouseCursorWorldPosition;
            var characterTransform = playerMovingComponent.transform;
            var characterWorldPos = characterTransform.localPosition;

            mouseCursorWorldX = mouseCursorWorldPos.x;
            mouseCursorWorldY = mouseCursorWorldPos.y;
        
            characterWorldX = characterWorldPos.x;
            characterWorldY = characterWorldPos.y;
        
            return new Vector2(mouseCursorWorldPos.x - characterWorldPos.x, mouseCursorWorldPos.y - characterWorldPos.y);
        }
        
        /// <summary>
        /// FixedUpdate Loop
        /// </summary>
        public void HandlePlayerMoveInput()
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
            
                playerMovingComponent.UpdateHandLightRotate(Vector2.SignedAngle(new Vector2(0, 1), GetMousePointerInWorld()));
            }
            else
            {
                playerMovingComponent.Move(0, 0, false, false);
            }
        }

        public void HandlePlayerInteractInput()
        {
            if (!isInputAvaliable)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.DollTalkWindow":
                        GamePlayManager.Instance.WindowsInstances.dollTalkWindow.Interact(InputType.Space);
                        return;
                    default:
                        PlayerInteraction().Forget();
                        return;
                }
            }
            
            if (Input.GetKeyDown((KeyCode.C)))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.DollTalkWindow":
                        GamePlayManager.Instance.WindowsInstances.dollTalkWindow.Interact(InputType.C);
                        return;
                    default:
                        GamePlayManager.Instance.WindowsInstances.dollTalkWindow.Activate();
                        return;
                }
            }

            if (false && Input.GetKeyDown(KeyCode.Escape))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.SettingWindow":
                        GamePlayManager.Instance.WindowsInstances.settingWindow.CloseWindow();
                        return;
                    default:
                        GamePlayManager.Instance.WindowsInstances.settingWindow.Activate();
                        return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.SettingWindow":
                        GamePlayManager.Instance.WindowsInstances.settingWindow.tabInput();
                        return;
                    default:
                        return;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.DollTalkWindow":
                        GamePlayManager.Instance.WindowsInstances.dollTalkWindow.Interact(InputType.Up);
                        return;
                    default:
                        return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.DollTalkWindow":
                        GamePlayManager.Instance.WindowsInstances.dollTalkWindow.Interact(InputType.Down);
                        return;
                    default:
                        return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.DollTalkWindow":
                        GamePlayManager.Instance.WindowsInstances.dollTalkWindow.Interact(InputType.Left);
                        return;
                    default:
                        return;
                }
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                switch (GamePlayManager.Instance.CurrentOpenWindowType)
                {
                    case "UI.DollTalkWindow":
                        GamePlayManager.Instance.WindowsInstances.dollTalkWindow.Interact(InputType.Right);
                        return;
                    default:
                        return;
                }
            }
        }
        
        public async UniTask PlayerInteraction()
        {
            if (!isInteractable)
                return;
            
            var obj = GamePlayManager.Instance.CurrentInteractObj;
            
            if (obj == null)
                return;
            
            bool interactionEnd = await obj.InteractAsync();
            
            if (obj == null)                              
                return;
            
            if (interactionEnd)
            {
                obj.SetActiveNotify(!obj.DisableAfterInteract);
                GamePlayManager.Instance.ClearInteractingObjList();
            }
        }

        public override void Start()
        {
            
        }
    }
}
