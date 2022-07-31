using System;
using System.Collections;
using System.IO;
using System.Net.Mime;
using DataBaseScripts;
using UnityEngine;
using UnityEngine.UI;

/* 대사 나오는 창
* InputManager에서 호출
* 
* 출력하는 것
* - IngameObject 이름
* - Scripts index = 1 ~ n까지 출력. 버튼 입력 받으면 다음으로 넘어감
* 
*/
namespace UI
{
    [Serializable]
    public struct ScriptUIData
    {
        public string name;
        public string script;
        public Sprite sprite;
    }

    public class ScriptWindow : UIControlWindow<ScriptUIData>
    {
        [SerializeField] private Text name;
        [SerializeField] private Text script;
        [SerializeField] private Image image;

        public override void Activate()
        {
            OpenWindow();
        }

        public override void DeActivate()
        {
            CloseWindow();
        }

        public override void Interact()
        {
            name.text = data.name;
            script.text = data.script;
            image.sprite = data.sprite;
            if (data.sprite == null)
            {
                image.gameObject.SetActive(false);
            }
            else
            {
                image.gameObject.SetActive(true);
            }
        }
    }
}
