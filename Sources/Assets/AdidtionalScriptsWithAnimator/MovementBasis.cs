using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementBasis : MonoBehaviour
{
    [NonSerialized]
    public CharacterRenderer isoRenderer;
    [NonSerialized]

    internal Rigidbody2D _rbody;
    public float movementSpeed = 1f;
    public bool isAnimation { 
        get { return isoRenderer.isAnimation; } 
    }

    internal virtual void Awake()
    {
        _rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<CharacterRenderer>();
    }

    public virtual void Move(float horizontalInput, float verticalInput)
    {
        Vector2 currentPos = _rbody.position;
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        _rbody.MovePosition(newPos);
    }

    public virtual void Hurt()
    {
        isoRenderer.Hurt();
    }

    public virtual void Death()
    {
        isoRenderer.Death();
        foreach (var c in GetComponentsInChildren<Collider2D>())
            c.enabled = false;
        _rbody.bodyType = RigidbodyType2D.Static;
        GetComponent<Entity>().enabled = false;
        Destroy(this);
    }

    public abstract void Attack();
}
