using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class P_Controller : MonoBehaviour
{
    [SerializeField] SO_Player_Stat pStat;
    [SerializeField] Vector3 groundCheckBoxSize;
    [SerializeField] Vector3 groundCheckBoxOffset;

    Rigidbody2D rb;
    private float additionalMaxMoveSpeed;
    private float additionalJumpForce;

    public float MoveForce { get; private set; }
    public float MaxMoveSpeed { get; private set; }
    public float JumpForce { get; private set; }
    public bool OnGround { get; set; }

    public UnityEvent OnLandEvent;
    public UnityEvent OnDeathEvent;

    /* Monobehavior handlers */
    void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        SetUp();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Death"))
        {
            OnDeathEvent?.Invoke();
            return;
        }

        RaycastHit2D hit = Physics2D.BoxCast(transform.position + groundCheckBoxOffset, groundCheckBoxSize, 0, Vector2.zero, 0, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
            OnGround = true;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + groundCheckBoxOffset, groundCheckBoxSize);
    }

    /* Public function handlers */
    public void Print(string text) { Debug.Log(text, gameObject); }
    public void P_UseBuff(BuffType type, float value, float duration)
    { StartCoroutine(BuffCoroutine(type, value, duration)); }


    /* Other handlers */
    void SetUp()
    {
        MoveForce = pStat.MoveForce;
        JumpForce = pStat.JumpForce;
        MaxMoveSpeed = pStat.MaxMoveSpeed;
    }
    IEnumerator BuffCoroutine(BuffType type, float value, float duration)
    {
        switch (type)
        {
            case BuffType.MaxSpeed:
                MaxMoveSpeed = pStat.MaxMoveSpeed + value;
                break;
            case BuffType.JumpForce:
                JumpForce = pStat.JumpForce + value;
                break;
        }

        yield return new WaitForSeconds(duration);

        switch (type)
        {
            case BuffType.MaxSpeed:
                MaxMoveSpeed = pStat.MaxMoveSpeed;
                break;
            case BuffType.JumpForce:
                JumpForce = pStat.JumpForce;
                break;
        }
    }
    public enum BuffType { JumpForce, MaxSpeed}
}
