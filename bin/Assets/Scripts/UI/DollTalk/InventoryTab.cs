using System;
using System.Collections.Generic;
using System.IO;
using DataBaseScripts;
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
            }
        }
        public override void OpenTab()
        {
            gameObject.SetActive(true);
            WindowManager.Instance.dollTalkWindow.SetChatText(false, "");
            inventorySlots[currentIndex].SetAvaliableBoard(true);
            isChooseAvaliable = false;

            foreach (var slot in inventorySlots)
            {
                slot.SetSprite(null);
            }
            var itemList = InventoryManager.Instance.GetItemList();
            for (int i = 0; i < itemList.Count; i++)
            {
                var sprite = Resources.Load<Sprite>(Path.Combine("Sprites", "Items", itemList[i].spriteName));
                inventorySlots[i].SetSprite(sprite);
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
                        dollTalkSelectionWindow.GetInput(InputType.Up);
                        return;
                    }
                    if (cururentRow != 0)
                        cururentRow -= 1;
                    return;
                case InputType.Down:
                    if (isChooseAvaliable)
                    {
                        dollTalkSelectionWindow.GetInput(InputType.Down);
                        return;
                    }
                    if (cururentRow != maxRow - 1)
                        cururentRow += 1;
                    return;
                case InputType.Space:
                    if (isChooseAvaliable)
                    {
                        dollTalkSelectionWindow.GetInput(InputType.Space);
                        return;
                    }

                    if (InventoryManager.Instance.GetItemList().Count <= currentIndex)
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
