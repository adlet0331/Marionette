using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UI;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class ChooseControl : IInteractionObjectWithUI<ChooseData, ChooseUIData>
    {
        protected override void GetUIWindowAndInit()
        {
            UIData.scriptList = new List<string>();
            UIData.interactingObjectList = new List<InteractingObject>();
            UIWindow = WindowManager.Instance.chooseWindow;
        }
        public override async UniTask<bool> Interact()
        {
            UIData.scriptList.Clear();
            for(int i = 0; i < data.scriptList.Count; i++)
            {
                UIData.scriptList.Add(data.scriptList[i]);
                UIData.interactingObjectList.Add(data.interactionGameObjectList[i]);
            }
            UIWindow.InteractWithData(UIData);
            return false;
        }
    }
}