using UnityEngine;
using UnityEngine.UI;

public class InteractionWindow : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text explanationText;
    [SerializeField] private int currentIdx;

    public bool SetInteractionObject(int idx)
    {
        currentIdx = idx;
        InGameObject currentObject = InGameObjectManager.Instance.ImGameObjectList[idx];
        bool isDummy = false;
        if(currentObject == null)
        {
            currentObject = InGameObjectManager.Instance.ImGameObjectList[0];
            isDummy = true;
        }
        nameText.text = currentObject.name;
        explanationText.text = currentObject.scripts[0];

        if (isDummy)
        {
            return false;
        }
        return true;
    }
}
