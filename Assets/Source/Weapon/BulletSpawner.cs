using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class BulletSpawner : Spawner
{
    [SerializeField] private Weapon Weapon;

    private void OnDisable()
    {
        Destroy(Container);
    }

    protected override void GetExternalData()
    {
        Weapon = GetComponent<Weapon>();
        Templates[0] = Weapon.Bullet.gameObject;
        Capacity = Weapon.MaxBulletCount;
        CreateContainer();
    }

    private void CreateContainer()
    {
        if (Weapon != null)
        {
            Container = new GameObject();
            Container.transform.SetParent(Weapon.GetComponentInParent<UnitSpawnDot>().transform);
        }
    }
}
