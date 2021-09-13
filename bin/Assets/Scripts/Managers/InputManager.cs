using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private bool isMoveable = true;
    public bool IsMoveable {
        get { return isMoveable; }
        set { isMoveable = value; }
    }

    [SerializeField] private int moveH, moveV;
    [SerializeField] private bool isRun, isSlowWalk;
    [SerializeField] private Dictionary<KeyCode, Action> keyDictionary;
    [SerializeField] private MovingObject movingComponent;

    private void Start() {
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

        if (Input.anyKeyDown) {
            foreach (var dic in keyDictionary) {
                if (Input.GetKeyDown(dic.Key))
                    dic.Value();
            }
        }
    }

    private void KeyDown_ESC() {
        Debug.Log("ESC");
        if (WindowManager.Instance.settingWindow.gameObject.activeSelf) {
            WindowManager.Instance.settingWindow.gameObject.SetActive(false);
        }
        else {
            WindowManager.Instance.settingWindow.gameObject.SetActive(true);
        }
    }
    private void KeyDown_Z() {
        
    }
}
