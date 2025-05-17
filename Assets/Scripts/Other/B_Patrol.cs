using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class B_Patrol : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 xPoint;
    [SerializeField] float coolDown;
    [SerializeField] bool moveLeft;
    [SerializeField] UnityEvent OnSwitchDirEvent;
    [SerializeField] UnityEvent OnStartMoveEvent;

    private bool canMove; 
    Transform t;
    Rigidbody2D rb;
    float left, right;
    Vector3 scale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        t = transform;
    }

    private void Start()
    {
        left = transform.position.x + xPoint.x;
        right = transform.position.x + xPoint.y;
        canMove = true;
        OnStartMoveEvent?.Invoke();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            SwapDir();
            rb.linearVelocityX = moveSpeed * (moveLeft ? -1 : 1);
        }
    }

    void SwapDir()
    {
        if ((moveLeft && t.position.x <= left) || (!moveLeft && t.position.x >= right))
        {
            moveLeft = !moveLeft;
            canMove = false;
            Invoke("SetCanMoveTrue", coolDown);
            OnSwitchDirEvent?.Invoke();
        }
    }

    public void Print(string msg) { Debug.Log(msg); }

    private void SetCanMoveTrue()
    {
        scale = Vector3.one;
        scale.x = moveLeft ? -1 : 1;
        t.localScale = scale;
        canMove = true;
        OnStartMoveEvent?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (left == 0) left = transform.position.x + xPoint.x;
        if(right == 0)right = transform.position.x + xPoint.y;
        Gizmos.DrawLine(Vector2.right * left + Vector2.up * transform.position.y, Vector2.right * right + Vector2.up * transform.position.y);
    }
}
