using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5.0f;

    void Update()
    {
        MoveForwards();
    }

    private void MoveForwards()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
    }
}