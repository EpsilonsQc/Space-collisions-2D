using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private float fireRate = 0.20f; // fire rate of the ammo
    [SerializeField] private Vector3 ammoOffset = new Vector3(0, 4.0f, 0);
    private float cooldownTimer = 0f;
    private GameObject ammoParent;
    
    
    private void Awake()
    {
        ammoParent = GameObject.Find("Player Ammo");
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer > fireRate)
        {
            if (Input.GetMouseButton(0))
            {
                cooldownTimer = 0f; // reset the timer
                Vector3 offset = transform.rotation * ammoOffset;
                GameObject ammoObject = Instantiate(ammoPrefab, transform.position + offset, transform.rotation, ammoParent.transform);
            }
        }
    }
}