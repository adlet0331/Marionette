using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 아이템을 얻었을 때 나오는 창
 * 
 * 5초 동안 냅두면 닫힘
 * 
 */
public class ItemGotWindow : WindowObject
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text infoText;
    public override void Activate()
    {
        return;
    }
}
