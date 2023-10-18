using System.Collections.Generic;
using UnityEngine;

public class Prop : Damagable
{

    public override void AcceptDamage(float amount)
    {
        if(health <= 0)
        {
            drop();
            Destroy(gameObject);
            return;
        }
        Debug.Log(this + ": accept damage " + amount);

        health -= Mathf.Abs(amount);
    }

    private void Start()
    {
        health = maxHealth;
    }
}
