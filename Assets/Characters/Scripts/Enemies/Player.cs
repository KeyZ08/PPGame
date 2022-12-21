using System;
using UnityEngine;

public class Player : Entity 
{
    public float invulnerability;
    private DateTime lastDamage;

    private void Awake()
    {
        movementController = GetComponent<MovementBasis>();
        _hp = 100;
        _force = 50;
    }

    public override void Hit(Entity target)
    {
        target.Damage(Force);
    }

    public override void Damage(int force)
    {
        if ((DateTime.Now - lastDamage).TotalSeconds > invulnerability)
        {
            lastDamage = DateTime.Now;
            movementController.Hurt();
            HP -= force;
        }
        print("Player hp: " + HP);
    }
}
