using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
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
    public void SetImage(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
        return;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
