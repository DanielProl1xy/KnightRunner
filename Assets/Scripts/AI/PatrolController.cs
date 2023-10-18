using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : AIController
{
    public List<TargetPoint> points;

    private int pIndex;

    private void Start()
    {
        pIndex = 0;
        possesed = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (points == null) return;
        if (points.Count == 0) return;
        if (possesed.canMove == false) return;

        if (IsAtPosition())
        {
            if(pIndex < points.Count - 1)
            {
                pIndex += 1;
            }
            else
            {
                pIndex = 0;
            }
        }
        GoToPosition(points[pIndex].transform.position);

        if(obstacles.Contains(Player.main.gameObject))
        {
            possesed.StartAttack();
        }
    }
}
