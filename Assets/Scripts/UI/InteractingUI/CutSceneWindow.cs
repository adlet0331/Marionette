using System;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace UI
{
    public class CutSceneUIData
    {
        public string sprite;
        public int milisec;
    }
    public class CutSceneWindow : UIControlWindow<CutSceneUIData>
    {
        [SerializeField] private Transform backBoard;
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
            image.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "CutScene", data.sprite));
            var random = new Random();
            var randomPos = new Vector3(-100 + random.Next(200), -20 + random.Next(40), 0);
            image.transform.localPosition = randomPos;
            backBoard.transform.localPosition = randomPos;
            _showAllImpact(data.milisec).Forget();
        }

        private async UniTask _showAllImpact(int time)
        {
            if (time > 500)
            {
                await _rotateImage(100, 10);
                await _rotateImage(200, -15);
                await _rotateImage(100, 5);
            }
        }

        private async UniTask _rotateImage(int time, int totalAddAngle)
        {
            var intervalTime = (float)time / Math.Abs(totalAddAngle);
            var intervalAngle = totalAddAngle / Math.Abs(totalAddAngle);
            for (int i = 1; i <= Math.Abs(totalAddAngle); i++)
            {
                image.transform.Rotate(new Vector3(0, 0, intervalAngle), Space.Self);
                await UniTask.Delay(TimeSpan.FromMilliseconds(intervalTime / 10));
            }
        }
    }
}