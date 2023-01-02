using UI;
using UnityEngine;
using UnityEngine.UI;

public class SLSlot : MonoBehaviour
{
    [SerializeField] private Text slotSaveNum;
    [SerializeField] private Text slotPlace;
    [SerializeField] private Text slotChapterNum;
    [SerializeField] private Text slotTime;
    public int slotIndex;

    public void SetSaveInfos(SaveInfo info)
    {
        slotPlace.text = info.sceneString;
        slotTime.text = info.timePassed.ToString();
    }
}
