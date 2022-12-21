using UnityEngine;

public enum Enemies{
    Crow = 0,
    Ghoul = 1,
    Shadow = 2,
    Agaric = 3
}

public abstract class Enemy : Entity
{
    public Enemies enemy { get; private set; }

    internal virtual void Awake()
    {
        movementController = GetComponent<MovementBasis>();
    }

    public Enemy(Enemies enemy)
    {
        switch (enemy)
        {
            case Enemies.Crow:
                _name = "Crow";
                this.enemy = Enemies.Crow;
                _hp = 50;
                _force = 20;
                break;
            case Enemies.Ghoul:
                _name = "Ghoul";
                this.enemy = Enemies.Ghoul;
                _hp = 150;
                _force = int.MaxValue;
                break;
            case Enemies.Shadow:
                _name = "Shadow";
                this.enemy = Enemies.Shadow;
                _hp = 150;
                _force = 34;
                break;
            case Enemies.Agaric:
                _name = "Agaric";
                this.enemy = Enemies.Agaric;
                _hp = 1;
                _force = 5;
                break;
            default:
                break;
        }
    }

    public Enemy(Enemies enemy, int health, int force)
    {
        this.enemy = enemy;
        this._hp = health;
        this._force = force;
    }

    public override void Hit(Entity target)
    {
        target.Damage(Force);
    }
}
