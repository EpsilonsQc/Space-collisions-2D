using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float timer = 7.5f; // seconds

    // References
    private PhysicalWorld physicalWorld;
    private PhysicalBody physicalBody;

    private void Awake()
    {
        physicalWorld = FindObjectOfType<PhysicalWorld>();
        physicalBody = GetComponent<PhysicalBody>();
    }

    void Update()
    {
        SelfDestructs();
    }

    private void SelfDestructs()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject); // destroy the object
            physicalWorld.bodyList.Remove(physicalBody); // remove the object from the list
        }
    }
}