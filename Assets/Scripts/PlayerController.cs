using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    public float velocidad;
    float moveX, moveY; //esto es para determinar los movimento en los ejes... (aunque deberia ser z no? ya que el y es vertical)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //con esto al objeto de tipo rigidbody que se cargue al rb, obtendremos sus valores y sus propiedades.    
    }

    // FixedUpdate es llamado a cada instante, y no frame por frame.. por ende es mas adecuado para cuando se necesita trabajar con fuerzas
    //o propiedades del fixed body
    private void FixedUpdate() 
    {
        Vector3 movimiento= new Vector3(moveX, 0 ,moveY);
        rb.AddForce(movimiento * velocidad);
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 move = moveValue.Get<Vector2>();
        moveX = move.x;
        moveY = move.y;
    }

    private void OnTriggerEnter(Collider other) //pense que se pondria en el objeto point, pero estamos en el player....
    {
        if(other.gameObject.CompareTag("Point")) //no olvidar poner este tag al objeto point
        {
            other.gameObject.SetActive(false);
        }
    }
}
