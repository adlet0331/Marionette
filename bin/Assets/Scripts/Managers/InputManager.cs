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
    [SerializeField] private bool isMoveable = false;
    [SerializeField] private bool isInputAvaliable = true;
    [SerializeField] private bool isInventoryWindowOn = false;
    [SerializeField] private bool isItemSelectionPannelOn = false;
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
    [SerializeField] private MovingObject movingComponent;
    public void SetMovingComponent(MovingObject mo)
    {
        movingComponent = mo;
    }
    [SerializeField] private int moveH, moveV;
    [SerializeField] private bool isRun, isSlowWalk;
    [SerializeField] private Dictionary<KeyCode, Action> keyDictionary;
    private void Start() {
        isInputAvaliable = true;
        keyDictionary = new Dictionary<KeyCode, Action> {
            { KeyCode.Z, KeyDown_Z },
            { KeyCode.Escape, KeyDown_ESC },
            { KeyCode.X, KeyDown_X }, 
        };
    }

    private void Update() {
        if (movingComponent == null)
            return;

        if (isMoveable) {
            moveH = (int)Input.GetAxisRaw("Horizontal");
            moveV = (int)Input.GetAxisRaw("Vertical");
            isRun = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            isSlowWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            movingComponent.Move(moveH, moveV, isRun, isSlowWalk);
        }
        else
        {
            movingComponent.Move(0, 0, false, false);
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
            int moveInt = 0;
            if (Input.GetKeyDown(KeyCode.RightArrow))
                moveInt += 1;
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
                moveInt -= 1;
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                moveInt -= 2;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                moveInt += 2;

            WindowManager.Instance.inventoryWindow.MoveInventoryUIdx(moveInt);
        }

    }

    private void KeyDown_ESC() {
        if (WindowManager.Instance.settingWindow.gameObject.activeSelf)
        {
            WindowManager.Instance.settingWindow.CloseWindow();
            return;
        }
        WindowManager.Instance.settingWindow.Activate();
    }
    private void KeyDown_Z() {
        if (WindowManager.Instance.inventoryWindow.gameObject.activeSelf)
        {
            WindowManager.Instance.inventoryWindow.PressInteract();
            return;
        }

        InteractionObject obj = PlayerManager.Instance.playerInteracting.GetFstInteractObj();
        InteractionObjectType type = obj.objectType;

        obj.Interact();

        return;
    }
    private void KeyDown_X()
    {
        WindowManager.Instance.inventoryWindow.Activate();
    }
}
