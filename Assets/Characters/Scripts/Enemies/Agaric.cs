using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agaric : Enemy
{
    public Agaric() : base(Enemies.Agaric) { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var gm = collision.gameObject;
        if (gm.TryGetComponent<Player>(out var pl))
        {
            Damage(HP);
        }
    }
}

