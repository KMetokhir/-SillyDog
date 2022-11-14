using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    public event UnityAction<Vector2> DirectionChangedEvent;
    public event UnityAction PointerUpEvent;

    private Vector3 _tapPosition;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tapPosition = Input.mousePosition;

        }

        if (Input.GetMouseButton(0))
        {

            if (Input.mousePosition - _tapPosition != Vector3.zero)
            {
                DirectionChangedEvent?.Invoke(Input.mousePosition - _tapPosition);
            }

        }

        if (Input.GetMouseButtonUp(0))
            PointerUpEvent?.Invoke();
    }

}
