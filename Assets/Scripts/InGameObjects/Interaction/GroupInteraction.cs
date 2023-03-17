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
            var dictionary = GamePlayManager.Instance.CurrentInteractingObjectStatusDict;
            foreach (var interactingObject in interactingObjects)
            {
                var status = dictionary[interactingObject.Idx].CurrentStatus;
                
                interactingObject.gameObject.SetActive(status);
            }
        }
    }
}
