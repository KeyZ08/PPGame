using Character.Enums;
using UnityEngine;

public class FourDirectionsCharacterRenderer : CharacterRenderer
{
    public override void SetDirection(Vector2 direction)
    {
        if (direction.magnitude < .01f)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            lastDirection = DirectionToIndex(direction, 8);
            if (lastDirection <= 7 && lastDirection >= 5) lastDirection = (int)Directions.E;
            else if (lastDirection <= 3 && lastDirection >= 1) lastDirection = (int)Directions.W;
            animator.SetInteger("Direction", lastDirection);
            animator.SetBool("Run", true);
        }
    }
}
