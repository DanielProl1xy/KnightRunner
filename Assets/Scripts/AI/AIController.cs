using System.Collections.Generic;
using UnityEngine;

// AIController class
// implements API for super-simple path-finding for bots
// acceptanceRadius is a distance which bot must pass to catch target
// IsAtPosition() returns if distance between target and own position less than acceptance radius

public abstract class AIController : MonoBehaviour
{

    public float   acceptanceRadius;
    public float   agentRadius;
    public float   avoidRadius;
    public bool    avoidObstacles;
    public Vector3 destination { get; private set; }
    public Enemy possesed;
    public List<GameObject> obstacles { get; private set; }

    private Vector2 objectDirection;
    private Vector2 moveDirection;

    public bool IsAtPosition()
    {
        return (transform.position - destination).magnitude < acceptanceRadius;
    }

    public void GoToPosition(Vector3 posistion)
    {
        destination = posistion;
        obstacles = new List<GameObject>();

        Debug.Log(this + ": moving to " + posistion);

        move();
    }

    public void GoToObject(GameObject obj)
    {
        GoToPosition(obj.transform.position);
    }
    
    private void move()
    {
        if (possesed == null) return;

        if (IsAtPosition())
        {
            Debug.Log(this + ": already at position");
            return;
        }

        objectDirection = (destination - possesed.transform.position).normalized;
        moveDirection = objectDirection;

        if(avoidObstacles)
        {
            checkForObstacles(moveDirection);
        }

        possesed.ProcessAIInput(moveDirection);
    }

    private void checkForObstacles(Vector2 direction)
    {
        List<Trace> traces = EnvironmentTracer.TraceAround(transform.position, direction, 180, 10, agentRadius, avoidRadius * 7);

        float maxFreeDistance = 0;
        float minFreeDistance = Mathf.Infinity;
        float minTargetDistance = Mathf.Infinity;
        float distance;
        bool anyFree = false;

        foreach(Trace trace in traces)
        {
            if (trace.hit == false)
            {
                // Check for closest point to destination
                distance = Vector2.Distance(trace.end, destination);
                if (distance < minTargetDistance)
                {
                    minTargetDistance = distance;
                    moveDirection = trace.direction;
                }
                anyFree = true;
            }

            Debug.DrawLine(trace.start, trace.end, Color.blue, Time.deltaTime);
        }

        foreach (Trace trace in traces)
        {
            if (trace.hit)
            { 
                if (trace.hit.distance > maxFreeDistance && anyFree == false)
                {
                    maxFreeDistance = trace.hit.distance;
                    moveDirection = trace.direction;
                }

                if (trace.hit.distance < avoidRadius)
                {
                    if(trace.hit.distance < minFreeDistance)
                    {
                        minFreeDistance = trace.hit.distance;
                        moveDirection = (moveDirection + trace.hit.normal).normalized;
                    }
                }

                if (obstacles.Contains(trace.hit.transform.gameObject)) continue;

                obstacles.Add(trace.hit.transform.gameObject);                    
            }
        }
    }
}
