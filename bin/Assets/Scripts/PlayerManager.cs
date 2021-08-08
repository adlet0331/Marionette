using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject {

    private Dictionary<KeyCode, Action> keyDictionary;
    private int moveH, moveV;
    private bool keyShift;

    void Start() {
        keyDictionary = new Dictionary<KeyCode, Action> {
            { KeyCode.A, KeyDown_A },
            { KeyCode.B, KeyDown_B },
        };
    }

    void Update() {
        if (Input.anyKeyDown) 
            foreach (var dic in keyDictionary){
                if (Input.GetKeyDown(dic.Key)) dic.Value();
            }
        moveH = (int) Input.GetAxisRaw("Horizontal");
        moveV = (int) Input.GetAxisRaw("Vertical");
        keyShift = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        this.Move(moveH, moveV, keyShift);
    }

    private void KeyDown_A() {
        Debug.Log("A");
    }
    private void KeyDown_B() {
        Debug.Log("B");
    }
}
