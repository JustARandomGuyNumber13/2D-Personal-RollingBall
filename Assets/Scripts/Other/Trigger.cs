using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] UnityEvent OnTriggerEvent;
    [SerializeField] float delay;
    bool trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!trigger && collision.gameObject.layer == Global.PlayerLayerInt)
        {
            Invoke("CallEvent", delay);
            trigger = true;
        }
    }

    void CallEvent()
    {
        OnTriggerEvent?.Invoke();
    }
}
