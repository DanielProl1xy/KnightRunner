using UnityEngine;

public abstract class Pickupable : MonoBehaviour
{
    protected abstract void OnPickUp();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player pObj = collision.gameObject.GetComponent<Player>();
        if (pObj)
        {
            OnPickUp();
            Debug.Log(pObj + ": picked up " + this);
            Destroy(gameObject);
        }
    }
}
