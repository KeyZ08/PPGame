using System.Collections.Generic;
using UnityEngine;

public class AgaricCollectiveBehaviour : MonoBehaviour
{
    private List<AgaricMovementController> PursuitAgarics;
    private List<AgaricMovementController> HarassmentAgarics;
    private Transform player;

    private void Start()
    {
        PursuitAgarics = new List<AgaricMovementController>();
        HarassmentAgarics = new List<AgaricMovementController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Add(AgaricMovementController agaric)
    {
        PursuitAgarics.Add(agaric);
    }

    private void FixedUpdate()
    {
        var pAgarics = PursuitAgarics.ToArray();
        var hAgarics = HarassmentAgarics.ToArray();
        CheckOnDestroy(ref pAgarics, ref hAgarics);

        if (pAgarics.Length != 0) { 
            if (hAgarics.Length < 3 && pAgarics.Length > 0)
            {
                int prCount = 3 - hAgarics.Length;
                foreach (var i in pAgarics)
                {
                    if (prCount == 0) break;
                    HarassmentAgarics.Add(i);
                    PursuitAgarics.Remove(i);
                    prCount--;
                }
            }
        }


        foreach (var i in hAgarics)
        {
            i.Harassment(player.position);
        }

        foreach (var i in pAgarics)
        {
            i.Pursuit(player.position);
        }
    }

    private void CheckOnDestroy(ref AgaricMovementController[] pAgarics, ref AgaricMovementController[] hAgarics)
    {
        foreach (var i in pAgarics)
        {
            if (i == null)
                PursuitAgarics.Remove(i);
        }

        foreach (var i in hAgarics)
        {
            if (i == null)
                HarassmentAgarics.Remove(i);
        }

        pAgarics = PursuitAgarics.ToArray();
        hAgarics = HarassmentAgarics.ToArray();
    }
}
