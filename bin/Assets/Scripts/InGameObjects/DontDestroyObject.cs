using System.Collections;
using UnityEngine;

namespace Assets.Scripts.InGameObjects
{
    public class DontDestroyObject : MonoBehaviour
    {
        public enum ObjectType
        {
            CanvasObject = 0,
            PlayerObject = 1,
        }
        [SerializeField] private ObjectType type;
        private void Start()
        {
            SceneObjManager.Instance.AddObject(type, this.gameObject);
            //Debug.Log("DontDestroyable Object : " + this.ToString());
        }
    }
}