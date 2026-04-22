using UnityEngine;

public class SpikeTrap : FloorTrap
{
    protected override void FloorEnter(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.Dead();
        }
    }
}
