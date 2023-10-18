using UnityEngine;

public class Joystick : MonoBehaviour
{
    public bool  isPressed { get; private set; }
    public float xAxis { get; private set; }
    public float yAxis { get; private set; }

    private float maxY;
    private float maxX;

    // Current touch position
    private Vector2 touchPos;
    // Position when touch just started
    private Vector2 lastTouch;

    private Player playerObject;


    private void Start()
    {
        maxX = Screen.width / 10;
        maxY = Screen.height / 8;

        playerObject = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if(!isPressed) {
                lastTouch = Input.GetTouch(0).position;
                isPressed = true;
            }


            touchPos = Input.GetTouch(0).position;

            // xAxis is 0 if touch.x and player.x are equal
            // otherwise xAxis contains what direction player should move to make X similar
            xAxis = Mathf.Clamp((touchPos.x 
                                - Camera.main.WorldToScreenPoint(playerObject.transform.position).x) 
                                / maxX,
                                -1, 1);


            yAxis = Mathf.Clamp((touchPos.y - lastTouch.y) / maxY, -1, 1);

            Debug.DrawRay(Camera.main.transform.position, Vector2.right * 10 * xAxis, Color.red);
            Debug.DrawRay(Camera.main.transform.position, Vector2.up * 10 * yAxis, Color.red);
        }
        else
        {
            if (!isPressed)
                return;

            isPressed = false;
            yAxis = 0;
            xAxis = 0;
        }
    }
}
