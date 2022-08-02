using System;
using Managers;
using UnityEngine;

namespace InGameObjects.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
        [SerializeField] private float lastUpdateTime;
        private void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            lastUpdateTime = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup - lastUpdateTime < 0.05)
                return;
            lastUpdateTime = Time.realtimeSinceStartup;
            agent.destination = PlayerManager.Instance.moveablePlayerObject.transform.position;
        }
    }
}