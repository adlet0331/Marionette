using UnityEngine;

public abstract class InteractionObject : MonoBehaviour
{
    [SerializeField] protected BoxCollider2D[] objectBoxColliders;

    private void Awake() {
        objectBoxColliders = this.GetComponents<BoxCollider2D>();
    }

    public abstract void Interact();

    public bool CheckPlayerCollided() {
        foreach(BoxCollider2D collider in objectBoxColliders) {
            if (collider.IsTouching(PlayerManager.Instance.moveablePlayerCollider)) {
                return true;
            }
        }
        return false;
    }
}
