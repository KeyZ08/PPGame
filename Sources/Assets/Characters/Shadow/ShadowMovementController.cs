using Character.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ShadowMovementController : MovementController
{
    public Collider2D TriggerArea;
    private NavMeshAgent _agent;
    private Transform _player;
    private Transform _tr;
    private NavMeshPath path;

    public EnemyState state { get; private set; }

    internal override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _tr = transform;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        state = EnemyState.Calm;
    }

    private void FixedUpdate()
    {
        if (isAnimation) return;

        isoRenderer.SetDirection(_agent.velocity);
        if (_agent.velocity.magnitude > 0.01f)
            isoRenderer.animator.SetBool("Run", true);
        else isoRenderer.animator.SetBool("Run", false);

        if(state == EnemyState.Calm)
        {

        }
        else if(state == EnemyState.Harassment)
        {
            Harassment();
        }
    }

    //преследование чтобы ударить
    public void Harassment()
    {
        var pos = FindPathToPlayer();
        _agent.SetDestination(pos);

        state = EnemyState.Harassment;
        if(!isAnimation && Vector2.Distance(_tr.position, _player.position) < 0.3f)
        {
            isoRenderer.SetDirection(_player.position - transform.position);
            RenderAttack();
        }
    }

    public Vector3 FindPathToPlayer()
    {
        bool PathIsComplete(Vector3 pos)
        {
            _agent.CalculatePath(pos, path);
            return path.status == NavMeshPathStatus.PathComplete;
        }

        var playerPos = _player.position;
        var pos1 = playerPos + new Vector3(0.3f, 0, 0);
        var pos2 = playerPos + new Vector3(-0.3f, 0, 0);
        var pathsComplete = new List<Vector3>(4);
        if (PathIsComplete(pos1))
            pathsComplete.Add(pos1);
        if (PathIsComplete(pos2))
            pathsComplete.Add(pos2);

        if (pathsComplete.Count == 0)
            return pos1;
        if (pathsComplete.Count == 1)
            return pathsComplete[0];
        else
            return pathsComplete.Where(x => Vector2.Distance(x, _tr.position) == pathsComplete.Min(i => Vector2.Distance(i, _tr.position))).First();
    }

    

    private void RenderAttack()
    {
        var direction = isoRenderer.GetDirection();
        if (attackAreas[direction] == null) return;

        isoRenderer.Attack();
    }

    public override void Attack()
    {
        var direction = isoRenderer.GetDirection();
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(attackAreas[direction].position, attackAreas[direction].localScale.x / 2, targetAttackLayers);
        foreach (var target in hitTargets)
        {
            var targetEntity = target.GetComponentInParent<Entity>();
            GetComponentInParent<Entity>().Hit(targetEntity);
        }
    }

    public override void Death()
    {
        base.Death();
        _agent.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(TriggerArea.transform.position, TriggerArea.transform.lossyScale.x / 2, LayerMask.GetMask("Player"));
        if (hitTargets.Length != 0)
        {
            state= EnemyState.Harassment;
        }
    }
}