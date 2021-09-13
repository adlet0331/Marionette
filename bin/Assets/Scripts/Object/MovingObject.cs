using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;

    [SerializeField] private bool isActive = true;
    [SerializeField] private float slowWalkSpeed = 300.0f;
    [SerializeField] private float moveSpeed = 500.0f;
    [SerializeField] private float runSpeed = 1000.0f;

    private Animator animator;

    public bool isMoving, isRunning, isSlowWalking;
    private int dirX, dirY;
    private Vector2 zeroVector;

    private void Start() {
        animator = GetComponent<Animator>();
        zeroVector = new Vector2(0, 0);
    }
    private void FixedUpdate() {
        if (isActive && isMoving) {
            if (isSlowWalking) {
                playerRigidBody.velocity = (Vector2.right * dirX + Vector2.up * dirY) * slowWalkSpeed;
            }
            else if (isRunning)
            {
                playerRigidBody.velocity = (Vector2.right * dirX + Vector2.up * dirY) * runSpeed;
            }
            else {
                playerRigidBody.velocity = (Vector2.right * dirX + Vector2.up * dirY) * moveSpeed;
            }
        }
        else
        {
            playerRigidBody.velocity = new Vector2(0, 0);
        }
        if (animator != null)
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
    public void SetActive(bool isActive) {
        this.isActive = isActive;
    }
}
