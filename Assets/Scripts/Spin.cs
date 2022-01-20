using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}
