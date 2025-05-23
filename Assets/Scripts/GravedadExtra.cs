using UnityEngine;

public class GravedadExtra : MonoBehaviour
{
    public float multiplicadorGravedad = 2f;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (multiplicadorGravedad - 1), ForceMode.Acceleration);
    }
}

