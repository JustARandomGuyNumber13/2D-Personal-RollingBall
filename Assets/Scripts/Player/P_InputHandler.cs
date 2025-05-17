using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class P_InputHandler : MonoBehaviour
{
    [SerializeField] P_Controller controller;
    [SerializeField] Animator anim;
    [SerializeField] UnityEvent OnJumpEvent;
    [SerializeField] UnityEvent OnResetEvent;

    float inputValue;
    Rigidbody2D rb;
    Transform t;

    /* Monobehavior handler */
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        t = transform;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        Action_Move();
    }

    /* Action handler */
    void Action_Move()
    {
        if (inputValue != 0)
        {
            rb.AddForce(Vector2.right * controller.MoveForce * inputValue);
        }

        rb.linearVelocityX = Mathf.Clamp(rb.linearVelocityX, -controller.MaxMoveSpeed, controller.MaxMoveSpeed);
    }
    void Action_Jump()
    {
        if (!controller.OnGround) return;

        rb.linearVelocityY = 0;
        rb.AddForce(Vector2.up * controller.JumpForce, ForceMode2D.Impulse);
        controller.OnGround = false;
        OnJumpEvent?.Invoke();
    }

    /* Input handler */
    void OnMove(InputValue value)
    { 
        inputValue = Mathf.Ceil(value.Get<float>());
        anim.SetInteger("Move", (int)inputValue);
    }
    void OnJump(InputValue value)
    {
        if (Mathf.Ceil(value.Get<float>()) == 1)
            Action_Jump(); 
    }
    void OnReset(InputValue value)
    {
        if (Mathf.Ceil(value.Get<float>()) == 1)
            OnResetEvent?.Invoke();
    }
}
