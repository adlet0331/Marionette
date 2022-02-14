using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/* 움직이는 오브젝트 관리
 * 
 */
public class MovingObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private Light2D aroundLight;
    [SerializeField] private Light2D handLight;

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
    }
    private void UpdateHandLightRotate()
    {
        int rotate = 0;
        if (dirX == 0 && dirY == 1)
            rotate = 0;
        if (dirX == -1 && dirY == 1)
            rotate = 45;
        if (dirX == -1 && dirY == 0)
            rotate = 90;
        if (dirX == -1 && dirY == -1)
            rotate = 135;
        if (dirX == 0 && dirY == -1)
            rotate = 180;
        if (dirX == 1 && dirY == -1)
            rotate = 225;
        if (dirX == 1 && dirY == 0)
            rotate = 270;
        if (dirX == 1 && dirY == 1)
            rotate = 315;
        handLight.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotate));
        return;
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
        if (animator != null)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetBool("IsRunning", isRunning);
            animator.SetBool("IsSlowWalking", isSlowWalking);
            animator.SetFloat("DirX", dirX);
            animator.SetFloat("DirY", dirY);
        }
        UpdateHandLightRotate();
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
    public void SetActive(bool isActive) 
    {
        this.isActive = isActive;
    }
}
