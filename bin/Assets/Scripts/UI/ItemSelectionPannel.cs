using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ItemSelectionPannel : MonoBehaviour
    {
        [SerializeField] private bool isEquip;
        [SerializeField] private int currentSelection;
        [SerializeField] private Text equipOrUn; 
        [SerializeField] private GameObject avaliableImage1;
        [SerializeField] private GameObject avaliableImage2;
        [SerializeField] private GameObject avaliableImage3;

        public void UpdateUI(int currentSelection)
        {
            avaliableImage1.gameObject.SetActive(false);
            avaliableImage2.gameObject.SetActive(false);
            avaliableImage3.gameObject.SetActive(false);

            if (currentSelection == 0)
            {
                avaliableImage1.gameObject.SetActive(true);
            }
            else if (currentSelection == 1)
            {
                avaliableImage2.gameObject.SetActive(true);
            }
            else if (currentSelection == 2)
            {
                avaliableImage3.gameObject.SetActive(true);
            }

            this.currentSelection = currentSelection;
            return;
        }

        public void UpdatePannel(bool bl)
        {
            isEquip = bl;

            setEquipText();
        }

        private void setEquipText()
        {
            if (isEquip)
                equipOrUn.text = "장착";
            else
                equipOrUn.text = "해제";
            return;
        }
    }
}
