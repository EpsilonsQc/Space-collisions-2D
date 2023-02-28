using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    // References
    private SpriteRenderer spriteRenderer;
    private PhysicalWorld physicalWorld;
    private PhysicalBody physicalBody;
    private EnemySpawner enemySpawner;
    private ExplosionManager explosionManager;

    public int health = 1;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // get the sprite renderer
        physicalWorld = FindObjectOfType<PhysicalWorld>(); // find the physical world
        physicalBody = GetComponent<PhysicalBody>(); // get the physical body component
        enemySpawner = FindObjectOfType<EnemySpawner>(); // find the enemy spawner

        if(this.gameObject.tag == "Player" || this.gameObject.tag == "Enemy")
        {
            explosionManager = GetComponent<ExplosionManager>(); // find the explosion manager
        }
    }

    private void Update()
    {
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(this.gameObject.tag == "Player")
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(enemy);
            }

            foreach (GameObject enemyAmmo in GameObject.FindGameObjectsWithTag("EnemyAmmo"))
            {
                Destroy(enemyAmmo);
            }

            physicalWorld.bodyList.Clear(); // clear the list
            enemySpawner.nextEnemySpawnTimer = 5.0f; // reset the enemy spawn timer to 5 seconds
        }

        if(explosionManager != null) // if the object has an explosion manager
        {
            explosionManager.ParticlesExplosion(); // play the particles explosion
        }

        Destroy(gameObject); // destroy the object
        physicalWorld.bodyList.Remove(physicalBody); // remove the object from the list
    }

    public void TakeDamage()
    {
        health--; // lose health
    }

    public void BounceBack(PhysicalBody body)
    {
        Vector3 direction = (body.transform.position - transform.position).normalized;
        Vector3 force = direction * 10f; // bounce back force
        transform.position = new Vector3(transform.position.x - force.x, transform.position.y - force.y, transform.position.z);
    }
}