using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float maxShootingDistance = 100f;
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private Vector3 ammoOffset = new Vector3(0, 4.0f, 0);
    private float cooldownTimer = 0.0f;
    private GameObject ammoParent;
    private GameObject player;

    private void Awake()
    {
        ammoParent = GameObject.Find("Enemies Ammo");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        cooldownTimer -= Time.deltaTime;

        if( cooldownTimer <= 0 && player != null && Vector3.Distance(transform.position, player.transform.position) < maxShootingDistance)
        {
            cooldownTimer = fireRate;
            Vector3 offset = transform.rotation * ammoOffset;
            GameObject bulletGO = (GameObject)Instantiate(ammoPrefab, transform.position + offset, transform.rotation, ammoParent.transform);
        }
    }
}