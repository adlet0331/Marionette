using System;
using System.Collections.Generic;
using DataBaseScripts;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryTab : ADollTalkWindowTab
    {
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private InventorySlot detailSlot;
        [SerializeField] private InventorySlot[] inventorySlots;
        
        [SerializeField] private int maxColumn;
        [SerializeField] private int maxRow;
        [SerializeField] private int _currentColumn;
        [SerializeField] private int _cururentRow;
        public override void OpenTab()
        {
            
        }
        public override void GetInput(InputType input)
        {
            switch (input)
            {
                case InputType.Left:
                    if (_currentColumn != 0)
                        currentColumn -= 1;
                    return;
                case InputType.Right:
                    if (currentColumn != maxColumn - 1)
                        currentColumn += 1;
                    return;
                case InputType.Up:
                    if (cururentRow != 0)
                        cururentRow -= 1;
                    return;
                case InputType.Down:
                    if (cururentRow != maxRow - 1)
                        cururentRow += 1;
                    return;
                case InputType.Space:
                    
                default:
                    return;
            }
        }
        private int currentColumn
        {
            get => _currentColumn;
            set
            {
                inventorySlots[_currentColumn + _cururentRow * maxColumn].SetAvaliableBoard(false);
                _currentColumn = value;
                inventorySlots[_currentColumn + _cururentRow * maxColumn].SetAvaliableBoard(true);
                detailSlot.SetSprite(inventorySlots[_currentColumn + _cururentRow * maxColumn].GetSprite());
            }
        }
        private int cururentRow
        {
            get => _cururentRow;
            set
            {
                inventorySlots[_currentColumn + _cururentRow * maxColumn].SetAvaliableBoard(false);
                _cururentRow = value;
                inventorySlots[_currentColumn + _cururentRow * maxColumn].SetAvaliableBoard(true);
                detailSlot.SetSprite(inventorySlots[_currentColumn + _cururentRow * maxColumn].GetSprite());
            }
        }

        private void Awake()
        {
            maxRow = gridLayoutGroup.constraintCount;
            maxColumn = inventorySlots.Length / maxRow;
            _currentColumn = 0;
            _cururentRow = 0;
        }
    }
}
