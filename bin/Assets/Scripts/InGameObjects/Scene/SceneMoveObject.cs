using Managers;
using UnityEngine;
using static Managers.SceneSwitchManager;

namespace InGameObjects.Scene
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SceneMoveObject : MonoBehaviour
    {
        [SerializeField] private SceneName sceneName;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                SceneSwitchManager.Instance.SwitchScene(sceneName);
            }
        }
    }
}