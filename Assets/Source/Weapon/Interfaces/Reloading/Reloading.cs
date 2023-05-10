using System;
using System.Threading.Tasks;

public class Reloading : IReloading
{
    private readonly Weapon _weapon;
    public event Action Reloaded;
    
    public Reloading(Weapon weapon)
    {
        _weapon = weapon;
        _weapon.BulletsEnded += () => Reload();
    }

    public async Task Reload()
    {
        int millisecondsInSecond = 1000;
        await Task.Delay((int)_weapon.ReloadTime*millisecondsInSecond);
        EndReload();
    }

    private void EndReload()
    {
        Reloaded?.Invoke();
    }
}
