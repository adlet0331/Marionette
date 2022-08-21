using System.Collections.Generic;
using System.IO;
using Managers;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StellaTab : ADollTalkWindowTab
    {
        [Header("Need to be Initialized")]
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private StellaSlot[] stellaSlots;
        [Header("Change In Game")]
        [SerializeField] private int maxColumn;
        [SerializeField] private int maxRow;
        [SerializeField] private int _currentColumn;
        [SerializeField] private int _cururentRow;
        [SerializeField] private bool isChooseAvaliable;
        private void Awake()
        {
            maxRow = gridLayoutGroup.constraintCount;
            maxColumn = stellaSlots.Length / maxRow;
            _currentColumn = 0;
            _cururentRow = 0;
            isChooseAvaliable = false;
        }
        private int currentIndex
        {
            get => _currentColumn + _cururentRow * maxColumn;
        }
        private int currentColumn
        {
            get => _currentColumn;
            set
            {
                stellaSlots[currentIndex].SetSelected(false);
                _currentColumn = value;
                stellaSlots[currentIndex].SetSelected(true);
                updateStellaSlotsSelection(currentIndex);
            }
        }
        private int cururentRow
        {
            get => _cururentRow;
            set
            {
                stellaSlots[currentIndex].SetSelected(false);
                _cururentRow = value;
                stellaSlots[currentIndex].SetSelected(true);
                updateStellaSlotsSelection(currentIndex);
            }
        }

        private bool checkStellaAvaliable(int index)
        {
            var stellaInfoList = StellaManager.Instance.GetStellaInfoList();
            if (index >= stellaInfoList.Count - 1 ||
                stellaInfoList[index + 1].level == 0)
            {
                return false;
            }
            return true;
        }
        private void updateStellaSlotsSelection(int currentIndex)
        {
            var stellaInfoList = StellaManager.Instance.GetStellaInfoList();
            
            if (checkStellaAvaliable(currentIndex))
            {
                var info = stellaInfoList[currentIndex + 1];
                WindowManager.Instance.dollTalkWindow.SetChatText(false, info.descriptionList[info.level - 1]);
            }
            else
            {
                WindowManager.Instance.dollTalkWindow.SetChatText(false, "");
            }
        }
        public override void OpenTab()
        {
            currentColumn = 0;
            cururentRow = 0;
            WindowManager.Instance.dollTalkWindow.ActivateNext(false);
            WindowManager.Instance.dollTalkWindow.SetChatText(false, "");
            gameObject.SetActive(true);
            var stellaInfoList = StellaManager.Instance.GetStellaInfoList();

            for (int i = 0; i < stellaSlots.Length; i++)
            {
                var av = checkStellaAvaliable(i);
                if (av)
                {
                    stellaSlots[i].SetStellaImage(Resources.Load<Sprite>(
                        Path.Combine("Sprites", "Stella", stellaInfoList[i+1].spriteName)));
                }
            }
        }

        public override void CloseTab()
        {
            gameObject.SetActive(false);
        }

        public override void GetInput(InputType input)
        {
            var dollTalkSelectionWindow = WindowManager.Instance.dollTalkSelectionWindowInnerTab;
            switch (input)
            {
                case InputType.Left:
                    if (isChooseAvaliable)
                        return;
                    if (_currentColumn != 0)
                        currentColumn -= 1;
                    return;
                case InputType.Right:
                    if (isChooseAvaliable)
                        return;
                    if (currentColumn != maxColumn - 1)
                        currentColumn += 1;
                    return;
                case InputType.Up:
                    if (isChooseAvaliable)
                    {
                        dollTalkSelectionWindow.GetInputIdx(InputType.Up);
                        return;
                    }
                    if (cururentRow != 0)
                        cururentRow -= 1;
                    return;
                case InputType.Down:
                    if (isChooseAvaliable)
                    {
                        dollTalkSelectionWindow.GetInputIdx(InputType.Down);
                        return;
                    }
                    if (cururentRow != maxRow - 1)
                        cururentRow += 1;
                    return;
                case InputType.Space:
                    if (isChooseAvaliable)
                    {
                        var selectionIdx = dollTalkSelectionWindow.GetInputIdx(InputType.Space);
                        switch (selectionIdx)
                        {
                            case 0:
                                dollTalkSelectionWindow.Close();
                                isChooseAvaliable = false;
                                break;
                            case 1:
                                dollTalkSelectionWindow.Close();
                                isChooseAvaliable = false;
                                var itemList = InventoryManager.Instance.GetItemList();
                                if (!itemList[currentIndex].isUsable)
                                {
                                    WindowManager.Instance.dollTalkWindow.SetChatText(false, "사용할 수 없는 아이템이야...");
                                }
                                else
                                {
                                    // TODO 아이템 별 사용 코드
                                }
                                break;
                        }
                        return;
                    }

                    if (!checkStellaAvaliable(currentIndex))
                        return;
                    dollTalkSelectionWindow.OpenWithType(typePerSelectionStrings);  
                    isChooseAvaliable = true;
                    return;
                default:
                    return;
            }
        }
    }
}