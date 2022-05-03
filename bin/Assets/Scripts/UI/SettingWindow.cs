using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 볼륨 및 해상도 설정 창. ESC 누르면 나옴
 */
public class SettingWindow : WindowObject
{
    public override void Activate()
    {
        if (gameObject.activeSelf)
            this.CloseWindow();
        else
            this.OpenWindow();
    }
}
