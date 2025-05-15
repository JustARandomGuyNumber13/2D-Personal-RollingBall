using UnityEngine;

public static class Global
{
    public static readonly string PlayerTag = "Player";

    public static readonly int PlayerLayerInt = LayerMask.NameToLayer("Player");
    public static readonly int GroundLayerInt = LayerMask.NameToLayer("Ground");
    public static readonly int LevelTriggerLayerInt = LayerMask.NameToLayer("Level Trigger");
    public static readonly int DoorLayerInt = LayerMask.NameToLayer("Door");
    public static readonly int KeyLayerInt = LayerMask.NameToLayer("Key");
    public static readonly int DeathLayerInt = LayerMask.NameToLayer("Death");
    public static readonly int DestroyLayerInt = LayerMask.NameToLayer("Destroy");

    public static readonly LayerMask PlayerLayer = LayerMask.GetMask("Player");
    public static readonly LayerMask GroundLayer = LayerMask.GetMask("Ground");
}
