using Character.Enums;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CrowMovementController : MovementBasis
{
    public Collider2D TriggerArea;
    public Collider2D AttackArea;
    public float attackInterval;

    private Transform playerPos;
    private Collider2D[] colliders;
    private CrowShallCast shallCast;    

    private DateTime _lastAttack = DateTime.Now;
    private bool _fight = false;
    private bool _castTrigger;


    internal override void Awake()
    {
        base.Awake();
        playerPos = FindObjectOfType<Player>().transform;
        colliders = GetComponentsInChildren<Collider2D>().Where(a => !a.Equals(TriggerArea)).ToArray();
        shallCast = GetComponentInChildren<CrowShallCast>();
    }

    private void FixedUpdate()
    {
        if (isAnimation) return;
        if (_castTrigger)
        {
            shallCast.ShallCast((Directions)isoRenderer.animator.GetInteger("Direction"));
            _castTrigger = false;
        }
        if (_fight && attackInterval < (DateTime.Now - _lastAttack).Seconds)
        {
            Attack();
            _lastAttack = DateTime.Now;
        }
    }

    public override void Attack()
    {
        Vector2 inputVector = new Vector2(playerPos.position.x - _rbody.position.x, 0);
        isoRenderer.SetDirection(inputVector);
        isoRenderer.Attack();
        _castTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (collision.IsTouching(TriggerArea))
        {
            Rise();
        }

        if (collision.IsTouching(AttackArea))
        {
            _fight = true;
            _lastAttack += (DateTime.Now - _lastAttack).Add(TimeSpan.FromSeconds(1f));
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        if (!collision.IsTouching(TriggerArea))
        { 
            Descent();
        }

        if (!collision.IsTouching(AttackArea))
        {
            _fight = false;
        }
    }

    /// <summary>
    /// Взлет
    /// </summary>
    public void Rise()
    {
        if (isoRenderer.animator.GetBool("Run")) return;
        isoRenderer.animator.SetBool("Run", true);
        foreach (var i in colliders)
            i.enabled = false;
    }

    /// <summary>
    /// Посадка
    /// </summary>
    public void Descent()
    {
        if (!isoRenderer.animator.GetBool("Run")) return;
        isoRenderer.animator.SetBool("Run", false);
        foreach (var i in colliders)
            i.enabled = true;
    }

    public override void Move(float horizontalInput, float verticalInput) { }
}
