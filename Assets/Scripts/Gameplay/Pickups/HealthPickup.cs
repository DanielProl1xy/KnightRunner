

public class HealthPickup : Pickupable
{
    public float health;

    protected override void OnPickUp()
    {
        Player.main.AddHealth(health);
    }
}
