using System;
using System.Collections.Generic;
using UnityEngine;

/* 플레이어의 KeyBoard 입력을 받아서 처리하는 매니저
 * 
 */
public class InputManager : Singleton<InputManager>
{
    [SerializeField] private bool isMoveable = false;
    public bool IsMoveable {
        get { return isMoveable; }
        set { isMoveable = value; }
    }
    public void SetInputAvaliable(bool isAv)
    {
        inputAvaliable = isAv;
    }

    [SerializeField] private bool inputAvaliable;
    [SerializeField] private int moveH, moveV;
    [SerializeField] private bool isRun, isSlowWalk;
    [SerializeField] private Dictionary<KeyCode, Action> keyDictionary;
    [SerializeField] private MovingObject movingComponent;

    private void Start() {
        inputAvaliable = true;
        keyDictionary = new Dictionary<KeyCode, Action> {
            { KeyCode.Z, KeyDown_Z },
            { KeyCode.Escape, KeyDown_ESC },
        };
    }

    private void Update() {
        if (isMoveable) {
            moveH = (int)Input.GetAxisRaw("Horizontal");
            moveV = (int)Input.GetAxisRaw("Vertical");
            isRun = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            isSlowWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

            movingComponent.Move(moveH, moveV, isRun, isSlowWalk);
        }

        if (inputAvaliable && Input.anyKeyDown) {
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
        WindowManager.Instance.scriptWindow.Activate();
    }
}
