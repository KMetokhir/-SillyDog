public class AngryTransition : EnemyTransition
{


    protected override void Enable()
    {
        CollisionTracker.PeeAriaEnteredEvent += OnPeeAriaEntered;
    }

    private void OnDisable()
    {
        CollisionTracker.PeeAriaEnteredEvent -= OnPeeAriaEntered;
    }

    private void OnPeeAriaEntered(PeeAria peeAria)
    {
        if (PeeAria == peeAria)
        {
            NeedTransit = true;
        }
    }

   




}
