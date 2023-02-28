using UnityEngine;

public class PhysicalBody : MonoBehaviour
{
    private PhysicalWorld physicalWorld;
    public Vector3 position;
    public float radius = 0.25f;

    public Vector3 Position { get { return position; } set { position = value; } }

    private void Awake()
    {
        physicalWorld = FindObjectOfType<PhysicalWorld>();
        physicalWorld.AddBody(this);
    }

    private void Update()
    {
        position = transform.position;
    }
}