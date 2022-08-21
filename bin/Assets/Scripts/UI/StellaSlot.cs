using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class StellaSlot : MonoBehaviour
    {
        [SerializeField] private Image stellaImage;
        [SerializeField] private GameObject selectedBoard;
        [SerializeField] private Slider expSlider;

        public void SetSelected(bool isSelect)
        {
            selectedBoard.SetActive(isSelect);
        }

        public void SetCurrentExp(float value)
        {
            expSlider.value = value;
        }

        public void SetStellaImage(Sprite sprite)
        {
            if (!sprite)
            {
                stellaImage.gameObject.SetActive(false);
                return;
            }
            stellaImage.gameObject.SetActive(true);
            stellaImage.sprite = sprite;
        }
    }
}

