using UnityEngine;

public class MapChunk : MonoBehaviour
{
    public bool isPlayerHere { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player pObj = collision.gameObject.GetComponent<Player>();

        if (pObj)
        {
            isPlayerHere = true;
            Debug.Log(this + ": player entered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player pObj = collision.gameObject.GetComponent<Player>();

        if (pObj)
        {
            isPlayerHere = false;
            Debug.Log(this + ": player left");
        }
    }
}
