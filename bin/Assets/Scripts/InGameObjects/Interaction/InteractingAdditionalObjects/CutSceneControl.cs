using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UI;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class CutSceneControl : ADataInteractionObjectWithUI<CutSceneData, CutSceneUIData>
    {
        [SerializeField] private int currentIndex;
        protected override void GetUIWindowAndInit()
        {
            currentIndex = 0;
            UIWindow = WindowManager.Instance.cutSceneWindow;
        }
        
        public override async UniTask<bool> Interact()
        {
            if (currentIndex >= data.spriteList.Count)
            {
                UIWindow.DeActivate();
                currentIndex = 0;
                return true;
            }
            
            _showImage(currentIndex);
            //await UniTask.Delay(TimeSpan.FromMilliseconds(data.playTimeList[currentIndex]));
            currentIndex++;
            
            return false;
        }

        private void _showImage(int index)
        {
            var uiData = new CutSceneUIData();
            uiData.sprite = data.spriteList[index];
            uiData.milisec = data.playTimeList[index];
            UIWindow.InteractWithData(uiData);
        }
    }
}