using UnityEngine;

public class Enemy : Damagable
{
    public float defence;
    public bool  canMove { get; private set; }
    public float maxSpeed;
    public float damage;
    public float attackLength;

    private Timer attackTimer;
    private Vector3 firstEncounterPos;

    public void ProcessAIInput(Vector2 moveDirection)
    { 
        if(canMove)
        {
            transform.Translate(moveDirection * maxSpeed * Time.deltaTime);
        }
    }

    public override void AcceptDamage(float amount)
    {
        if(health <= 0)
        {
            drop();
            Destroy(gameObject);
            return;
        }
        Debug.LogWarning(this + ": accept damage " + amount);
        health -= amount / defence;
    }

    public void StartAttack()
    {
        Debug.LogWarning(this + ": start attack");

        if (attackTimer.isActive) return;
        firstEncounterPos = Player.main.transform.position;
        attackTimer.StartTimer(1.3f, false);

        canMove = false;
    }

    private void StopAttack()
    {
        canMove = true;

        Trace attackTrace = EnvironmentTracer.TraceLine(transform.position, firstEncounterPos - transform.position, 1, attackLength);

        if(attackTrace.hit)
        {
            Damagable dmgObj = attackTrace.hit.transform.gameObject.GetComponent<Damagable>();

            if (dmgObj)
            {
                dmgObj.AcceptDamage(damage);
                Debug.LogWarning(this + ": hit " + dmgObj.gameObject);
            }
        }

        Debug.DrawLine(transform.position, attackTrace.end, Color.red, 1f);

    }

    private void Start()
    { 
        health = maxHealth;
        canMove = true;
        attackTimer = gameObject.AddComponent<Timer>();
        attackTimer.TimerFinished += StopAttack;
    }
}
