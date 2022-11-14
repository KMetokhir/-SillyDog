using System;
using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    public event Action<PeeAria> PeeAriaEnteredEvent;
    public event Action<PeeAria> PeeAriaExitEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PeeAria peeAria))
        {
            PeeAriaEnteredEvent?.Invoke(peeAria);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PeeAria peeAria))
        {
            PeeAriaExitEvent?.Invoke(peeAria);
        }
    }



}
