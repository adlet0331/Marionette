using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float runSpeed = 10.0f;
    public Rigidbody body;

    public bool isMoving, isRunning;
    private int dirH, dirV;

    private void Start() {
        body = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() {
        if (isMoving) {
            if (isRunning) {
                transform.Translate((Vector2.right * dirH + Vector2.up * dirV) * Time.deltaTime * runSpeed);
            }
            else {
                transform.Translate((Vector2.right * dirH + Vector2.up * dirV) * Time.deltaTime * moveSpeed);
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
}
