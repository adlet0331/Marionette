﻿using UnityEngine;


/* 플레이어 키 입력으로 움직이는 물체 관리
 */
namespace InGameObjects.Object
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRigidBody;

        [SerializeField] private bool isActive = true;
        [SerializeField] private float slowWalkSpeed = 300.0f;
        [SerializeField] private float moveSpeed = 500.0f;
        [SerializeField] private float runSpeed = 1000.0f;

        private Animator animator;

        public bool isMoving, isRunning, isSlowWalking;
        [SerializeField] private int dirX, dirY;

        public void MovingObjectStart() 
        {
            animator = GetComponent<Animator>();
            playerRigidBody = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate() 
        {
            if (isActive && isMoving) 
            {
                if (isSlowWalking) 
                {
                    playerRigidBody.velocity = (Vector2.right * dirX + Vector2.up * dirY) * slowWalkSpeed;
                }
                else if (isRunning)
                {
                    playerRigidBody.velocity = (Vector2.right * dirX + Vector2.up * dirY) * runSpeed;
                }
                else 
                {
                    playerRigidBody.velocity = (Vector2.right * dirX + Vector2.up * dirY) * moveSpeed;
                }
            }
            else
            {
                playerRigidBody.velocity = new Vector2(0, 0);
            }
            if (animator)
            {
                animator.SetBool("IsMoving", isMoving);
                animator.SetBool("IsRunning", isRunning);
                animator.SetBool("IsSlowWalking", isSlowWalking);
                animator.SetFloat("DirX", dirX);
                animator.SetFloat("DirY", dirY);
            }
        }
        public void Move(int hor, int ver, bool isCtrl, bool isShift)
        {
            if (hor == 0 && ver == 0)
            {
                isMoving = false;
                isRunning = false;
                isSlowWalking = false;
                return;
            }
            isMoving = true;
            isRunning = isCtrl;
            isSlowWalking = isShift;
            dirX = hor;
            dirY = ver;
            return;
        }
    }
}
