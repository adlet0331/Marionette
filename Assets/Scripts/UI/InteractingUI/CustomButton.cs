using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Image Image;
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite activateSprite;

        private void Start()
        {
            Image = this.GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Image.sprite = activateSprite;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            Image.sprite = defaultSprite;
        }
    }
}