using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 30f;

    void Update()
    {
        if (target != null)
        {
            transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}

