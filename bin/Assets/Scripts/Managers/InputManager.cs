using System;
using System.Collections.Generic;
using UnityEngine;
using static InteractionObject;

/* 
 * 플레이어의 KeyBoard 입력을 받아서 처리하는 매니저
 * 
 */
public class InputManager : Singleton<InputManager>
{
    [SerializeField] private bool isMoveable = true;
    [SerializeField] private bool isInputAvaliable = true;
    [SerializeField] private bool isInventoryWindowOn = false;
    [SerializeField] private bool isItemSelectionPannelOn = false;
    [SerializeField] private int moveH, moveV;
    [SerializeField] private bool isRun, isSlowWalk;
    [SerializeField] private MovingObject playerMovingComponent;
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
        var characterWorldPos = characterTransform.localToWorldMatrix * characterTransform.localPosition;

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

        if (isInputAvaliable && Input.anyKeyDown) {
            foreach (var dic in keyDictionary) {
                if (Input.GetKeyDown(dic.Key))
                    dic.Value();
            }
        }

        if (!isMoveable && isInputAvaliable && isItemSelectionPannelOn)
        {
            int moveInt = 0;
            if (Input.GetKeyDown(KeyCode.UpArrow))
                moveInt -= 1;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                moveInt += 1;

            WindowManager.Instance.inventoryWindow.MoveEquipWindowIdx(moveInt);
        }

        else if (!isMoveable && isInputAvaliable && isInventoryWindowOn)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(1);
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(-1);
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(-2);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(2);
        }

    }

    private void OpenSettings() {
        WindowManager.Instance.settingWindow.Activate();
    }
    private void Interact() {
        if (WindowManager.Instance.scriptWindow.gameObject.activeSelf)
        {
            WindowManager.Instance.scriptWindow.Activate();
        }

        InteractionObject obj = PlayerManager.Instance.playerInteracting.GetFstInteractObj();
        InteractionObjectType type = obj.objectType;

        obj.Interact();
    }
    private void TalkWithDoll()
    {
        WindowManager.Instance.inventoryWindow.Activate();
    }

    private void SettingTabChange()
    {
        if (WindowManager.Instance.settingWindow.gameObject.activeSelf)
        {
            WindowManager.Instance.settingWindow.tabInput();
        }
    }
}
