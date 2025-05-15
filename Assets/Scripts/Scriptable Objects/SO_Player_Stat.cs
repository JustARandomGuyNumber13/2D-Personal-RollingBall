using UnityEngine;

[CreateAssetMenu(fileName = "P_Stat", menuName = "Scriptable Objects/P_Stat")]
public class SO_Player_Stat : ScriptableObject
{
    [SerializeField] float moveForce;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float jumpForce;

    public float MoveForce => moveForce;
    public float MaxMoveSpeed => maxMoveSpeed;
    public float JumpForce => jumpForce;
}
