public class BrokenTransition : PlayerTransition
{
    public override void Enable()
    {
        Player.PlayerWasAttackedEvent += OnPlayerAttacked;
    }

    private void OnPlayerAttacked()
    {
        NeedTransit = true;
    }

    private void OnDisable()
    {
        Player.DamagedEvent -= OnPlayerAttacked;
    }

}
