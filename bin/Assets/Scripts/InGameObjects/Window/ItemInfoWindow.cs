using UnityEngine;
using UnityEngine.UI;

public class ItemInfoWindow : WindowObject
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text descriptionString;

    public void OpenWindow(Sprite itemSprite, string str)
    {
        OpenWindow();
        itemImage.sprite = itemSprite;
        descriptionString.text = str;

        return;
    }

    public override void Activate()
    {
        return;
    }
}
