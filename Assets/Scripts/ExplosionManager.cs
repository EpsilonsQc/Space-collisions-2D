using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [Header("Explosion settings")]
    [SerializeField][Range(0, 10)] private float radius;

    [Header("Particles Parameters")]
    [SerializeField] private int quantity = 25;
    [SerializeField] private float speed = 0.75f;
    [SerializeField] private float mass = 0.5f;
    [SerializeField] private float lifespan = 1.0f;

    [Header("Particles Color")]
    [SerializeField] private Color startColor = Color.red;
    [SerializeField] private Color endColor = Color.yellow;

    [Header("Particles Prefab")]
    [SerializeField] private GameObject particlesPrefab;


    // Particles Container
    private GameObject particlesParent;

    // Velocity
    private Vector3 velocity;

    private void Awake()
    {          
        particlesParent = GameObject.Find("===== PARTICLES =====");
    }

    public void ParticlesExplosion()
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject particle = Instantiate(particlesPrefab, this.transform.position, Quaternion.identity, particlesParent.transform);
            Particle thisParticle = particle.GetComponent<Particle>();

            thisParticle.name = "Particle_" + i;

            thisParticle.mass = mass;
            thisParticle.lifespan = lifespan;

            thisParticle.startColor = startColor;
            thisParticle.endColor = endColor;

            thisParticle.velocity.x = Mathf.Cos(Mathf.Abs(i * 360 / quantity) * Mathf.Deg2Rad) * speed; 
            thisParticle.velocity.y = Mathf.Sin(Mathf.Abs(i * 360 / quantity) * Mathf.Deg2Rad) * speed; 

            thisParticle.applyForce = true;
        }
    }
}