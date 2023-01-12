using System;
using UnityEngine;

public class Shadow : Enemy
{
    public Shadow() : base(Enemies.Shadow) { }
    private void FixedUpdate()
    {
        if (movementController.isAnimation) return;
        float horizontalInput = Input.GetAxis("HorizontalJL");
        float verticalInput = Input.GetAxis("VerticalIK");
        movementController.Move(horizontalInput, verticalInput);
    }
}
