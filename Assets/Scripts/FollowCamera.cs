using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public float maxDistX;
    public float maxDistY;
    public float maxSpeed;

    private float xDist;
    private float yDist;

    private void Start()
    {
        Application.targetFrameRate = 90;
    }

    private void Update()
    {
        xDist = Mathf.Abs(transform.position.x - Player.main.transform.position.x);
        yDist = Mathf.Abs(transform.position.y - Player.main.transform.position.y);
         
        if (xDist >= maxDistX)
        {
            int xDir = transform.position.x > Player.main.transform.position.x ? -1 : 1;

            transform.Translate(Vector3.right * xDir * Mathf.Sqrt(maxSpeed * xDist) * Time.deltaTime);
        }

        if (yDist >= maxDistY)
        {
            int yDir = transform.position.y > Player.main.transform.position.y ? -1 : 1;

            transform.Translate(Vector3.up * yDir * Mathf.Sqrt(maxSpeed * yDist) * Time.deltaTime);
        }
    }
}
