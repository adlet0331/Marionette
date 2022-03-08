using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionPannel : MonoBehaviour
{
    [SerializeField] private bool isEquip;
    [SerializeField] private int itemInvenIdx;
    [SerializeField] private int currentSelection;
    [SerializeField] private Text EquipOrUn; 
    [SerializeField] private GameObject AvalibleImage1;
    [SerializeField] private GameObject AvalibleImage2;
    [SerializeField] private GameObject AvalibleImage3;

    public void UpdateUI(int currentSelection)
    {
        AvalibleImage1.gameObject.SetActive(false);
        AvalibleImage2.gameObject.SetActive(false);
        AvalibleImage3.gameObject.SetActive(false);

        if (currentSelection == 0)
        {
            AvalibleImage1.gameObject.SetActive(true);
        }
        else if (currentSelection == 1)
        {
            AvalibleImage2.gameObject.SetActive(true);
        }
        else if (currentSelection == 2)
        {
            AvalibleImage3.gameObject.SetActive(true);
        }

        this.currentSelection = currentSelection;
        return;
    }

    public void OpenPannel(bool bl, int itemInventoryIdx)
    {
        isEquip = bl;
        itemInvenIdx = itemInventoryIdx;

        setEquipText();
        
    }

    public void Interact(int idx)
    {
        if (idx == 0)
        {
            BtnFirst_Equip();
        }
        else if (idx == 1)
        {
            BtnSecond_SeeClose();
        }
        else if (idx == 2)
        {
            BtnThird_Close();
        }
    }

    public void BtnFirst_Equip()
    {

    }

    public void BtnSecond_SeeClose()
    {

    }

    public void BtnThird_Close()
    {

    }

    private void setEquipText()
    {
        if (isEquip)
            EquipOrUn.text = "장착하기";
        else
            EquipOrUn.text = "해제하기";
        return;
    }
}
