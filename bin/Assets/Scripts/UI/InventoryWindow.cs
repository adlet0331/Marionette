using System;
using System.Collections.Generic;
using DataBaseScripts;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryWindow : WindowObject
    {
        [SerializeField] private GridLayoutGroup gridLayoutGroup;
        [SerializeField] private InventorySlot detailSlot;
        [SerializeField] private InventorySlot[] inventorySlots;
        [SerializeField] private int maxColumn;
        [SerializeField] private int maxRow;
        [SerializeField] private int _currentColumn;
        [SerializeField] private int _cururentRow;
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

        public override void Activate()
        {
            OpenWindow();
        }

        public void ArrowInput(DirectionArrowType arrowType)
        {
            if (arrowType == DirectionArrowType.Left)
            {
                if (_currentColumn != 0)
                    currentColumn -= 1;
            }
            else if (arrowType == DirectionArrowType.Right)
            {
                if (currentColumn != maxColumn - 1)
                    currentColumn += 1;
            }
            else if (arrowType == DirectionArrowType.Up)
            {
                if (cururentRow != 0)
                    cururentRow -= 1;
            }
            else if (arrowType == DirectionArrowType.Down)
            {
                if (cururentRow != maxRow - 1)
                    cururentRow += 1;
            }
        }
    }
}
