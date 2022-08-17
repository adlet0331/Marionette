using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private GameObject isAvaliableBoard;
        [SerializeField] private Image itemImage;

        public void SetSprite(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }

        public Sprite GetSprite()
        {
            return itemImage.sprite;
        }
        
        public void SetAvaliableBoard(bool isAvaliable)
        {
            isAvaliableBoard.SetActive(isAvaliable);
        }
    }
}