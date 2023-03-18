using System.IO;
using Managers;
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
            var stellaInfoDictionary = GamePlayManager.Instance.StellaInfoDictionary;
            return stellaInfoDictionary.ContainsKey(index);
        }
        private void updateStellaSlotsSelection(int currentIndex)
        {
            var stellaInfoDictionary = GamePlayManager.Instance.StellaInfoDictionary;
            
            if (checkStellaAvaliable(currentIndex))
            {
                var stellaInfo = stellaInfoDictionary[currentIndex];
                GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(false, stellaInfo.descriptionList[stellaInfo.level]);
            }
            else
            {
                GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(false, "");
            }
        }
        public override void OpenTab()
        {
            currentColumn = 0;
            cururentRow = 0;
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.ActivateNext(false);
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(false, "");
            gameObject.SetActive(true);
            var stellaInfoList = GamePlayManager.Instance.StellaInfoDictionary;

            for (int i = 1; i <= stellaSlots.Length; i++)
            {
                var av = checkStellaAvaliable(i);
                if (av)
                {
                    stellaSlots[i].SetStellaImage(Resources.Load<Sprite>(
                        Path.Combine("Sprites", "Stella", stellaInfoList[i].spriteName)));
                }
            }
        }

        public override void CloseTab()
        {
            gameObject.SetActive(false);
        }

        public override void GetInput(InputType input)
        {
            var dollTalkSelectionWindow = GamePlayManager.Instance.WindowsInstances.dollTalkSelectionWindowInnerTab;
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
                                var itemList = GamePlayManager.Instance.InventoryItemList;
                                if (!itemList[currentIndex].isUsable)
                                {
                                    GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(false, "사용할 수 없는 아이템이야...");
                                }
                                else
                                {
                                    // TODO : 아이템 사용 처리
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