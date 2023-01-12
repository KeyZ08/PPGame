using System;
using System.Linq;
using UnityEngine;

public class PlayerMovementController : MovementController
{
    public Transform SelectionArea;
    private GameObject Stone;
    private Trajectory trajectory;


    internal override void Awake()
    {
        base.Awake();
        trajectory = GetComponentInChildren<Trajectory>();
        trajectory.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isAnimation) return;
        if (Input.GetKeyDown(KeyCode.E) && Stone == null)
            Attack();

        if (Input.GetKeyDown(KeyCode.Z))
            if (Stone != null && !Stone.TryGetComponent<Waypoints>(out var waypoints))
                ThrowStone();
            else TakeStone();

        if (Stone != null)
        {
            Stone.transform.position = trajectory.ItemPosition.position;
        }
    }


    private void FixedUpdate()
    {
        if (isAnimation) return;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Move(horizontalInput, verticalInput);
    }

    private void ThrowStone()
    {
        isoRenderer.Cast();
        trajectory.Hide();
        trajectory.gameObject.SetActive(false);
        var way = Stone.AddComponent<Waypoints>();
        way.Points = trajectory.GetPoints();
        Stone = null;
    }

    private void TakeStone()
    {
        Collider2D[] hitTargets = Physics2D.OverlapCircleAll(SelectionArea.position, SelectionArea.lossyScale.x / 2, LayerMask.GetMask("Stone"));
        if (hitTargets.Length == 0) return;
        trajectory.gameObject.SetActive(true);
        trajectory.Show();

        var rock = hitTargets[0].GetComponentInParent<Stone>();
        Stone = rock.Pick_Up();
        var collider = Stone.GetComponentInChildren<Collider2D>();
        collider.enabled = false;
    }
}