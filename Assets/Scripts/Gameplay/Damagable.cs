using UnityEngine;

public abstract class Damagable : MonoBehaviour
{
    public float maxHealth;
    public float health { get; protected set; }

    private DropManager dropManager;

    public abstract void AcceptDamage(float amount);

    protected void drop()
    {
        if(!dropManager)
        {
            dropManager = GetComponent<DropManager>();
        }

        if(dropManager)
        {
            Instantiate<GameObject>(dropManager.GetObjectToDrop(), transform.position, transform.rotation);
        }
    }
}
