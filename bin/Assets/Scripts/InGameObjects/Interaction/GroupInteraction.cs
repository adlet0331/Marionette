using Managers;
using UnityEngine;

namespace InGameObjects.Interaction
{
    public class GroupInteraction : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var interactingObjects = GetComponentsInChildren<InteractingObject>(this.gameObject);
            SLManager.Instance.InitsceneInteractingObjects(interactingObjects);
        }
    }
}
