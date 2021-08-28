using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private bool isActive = true;
    [SerializeField] private float slowWalkSpeed = 300.0f;
    [SerializeField] private float moveSpeed = 500.0f;
    [SerializeField] private float runSpeed = 1000.0f;

    public bool isMoving, isRunning, isSlowWalking;
    private int dirH, dirV;

    private void Start() {
        
    }
    private void FixedUpdate() {
        if (isActive && isMoving) {
            if (isSlowWalking) {
                this.transform.Translate((Vector2.right * dirH + Vector2.up * dirV) * Time.deltaTime * slowWalkSpeed);
            }
            else if (isRunning) {
                this.transform.Translate((Vector2.right * dirH + Vector2.up * dirV) * Time.deltaTime * runSpeed);
            }
            else {
                this.transform.Translate((Vector2.right * dirH + Vector2.up * dirV) * Time.deltaTime * moveSpeed);
            }
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
        dirH = hor;
        dirV = ver;
        return;
    }
    public void SetCanMove(bool isActive) {
        this.isActive = isActive;
    }
}
