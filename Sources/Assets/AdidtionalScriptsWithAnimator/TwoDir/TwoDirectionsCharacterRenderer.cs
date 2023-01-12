using Character.Enums;
using UnityEngine;

public class TwoDirectionsCharacterRenderer : CharacterRenderer
{
    public override void SetDirection(Vector2 direction)
    {
        var newDirection = DirectionToIndex(direction, 8);
        if (newDirection <= 3 && newDirection >= 1 ) lastDirection = (int)Directions.W;
        else if (newDirection <= 7 && newDirection >= 5) lastDirection = (int)Directions.E;
        animator.SetInteger("Direction", lastDirection);
    }
}
