using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour
{
    public LayerMask targetAttackLayers;
    public int Force = 75;
    public Transform Area;

    public virtual void Attack()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(Area.position, Area.localScale.x / 2, targetAttackLayers);
        foreach (var target in hitTargets)
        {
            var targetEntity = target.GetComponentInParent<Entity>();
            Hit(targetEntity);
        }
    }

    public virtual void Hit(Entity target)
    {
        target.Damage(Force);
    }
}
