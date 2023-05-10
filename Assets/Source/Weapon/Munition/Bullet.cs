using UnityEngine;

public class Bullet : Munition,IAfterHitActionSelectionner
{
    private float _speed=1;
    private int _damage=1;
    [SerializeField]private Vector3 _target;
    private AfterHitActionVariation _afterHitActionVariant;
    private IAfterHitAction _afterHitAction;
    
    private void Update()
    {
        if (_target != null)
        {
            if (Vector3.Distance(transform.position, _target) > 0.01)
            {
                MovingToTarget();
            }
            else
            {
                Destroy();
            }
        }
    }

    public void Initialization(Weapon weapon)
    {
        _speed = weapon.BulletSpeed;
        _damage = weapon.BulletDamage;
        IAfterHitActionSelectionner bulletSpellSelectionner = this as IAfterHitActionSelectionner;
        bulletSpellSelectionner.SetAfterHitAction(_afterHitActionVariant);
        SetTargetPoint(weapon.TargetPoint);
    }

    public void SetTargetPoint(Vector3 newPoint)
    {
        _target = newPoint+new Vector3(0,0.5f,0);
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void MovingToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Debug.Log("hit");
            enemy.Health.ApplyDamage(_damage);
        }
        else
        {
            enemy = null;
        }

        _afterHitAction.Action(this,enemy);
    }

    void IAfterHitActionSelectionner.OnReboundSelected()
    {
        _afterHitAction = new ReboundingAfterHitAction(this);
    }

    void IAfterHitActionSelectionner.OnNothingSelected()
    {
        _afterHitAction = new NothingAfterHitAction();
    }
}
