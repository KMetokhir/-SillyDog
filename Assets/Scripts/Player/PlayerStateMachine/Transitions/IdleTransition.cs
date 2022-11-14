public class IdleTransition : PlayerTransition
{
    public override void Enable()
    {
        Input.PointerUpEvent += OnPointerUp;
    }

    private void OnPointerUp()
    {
        NeedTransit = true;
    }

    private void OnDisable()
    {
        Input.PointerUpEvent -= OnPointerUp;
    }


}
