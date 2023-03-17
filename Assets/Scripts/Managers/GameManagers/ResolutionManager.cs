using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* �ػ� ����
* 
*/
namespace Managers
{
    public class ResolutionManager : Singleton<ResolutionManager>
    {
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Button resolutionButton;
        [SerializeField] private Camera currentCamera;
        [SerializeField] private int currIndex;
    
        private List<Resolution> resolutionList;
    
        private void Start() {
            resolutionList = new List<Resolution>();

            //���߿� ������ ����?
            foreach (Resolution item in Screen.resolutions)
            {
                if (item.refreshRate == 60 || true)
                {
                    //Debug.Log(item.refreshRate);
                    resolutionList.Add(item);
                }
            }

            // int optionNum = 0;
            // resolutionDropdown.options.Clear();
            // foreach (Resolution item in resolutionList)
            // {
            //     Dropdown.OptionData option = new Dropdown.OptionData();
            //     option.script = item.width + "x" + item.height;
            //     resolutionDropdown.options.Add(option);
            // 
            //     if (item.width == Screen.width && item.height == Screen.height)
            //     {
            //         resolutionDropdown.value = optionNum;
            //         currIndex = optionNum;
            //     }
            //     optionNum++;
            // }
            // 
            // resolutionDropdown.RefreshShownValue();
        }
        public void DropboxOptionChange(int index)
        {
            currIndex = index;
        }

        public void ExeBtnClick()
        {
            Screen.SetResolution(resolutionList[currIndex].width,
                resolutionList[currIndex].height,
                false,
                resolutionList[currIndex].refreshRate);
        }
    }
}
