using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D[] objectBoxColliders;
    [SerializeField] private bool canInteract;

    private void Awake() {
        objectBoxColliders = this.GetComponents<BoxCollider2D>();
    }

    public abstract void Interact();
}
