using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField] Vector3 destination;
    [SerializeField] float delay;
    Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Global.PlayerLayerInt)
        {
            target = collision.transform;
            Invoke("DoSomething", delay);
        }
    }

    void DoSomething()
    { 
        target.position = destination;
        Game_Manager.instance.P_SetLevel(0);
    }
}
