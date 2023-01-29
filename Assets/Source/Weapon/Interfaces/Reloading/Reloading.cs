using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class Reloading : MonoBehaviour, IReloading
{
    private Weapon _weapon;
    private Coroutine _reloading;
    public event Action Reloaded;

    private void OnEnable()
    {
        _weapon = GetComponent<Weapon>();
        _weapon.BulletsEnded += Reload;
    }

    private void OnDisable()
    {
        _weapon.BulletsEnded -= Reload;
    }

    public void Reload()
    {
        if (_reloading != null)
        {
            StopCoroutine(_reloading);
        }

        _reloading = StartCoroutine(ReloadedWait());
    }

    private IEnumerator ReloadedWait()
    {
        yield return new WaitForSeconds(_weapon.ReloadTime);
        Reloaded?.Invoke();
    }
}
