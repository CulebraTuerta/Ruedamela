using Unity.VisualScripting;
using UnityEngine;

public class Points : MonoBehaviour
{
    Rigidbody rb;
    public float velocidadRotacion = 0.005f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePoint();

    }

    void RotatePoint()
    {
        transform.Rotate(new Vector3(14,45,30)*velocidadRotacion); //tambien puede ser Time.deltatime()
    }
}
