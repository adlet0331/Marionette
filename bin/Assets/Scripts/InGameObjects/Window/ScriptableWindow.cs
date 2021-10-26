using UnityEngine;
using UnityEngine.UI;

/* ��ȣ�ۿ� ������ ���� ����� ������Ʈ ���� ����ִ� â
 * PlayerManager���� ȣ��
 * 
 * �Ʒ� 2�� ���
 * - ������Ʈ �̸�
 * - InGameObjectManager index = 0 �� String
 */
public class ScriptableWindow : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Text explanationText;
    [SerializeField] private int currentIdx;

    public bool SetInteractionObject(int idx)
    {
        currentIdx = idx;
        ScriptableObjData currentObject = ScriptObjDataManager.Instance.ScriptObjDataList[idx];
        bool isDummy = false;
        if(currentObject == null)
        {
            currentObject = ScriptObjDataManager.Instance.ScriptObjDataList[0];
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
