using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private bool canMove;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private Rigidbody body;

    public bool isMoving, isRunning;
    private int dirH, dirV;

    private void Start() {
        body = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        if (canMove && isMoving) {
            if (isRunning) {
                this.transform.Translate((Vector2.right * dirH + Vector2.up * dirV) * Time.deltaTime * runSpeed);
            }
            else {
                this.transform.Translate((Vector2.right * dirH + Vector2.up * dirV) * Time.deltaTime * moveSpeed);
            }
        }
    }
    public void Move(int hor, int ver, bool isShift)
    {
        if (hor == 0 && ver == 0)
        {
            isMoving = false;
            isRunning = false;
            return;
        }
        isMoving = true;
        isRunning = isShift;
        dirH = hor;
        dirV = ver;
        return;
    }
    public void StopMove() {
        isMoving = false;
        isRunning = false;
        return;
    }
    public void SetCanMove(bool canMove) {
        this.canMove = canMove;
    }
}
