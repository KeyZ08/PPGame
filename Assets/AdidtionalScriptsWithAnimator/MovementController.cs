using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public abstract class MovementController : MovementBasis
{
    public Transform attackE;
    public Transform attackW;
    public Transform attackN;
    public Transform attackS;
    internal Transform[] attackAreas;
    public LayerMask targetAttackLayers;

    internal override void Awake()
    {
        base.Awake();
        attackAreas = new Transform[8] { attackN, null, attackW, null, attackS, null, attackE, null };
    }

    public override void Attack()
    {
        var direction = isoRenderer.GetDirection();
        if (attackAreas[direction] == null) return;

        isoRenderer.Attack();
        
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackAreas[direction].position, attackAreas[direction].localScale.x / 2, targetAttackLayers);
        foreach (var target in hitTargets)
        {
            var targetEntity = target.GetComponentInParent<Entity>();
            GetComponentInParent<Entity>().Hit(targetEntity);
        }
    }
}
