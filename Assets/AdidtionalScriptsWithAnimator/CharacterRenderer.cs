using System;
using UnityEngine;

public abstract class CharacterRenderer : MonoBehaviour
{
    [NonSerialized]
    public Animator animator;
    [NonSerialized]
    public int lastDirection;
    [NonSerialized]
    public bool isAnimation;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount)
    {
        Vector2 normDir = dir.normalized;
        float step = 360f / sliceCount;
        float halfstep = step / 2;
        float angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfstep;
        if (angle < 0) angle += 360;
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }

    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public virtual void Death()
    {
        animator.SetTrigger("Death");
    }

    public virtual void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public virtual int GetDirection()
    {
        return animator.GetInteger("Direction");
    }

    public virtual void Cast()
    {
        animator.SetTrigger("Cast");
    }

    public abstract void SetDirection(Vector2 direction);

    public void AttackStart()
    {
        isAnimation = true;
    }

    public void AttackEnd()
    {
        isAnimation = false;
    }
}
