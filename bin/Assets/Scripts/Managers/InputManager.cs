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
    public void SetOptions(bool isMv, bool isAv)
    {
        isMoveable = isMv;
        isInputAvaliable = isAv;
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
        if (isMoveable && movingComponent != null) {
            moveH = (int)Input.GetAxisRaw("Horizontal");
            moveV = (int)Input.GetAxisRaw("Vertical");
            isRun = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            isSlowWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            movingComponent.Move(moveH, moveV, isRun, isSlowWalk);
        }
        if (isInputAvaliable && Input.anyKeyDown) {
            foreach (var dic in keyDictionary) {
                if (Input.GetKeyDown(dic.Key))
                    dic.Value();
            }
        }
    }

    private void KeyDown_ESC() {
        Debug.Log("ESC");
        WindowManager.Instance.settingWindow.Activate();
    }
    private void KeyDown_Z() {
        Debug.Log("Z");

        InteractionObject obj = PlayerManager.Instance.playerInteractObject.GetFstInteractObj();
        InteractionObjectType type = obj.objectType;

        PlayerManager.Instance.playerInteractObject.InteractWithObject();

        return;
    }
    private void KeyDown_X()
    {
        Debug.Log("X");
        WindowManager.Instance.inventoryWindow.Activate();
    }
}
