public class NothingAfterHitAction : IAfterHitAction
{
    public virtual void Action(Bullet bullet,Enemy enemy)
    {
        bullet.Destroy();
    }
}
