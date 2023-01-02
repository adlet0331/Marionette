using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] bool isInteractable;
        [SerializeField] bool isSelected;
        [SerializeField] bool isEquiped;

        [SerializeField] private int index;

        [SerializeField] Image itemImage;
        [SerializeField] Image isSelectedImage;
        [SerializeField] Image isEquippedImage;

        public void SetSlotStatus(bool isSelected, bool isEquiped)
        {
            this.isSelected = isSelected;
            this.isEquiped = isEquiped;

            if (isSelected)
                isSelectedImage.gameObject.SetActive(true);
            else
                isSelectedImage.gameObject.SetActive(false);

            if (isEquiped)
                isEquippedImage.gameObject.SetActive(true);
            else
                isEquippedImage.gameObject.SetActive(false);
        }
        public void SetImage(string spriteName)
        {
            if(spriteName == null){
                itemImage.sprite = null;
                return;
            }
            string path = "Items/" + spriteName;
            Sprite sprite = Resources.Load<Sprite>(path);
            itemImage.sprite = sprite;
            return;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isInteractable) return;
            throw new System.NotImplementedException();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isInteractable) return;
            throw new System.NotImplementedException();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!isInteractable) return;
            throw new System.NotImplementedException();
        }
    }
}
