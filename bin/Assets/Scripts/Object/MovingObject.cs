using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private bool isActive = true;
    [SerializeField] private float slowWalkSpeed = 300.0f;
    [SerializeField] private float moveSpeed = 500.0f;
    [SerializeField] private float runSpeed = 1000.0f;

    private Animator animator;

    public bool isMoving, isRunning, isSlowWalking;
    private int dirX, dirY;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate() {
        if (isActive && isMoving) {
            if (isSlowWalking) {
                this.transform.Translate((Vector2.right * dirX + Vector2.up * dirY) * Time.deltaTime * slowWalkSpeed);
            }
            else if (isRunning)
            {
                this.transform.Translate((Vector2.right * dirX + Vector2.up * dirY) * Time.deltaTime * runSpeed);
            }
            else {
                this.transform.Translate((Vector2.right * dirX + Vector2.up * dirY) * Time.deltaTime * moveSpeed);
            }
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
