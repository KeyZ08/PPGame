using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Transform = UnityEngine.Transform;

public class EightDirectionsCharacterRenderer : CharacterRenderer
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
            animator.SetInteger("Direction", lastDirection);
            animator.SetBool("Run", true);
        }
    }
}
