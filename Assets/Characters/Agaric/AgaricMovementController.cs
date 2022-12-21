using Character.Enums;
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AgaricMovementController : MovementBasis
{
    public Collider2D TriggerArea;
    public Transform AttackArea;
    private NavMeshAgent agent;
   

    public EnemyState state { get; private set; }
    

    internal override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = Random.Range(1f, 1.5f);
        state = EnemyState.Calm;
    }

    private void FixedUpdate()
    {
        isoRenderer.SetDirection(agent.velocity);
        if (agent.velocity.magnitude > 0.1) 
            isoRenderer.animator.SetBool("Run", true);
        else isoRenderer.animator.SetBool("Run", false);
    }

    public override void Attack() 
    {
        isoRenderer.Attack();

        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(AttackArea.position, AttackArea.localScale.x / 2, LayerMask.GetMask("Player"));
        foreach (var target in hitTargets)
        {
            var targetEntity = target.GetComponentInParent<Entity>();
            GetComponentInParent<Entity>().Hit(targetEntity);
        }
    }

    //преследование чтобы ударить
    public void Harassment(Vector3 playerPos)
    {
        agent.SetDestination(playerPos);
        agent.stoppingDistance = 0;
        agent.speed = 1.1f;
        state = EnemyState.Harassment;
    }

    //просто преследование
    public void Pursuit(Vector3 playerPos)
    {
        agent.SetDestination(playerPos);
        agent.speed = 1f;
        state = EnemyState.Pursuit;
    }

    public override void Death()
    {
        base.Death();
        agent.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state != EnemyState.Calm) return;
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(TriggerArea.transform.position, TriggerArea.transform.lossyScale.x / 2, LayerMask.GetMask("Player"));
        if (hitTargets.Length != 0)
        {
            Pursuit(hitTargets[0].transform.position);
            FindObjectOfType<AgaricCollectiveBehaviour>().Add(this);
        }
    }

    private void OnDestroy()
    {
        Attack();
    }
}
