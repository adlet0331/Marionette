using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ProfileWindow : WindowObject
    {
        [SerializeField] private Slot equipedItemSlot;
        [SerializeField] private Image profileImage;

        [SerializeField] private int equipedItemIdx = -1;
        [SerializeField] private string currentSpriteName;

        public void UpdateEquipItem(int itemIdx)
        {
            equipedItemIdx = itemIdx;
            if (equipedItemIdx >= 0)
                equipedItemSlot.SetImage(GamePlayManager.Instance.GetItemDataWithIdx(itemIdx).spriteName);
            else
                equipedItemSlot.SetImage(null);
        }

        public override void Activate()
        {
            this.OpenWindow();
        }
    }
}
