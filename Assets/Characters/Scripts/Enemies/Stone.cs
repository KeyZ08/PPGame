using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : Missile
{
    public GameObject Pick_Up()
    {
        return gameObject;
    }

    public override void Attack()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(Area.position, Area.localScale.x / 2, targetAttackLayers);
        foreach (var target in hitTargets)
        {
            var targetEntity = target.GetComponentInParent<Entity>();
            if (targetEntity is Shadow) continue;
            Hit(targetEntity);
        }
    }
}
