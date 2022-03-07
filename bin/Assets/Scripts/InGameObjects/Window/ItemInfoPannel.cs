using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPannel : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text descriptionString;

    public void OpenWindow(Sprite itemSprite, string str)
    {
        gameObject.SetActive(true);
        itemImage.sprite = itemSprite;
        descriptionString.text = str;

        return;
    }
}
