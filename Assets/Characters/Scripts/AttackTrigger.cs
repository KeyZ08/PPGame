using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    private MovementBasis controller;
    void Awake()
    {
        controller = GetComponentInParent<MovementBasis>();
    }

    public void Hit()
    {
        controller.Attack();
    }
}
