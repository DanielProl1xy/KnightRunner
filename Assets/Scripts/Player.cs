using System.Collections.Generic;
using UnityEngine;

public class Player : Damagable
{
    public static Player main;

    public SwordComponent sword;
    public Joystick JoystickInput;

    public float attackDelay;
    public float maxSpeed;
    public float damageAmount;
    public float startTimeLimit;
    public float stunLength;
    public bool healthTicking;

    private Vector3 moveDirection;
    private Timer stunTimer;

    public void AddHealth(float amount)
    {
        health += amount;
    }

    public override void AcceptDamage(float amount)
    {
        health = Mathf.Clamp(health - amount, 0, Mathf.Infinity);
        stunTimer.StartTimer(stunLength, false);
    }

    private void Start()
    {
        main = this;
        health = startTimeLimit;
        stunTimer = gameObject.AddComponent<Timer>();
        healthTicking = true;
        
        // TODO: refactor sword init
        sword = gameObject.AddComponent<SwordComponent>();
        sword.damageAmount = 15;
        sword.length = 2.5f;
        sword.attackDelay = 0.9f;
        sword.isLooping = true;
    }

    private void Update()
    {
        if(health <= 0)
        {
            Debug.LogWarning(this + ": time limit reached");
            return;
        }

        processInputMovement();

        if(healthTicking)
        {
            health = Mathf.Clamp(health - Time.deltaTime, 0, Mathf.Infinity);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this + ": collision started with: " + collision.gameObject);

        if(collision.gameObject.GetComponent<Damagable>())
        {
            sword.StartAttack();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(this + ": collision ended with: " + collision.gameObject);

        if (collision.gameObject.GetComponent<Damagable>())
        {
            sword.StopAttack();
        }
    }

    private void processInputMovement()
    {
        if (JoystickInput.xAxis == 0 && JoystickInput.yAxis == 0) return;

        moveDirection = JoystickInput.yAxis * Vector3.up
                         + JoystickInput.xAxis * Vector3.right;

        // Calculate speed with stun
        float speed = maxSpeed - (Mathf.Clamp((stunTimer.timeLeft / stunLength), 0.0f, 0.5f) * maxSpeed);

        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

}
