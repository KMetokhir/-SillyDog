using UnityEngine;

public class MoveTransition : PlayerTransition
{
    public override void Enable()
    {
        Input.DirectionChangedEvent += OnDirectionChanged;
    }

    private void OnDirectionChanged(Vector2 direction)
    {
        NeedTransit = true;
    }

    private void OnDisable()
    {
        Input.DirectionChangedEvent -= OnDirectionChanged;
    }

}
