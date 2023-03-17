using System.IO;
using Cysharp.Threading.Tasks;
using DataBaseScripts;
using Managers;
using UI;
using UnityEngine;

namespace InGameObjects.Interaction.InteractingAdditionalObjects
{
    public class StellaControl : ADataInteractionObjectWithUI<StellaControlData, StellaGotUIData>
    {
        [SerializeField] private bool interacted;
        private StellaDataBase stellaControlDataBase;
        protected override void GetUIWindowAndInit()
        {
            interacted = false;
            stellaControlDataBase = GamePlayManager.Instance.dataBaseCollection.stellaDataBase;
            UIWindow = GamePlayManager.Instance.WindowsInstances.stellaGotWindow;
        }
        public override async UniTask<bool> Interact()
        {
            if (!interacted)
            {
                var currentIndex = data.stellaIdx;
                UIData.name = stellaControlDataBase.dataKeyDictionary[currentIndex].name;
                UIData.script = data.getDescription;
                UIData.sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Stella",
                    stellaControlDataBase.dataKeyDictionary[currentIndex].spriteName));
                
                UIWindow.InteractWithData(UIData);
                interacted = true;
                return false;
            }
            else
            {
                UIWindow.CloseWindow();
                interacted = false;
                return true;
            }
        }
    }
}