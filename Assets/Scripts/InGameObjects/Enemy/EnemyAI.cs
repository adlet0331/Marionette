using System;
using Managers;
using UnityEngine;
using UnityEngine.AI;

namespace InGameObjects.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private GameObject spriteGameObject;
        [SerializeField] private float lastUpdateTime;

        [SerializeField] private Vector2 direction;
        [SerializeField] private bool isMoving;

        private NavMeshAgent agent;
        private BoxCollider boxCollider;
        private Animator animator;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            boxCollider = GetComponent<BoxCollider>();
            animator = spriteGameObject.GetComponent<Animator>();
            
            agent.updatePosition = true;
            lastUpdateTime = Time.realtimeSinceStartup;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // Do Something
            }
        }

        private void Update()
        {
            if (Time.realtimeSinceStartup - lastUpdateTime < 0.05)
                return;

            lastUpdateTime = Time.realtimeSinceStartup;
            agent.destination = GamePlayManager.Instance.MoveablePlayerObjectPosition;
            
            var movingVector = agent.velocity;

            if (Math.Abs(movingVector.x) > Math.Abs(movingVector.y))
            {
                movingVector.y = 0;
            }
            else
            {
                movingVector.x = 0;
            }

            movingVector = movingVector.normalized;

            if (movingVector.magnitude >= 1.0)
            {
                isMoving = true;
                direction = movingVector;
            }
            else
            {
                isMoving = false;
            }

            animator.SetBool("IsMoving", isMoving);
            animator.SetFloat("DirX", direction.x);
            animator.SetFloat("DirY", direction.y);
        }
    }
}