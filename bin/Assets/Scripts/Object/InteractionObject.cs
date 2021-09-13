using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField] protected bool canInteract;
    [SerializeField] private int idx;

    public int GetIdx()
    {
        if (canInteract)
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
