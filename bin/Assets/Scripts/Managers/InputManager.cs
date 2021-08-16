using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public bool isActive = true;

    private Dictionary<KeyCode, Action> keyDictionary;
    public InputManager(Dictionary<KeyCode, Action> keyDictionary, bool isActive) {
        this.keyDictionary = keyDictionary;
        this.isActive = isActive;
    }
    public void Update() {
        if (Input.anyKeyDown) {
            foreach (var dic in keyDictionary) {
                if (Input.GetKeyDown(dic.Key))
                    dic.Value();
            }
        }
    }
    public void RefreshInputActive(bool isActive) {
        this.isActive = isActive;
    }
}
