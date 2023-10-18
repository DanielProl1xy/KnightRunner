using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private Text timerText;

    private void Start()
    {
        timerText = GetComponent<Text>();
    }

    private void OnGUI()
    {
        timerText.text = Player.main.health.ToString("0.00");

        if(Player.main.health > 10)
        {
            timerText.color = Color.green;
        }
        else if(Player.main.health > 5)
        {
            timerText.color = Color.yellow;
        }
        else
        {
            timerText.color = Color.red;
        }
        
    }
}
