using System.Collections.Generic;
using UnityEngine;

public struct Trace
{
    public RaycastHit2D hit;
    public Vector2 start;
    public Vector2 end;
    public Vector2 direction { get => (end - start).normalized; }
}

public class EnvironmentTracer : MonoBehaviour
{
    public static List<Trace> TraceAround(Vector2 center, Vector2 forward, 
        float halfAngle, float count, float offset, float length)
    {
        Trace trace;
        List<Trace> traces = new List<Trace>();

        for (int i = 0; i < count; i++)
        {
            float angle = halfAngle * 2 / count * i - halfAngle;

            trace = TraceLine(center, Quaternion.Euler(0.0f, 0.0f, angle) * forward, offset, length);
            traces.Add(trace);
        }
        return traces;
    }

    public static Trace TraceLine(Vector2 start, Vector2 direction, float offset, float length)
    {
        start += direction * offset;
        RaycastHit2D hit = Physics2D.Raycast(start, direction, length);

        Trace trace = new Trace();

        trace.hit = hit;
        trace.start = start;
        trace.end = hit ? hit.point : start + direction * length;

        return trace;
    }
}
