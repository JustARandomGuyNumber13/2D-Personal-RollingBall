using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class P_Controller : MonoBehaviour
{
    [SerializeField] SO_Player_Stat pStat;
    [SerializeField] Vector3 groundCheckBoxSize;
    [SerializeField] Vector3 groundCheckBoxOffset;

    Rigidbody2D rb;
    private Game_Manager gm;
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
        gm = Game_Manager.instance;
    }
    void Start()
    {
        SetUp();
    }

    int l;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        l = collision.collider.gameObject.layer;

        if (l == Global.DoorLayerInt)
        {
            if (gm.KeyCount > 0)
            {
                gm.KeyCount--;
                collision.gameObject.SetActive(false);
            }
        }
        else if (l == Global.EnemyLayerInt)
        {
            rb.linearVelocityY = 0;
            rb.AddForce(Vector2.up * JumpForce* 0.75f, ForceMode2D.Impulse);
            collision.gameObject.SetActive(false);
        }
        else if (l == Global.DeathLayerInt)
        {
            Game_Manager.instance.KeyCount = 0;
            OnDeathEvent?.Invoke();
        }
        else if (l == Global.GroundLayerInt)
        {
            RaycastHit2D hit = Physics2D.BoxCast(transform.position + groundCheckBoxOffset, groundCheckBoxSize, 0, Vector2.zero, 0, Global.GroundLayer);
            if (hit.collider != null)
                OnGround = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        l = collision.gameObject.layer;

        if (l == Global.LevelTriggerLayerInt)
        {
            gm.P_NextLevel();
        }
        else if (l == Global.KeyLayerInt)
        {
            gm.KeyCount++;
            collision.gameObject.SetActive(false);
        }

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
