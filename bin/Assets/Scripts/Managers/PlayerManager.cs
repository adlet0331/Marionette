using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject {

    public static PlayerManager instance;
    #region Singleton
    private void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
        }
        else {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }
    #endregion Singleton
    
    [SerializeField] GameObject settingWindow;
    [SerializeField] GameObject currentCharacter;

    private InputManager inputManager;
    private Dictionary<KeyCode, Action> keyDictionary;
    private int moveH, moveV;
    private bool isRun;

    void Start() {
        keyDictionary = new Dictionary<KeyCode, Action> {
            { KeyCode.Escape, KeyDown_Esc },
        };
        inputManager = new InputManager(keyDictionary, true);
        this.SetCanMove(true);
    }

    void Update() {
        if (Input.anyKeyDown)
            inputManager.Update();
        moveH = (int) Input.GetAxisRaw("Horizontal");
        moveV = (int) Input.GetAxisRaw("Vertical");
        isRun = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        if (moveH == 0 && moveV == 0){
            this.StopMove();
        }
        else {
            this.Move(moveH, moveV, isRun);
        }
    }

    private void KeyDown_Esc() {
        if (settingWindow.activeSelf) {
            settingWindow.SetActive(false);
            this.SetCanMove(true);
        } else {
            settingWindow.SetActive(true);
            this.SetCanMove(false);
        }
    }
    private void KeyDown_Z() {
        Debug.Log("B");
    }
}
