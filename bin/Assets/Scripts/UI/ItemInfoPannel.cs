using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPannel : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text nameText;

    public void OpenWindow(string spriteName, string str, string name)
    {
        gameObject.SetActive(true);
        //itemImage.sprite = itemSprite;
        descriptionText.text = str;
        nameText.text = name;

        return;
    }
}
