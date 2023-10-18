using UnityEngine;

public class Timer : MonoBehaviour
{
    public delegate void timerFinisheHandler();
    public event timerFinisheHandler TimerFinished;
    public bool isActive { get; private set; }
    public float timeLeft { get; private set; }

    private float duration;
    private bool  isLooping;
    private bool shouldStop;

    public void StartTimer(float time, bool isloop)
    {
        duration  = time;
        timeLeft  = duration;
        shouldStop = false;
        isActive  = true;
        isLooping = isloop;
    }

    public void StopTimer()
    {
        shouldStop = true;
    }

    private void Update()
    {
        if(!isActive)
        {
            return;
        }

        if(timeLeft <= 0)
        {
            Debug.Log("Time left");
            TimerFinished.Invoke();
            timeLeft = duration;
            if(isLooping == false || shouldStop)
                isActive = false;
            return;
        }
        timeLeft -= Time.deltaTime;
    }

}
