using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    internal string _name;
    internal int _hp;
    internal int _force;
    internal bool _dead = false;
    internal MovementBasis movementController;

    public virtual void Damage(int force)
    {
        movementController.Hurt();
        HP -= force;
        print($"{_name}: {HP}");
    }


    public int HP
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp <= 0) isDead = true;
        }
    }

    public bool isDead
    {
        get { return _dead; }
        private set
        {
            _dead = value;
            if (_dead == true) Death();
        }
    }

    public virtual void Death()
    {
        movementController.Death();
    }

    public int Force
    {
        get { return _force; }
        set { _force = value; }
    }

    public abstract void Hit(Entity target);

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
