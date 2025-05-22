using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    public float velocidad;
    float moveX, moveY; //esto es para determinar los movimento en los ejes... (aunque deberia ser z no? ya que el y es vertical)

    public float jumpForce;
    public int maxJumps = 2; //doblesalto
    private int countJump = 0;
    private bool isGround;
    public float airControl=0.5f;
    public float resistenciaVelocidadLineal = 0.95f;
    public Vector3 posicionInicial;

    private void Awake()
    {
        posicionInicial = transform.position; //esto funciona, esta guardando la posicion inicial... 
    }

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

        if(isGround)
        {
            rb.AddForce(movimiento * velocidad);
        }
        else
        {
            rb.AddForce(movimiento*velocidad * airControl); //osea el mismo movimiento que hago en tierra, pero aplicado al aire con un porcentaje menor

        }
        rb.linearVelocity = new Vector3(rb.linearVelocity.x * resistenciaVelocidadLineal, rb.linearVelocity.y, rb.linearVelocity.z * resistenciaVelocidadLineal); //supuestamente esto reduce un poco
        // el deslizamiento de la esfera
        //claro mantengo la misma velocidad lineal en y (para arriba y abajo) y las otras velocidades lineales las bajo a un 90%, casi que diciendo que 
        //pongo un 10% de resistencia... por ende deberia parar...
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 move = moveValue.Get<Vector2>();
        moveX = move.x;
        moveY = move.y;
    }

    void OnJump(InputValue valorSalto)
    {
        if (countJump<maxJumps)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); //con esto mantengo los valores en x,z, ya que en Y los vamos a modificar
            rb.AddForce(Vector3.up*jumpForce,ForceMode.VelocityChange); //el up si le pones el mouse encima le aparece un vector normal en Y, osea que solo toca aplicarle la fuerza salto
            countJump++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            countJump = 0;
            isGround = true;
        }        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }

    private void OnTriggerEnter(Collider other) //pense que se pondria en el objeto point, pero estamos en el player....
    {
        if(other.gameObject.CompareTag("Point")) //no olvidar poner este tag al objeto point
        {
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            rb.transform.position = posicionInicial; //el profe hizo un codigo aparte, del enemigo, que llamaba al player, pero creo que es mejor aqui mismo...
        }
    }
}
