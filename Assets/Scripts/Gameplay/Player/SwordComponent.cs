using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordComponent : MonoBehaviour
{
    public float length;
    public float damageAmount;
    public float attackDelay;
    public bool isLooping;

    private Timer attackTimer;

    public void StartAttack()
    {
        attack();
        attackTimer.StartTimer(attackDelay, isLooping);
    }

    public void StopAttack()
    {
        attackTimer.StopTimer();
    }

    private void attack()
    {
        List<Trace> traces = EnvironmentTracer.TraceAround(transform.position, transform.up, 60, 20, 2, length);
        List<Damagable> damaged = new List<Damagable>();

        foreach (Trace trace in traces)
        {

            if (!trace.hit)
            {
                Debug.DrawLine(trace.start, trace.end, Color.blue, 1);
                continue;
            }

            Debug.DrawLine(trace.start, trace.end, Color.red, 1);

            Damagable dmgComp = trace.hit.transform.gameObject.GetComponent<Damagable>();
            if (dmgComp)
            {
                if (damaged.Contains(dmgComp))
                {
                    continue;
                }

                damaged.Add(dmgComp);
                dmgComp.AcceptDamage(damageAmount);
            }
        }
    }

    private void Start()
    {
        attackTimer = gameObject.AddComponent<Timer>();

        attackTimer.TimerFinished += attack;
    }

}
