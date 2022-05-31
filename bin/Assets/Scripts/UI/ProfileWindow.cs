using UnityEngine;
using UnityEngine.UI;

public class ProfileWindow : WindowObject
{
    [SerializeField] private Slot equipedItemSlot;
    [SerializeField] private Image profileImage;

    [SerializeField] private int equipedItemIdx = -1;
    //[SerializeField] private int currentStatus = -1;

    private void updateEquipedSlot()
    {
        equipedItemSlot.SetSlotStatus(false, false);
    }

    public void UpdateEquipItem(int itemIdx)
    {
        equipedItemIdx = itemIdx;
        if (equipedItemIdx >= 0)
            equipedItemSlot.SetImage(InventoryManager.Instance.GetItem(itemIdx).spriteName);
        else
            equipedItemSlot.SetImage(null);
    }

    public override void Activate()
    {
        return;
    }
}
