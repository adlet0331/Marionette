using System.IO;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryTab : ADollTalkWindowTab
    {
        [Header("Need to be Initialized")]
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private InventorySlot[] inventorySlots;
        
        [Header("Change In Game")]
        [SerializeField] private int maxColumn;
        [SerializeField] private int maxRow;
        [SerializeField] private int _currentColumn;
        [SerializeField] private int _cururentRow;
        [SerializeField] private bool isChooseAvaliable;
        private void Awake()
        {
            maxRow = gridLayoutGroup.constraintCount;
            maxColumn = inventorySlots.Length / maxRow;
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
                inventorySlots[currentIndex].SetAvaliableBoard(false);
                _currentColumn = value;
                inventorySlots[currentIndex].SetAvaliableBoard(true);
                updateInventorySlots(currentIndex);
            }
        }
        private int cururentRow
        {
            get => _cururentRow;
            set
            {
                inventorySlots[currentIndex].SetAvaliableBoard(false);
                _cururentRow = value;
                inventorySlots[currentIndex].SetAvaliableBoard(true);
                updateInventorySlots(currentIndex);
            }
        }

        private void updateInventorySlots(int currentIndex)
        {
            var itemList = GamePlayManager.Instance.InventoryItemList;
            if (currentIndex >= itemList.Count)
            {
                GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(false, "");
                return;
            }
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(false, itemList[currentIndex].itemInfo);
        }
        public override void OpenTab()
        {
            gameObject.SetActive(true);
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.SetChatText(false, "");
            GamePlayManager.Instance.WindowsInstances.dollTalkWindow.ActivateNext(false);
            inventorySlots[currentIndex].SetAvaliableBoard(true);
            isChooseAvaliable = false;

            foreach (var slot in inventorySlots)
            {
                slot.SetSprite(null);
            }
            var itemList = GamePlayManager.Instance.InventoryItemList;
            for (int i = 0; i < itemList.Count; i++)
            {
                var sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Items", itemList[i].spriteName));
                inventorySlots[i].SetSprite(sprite);
            }

            updateInventorySlots(currentIndex);
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
                                    // TODO 아이템 별 사용 코드
                                }
                                break;
                        }
                        return;
                    }

                    if (GamePlayManager.Instance.InventoryItemList.Count <= currentIndex)
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
