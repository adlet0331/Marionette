using UnityEngine;

/* Interact 되는 객체
 * 
 * Trigger check 한 Collider에 부딛히면 InteractionList에 추가
 * 
 * Z 로 Interact 발동
 * 
 */
public abstract class InteractionObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInteractable)
            return;
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerInteractObject.AddInteractionList(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Instance.playerInteractObject.RemoveInteractionObj(this);
        }
    }
    public enum Type
    {
        ScriptableObject = 0,
        ItemableObject = 1,
    }
    // 스크립트가 있는가 (대화창이 뜨는가)
    [SerializeField] public bool isInteractable;
    [SerializeField] public Type objectType;
    // 데이터 인덱스 (없으면 -1)
    [SerializeField] private int idx;
    public int GetIdx()
    {
        if (isInteractable)
        {
            return idx;
        }
        else
        {
            return -1;
        }
    }

    public abstract void Interact();
}
