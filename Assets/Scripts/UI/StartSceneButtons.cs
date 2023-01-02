using Cysharp.Threading.Tasks;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartSceneButtons : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button settingButton;
        private void Start()
        {
            startButton.onClick.AddListener(async () => await NewGameButton());
            loadButton.onClick.AddListener(LoadGameButton);
            settingButton.onClick.AddListener(SettingButton);
        }

        public async UniTask NewGameButton()
        {
            SLManager.Instance.InitSaveData();
            await SceneSwitchManager.Instance.SwitchScene(SceneSwitchManager.SceneName.Girl_room, 0);
        }

        public void LoadGameButton()
        {
            //SLManager.Instance.Load(1);
        }

        public void SettingButton()
        {
            //WindowManager.Instance.settingWindow.Activate();
        }
    }
}
