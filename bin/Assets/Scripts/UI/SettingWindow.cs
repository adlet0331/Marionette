using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 사용자 설정 창 (아니마 도감, 스토리 진행도, 게임 설정). ESC 누르면 나옴
 */
public class SettingWindow : WindowObject
{

    [SerializeField]
    private int currentWindow;
    [SerializeField]
    private GameObject Object_AnimaLibrary;
    [SerializeField]
    private GameObject Object_StoryLine;
    [SerializeField]
    private GameObject Object_Settings;

    public override void Activate()
    {
        if (gameObject.activeSelf)
            this.CloseWindow();
        else
            this.OpenWindow();
    }

    public void tabInput()
    {
        if (currentWindow <= 2)
            currentWindow += 1;
        else 
            currentWindow = 1;
        

        changeWindow(currentWindow);
    }
    private void changeWindow(int cur)
    {
        Object_AnimaLibrary.active = false;
        Object_StoryLine.active = false;
        Object_Settings.active = false;

        if (cur == 1) {
            Object_AnimaLibrary.active = true;
            currentWindow = 1;
            Debug.Log("1");
        } else if (cur == 2) {
            Object_StoryLine.active = true;
            currentWindow = 2;
            Debug.Log("2");
        } else if (cur == 3) {
            Object_Settings.active = true;
            currentWindow = 3;
            Debug.Log(3);
        }

    }
    public void changeTab1()
    {
        currentWindow = 1;
        changeWindow(1);
    }

    public void changeTab2()
    {
        currentWindow = 2;
        changeWindow(2);
    }

    public void changeTab3()
    {
        currentWindow = 3;
        changeWindow(3);
    }
    
}
