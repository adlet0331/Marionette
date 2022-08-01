using System;
using Managers;
using UnityEngine;

namespace InGameObjects.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        private void Start()
        {
            UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.destination = PlayerManager.Instance.moveablePlayerObject.transform.position;
        }
    }
}