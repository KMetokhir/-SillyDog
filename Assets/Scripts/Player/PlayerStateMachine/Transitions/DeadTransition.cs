public class DeadTransition : PlayerTransition
{
    public override void Enable()
    {
        Player.DeadEvent += OnDead;
    }

    private void OnDead()
    {
        NeedTransit = true;
    }

    private void OnDisable()
    {
        Player.DeadEvent -= OnDead;
    }


}
