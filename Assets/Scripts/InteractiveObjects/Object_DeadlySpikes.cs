using UnityEngine;

public class Object_DeadlySpikes : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private LayerMask whatIsEnemy;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerBit = 1 << collision.gameObject.layer;
        if ((layerBit & whatIsPlayer) != 0)
        {
            if(collision.TryGetComponent(out Player player))
            {
                player.EntityDeath();
                player.ui.OpenDeathScreenUI();
            }

        }
        else if ((layerBit & whatIsEnemy) != 0)
        {
            if(collision.TryGetComponent(out Enemy enemy))
            {
                if (!enemy.isImuneToSpikes)
                    enemy.EntityDeath();
                else
                    return;
            }
                
        }
    }
}
