using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    Rigidbody rb;
    public float velocidad;
    float moveX, moveY; //esto es para determinar los movimento en los ejes... (aunque deberia ser z no? ya que el y es vertical)

    public float jumpForce;
    public AudioSource sonidoSalto;
    public AudioSource sonidoGolpeSuelo;
    public AudioSource sonidoVictoria;
    public int maxJumps = 2; //doblesalto
    private int countJump = 0;
    private bool isGround;
    public float airControl=0.5f;
    public float resistenciaVelocidadLineal = 0.95f;
    public ParticleSystem fuegosArtificiales;
    public GameObject panelDeVictoria;
    public GameObject panelUI;
    //private bool esperandoInput = false;

    private float tiempoUltimoSonido = 0f;
    public float intervaloSonido = 0.1f; // tiempo mínimo entre sonidos (en segundos)
    //public Vector3 posicionMeta;

    //private void Awake()
    //{
    //    posicionInicial = transform.position; //esto funciona, esta guardando la posicion inicial... 
    //}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //con esto al objeto de tipo rigidbody que se cargue al rb, obtendremos sus valores y sus propiedades.
        fuegosArtificiales.Pause();

    }

    //private void Update()
    //{
    //    if (esperandoInput && (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame))
    //    {
    //        panelUI.SetActive(false);
    //        panelDeVictoria.SetActive(true);
    //        //esperandoInput=false;
    //    }
    //}

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
            sonidoSalto.Play();
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); //con esto mantengo los valores en x,z, ya que en Y los vamos a modificar
            rb.AddForce(Vector3.up*jumpForce,ForceMode.VelocityChange); //el up si le pones el mouse encima le aparece un vector normal en Y, osea que solo toca aplicarle la fuerza salto
            countJump++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Meta"))
        {
            if (Time.time - tiempoUltimoSonido > intervaloSonido)
            {
                sonidoGolpeSuelo.Play();
                tiempoUltimoSonido = Time.time;
                
            }
            if (collision.gameObject.CompareTag("Meta") && GameManager.instance.TodasLasGemasRecogidas())
            {
                Victoria();
            }
            countJump = 0;
            isGround = true;
        }        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Meta"))
        {
            isGround = false;
        }
    }

    private void OnTriggerEnter(Collider other) //pense que se pondria en el objeto point, pero estamos en el player....
    {
        if(other.gameObject.CompareTag("Point")) //no olvidar poner este tag al objeto point
        {
            //other.gameObject.SetActive(false); //esto lo dejamos de hacer por el gamemanager
            Points point = other.gameObject.GetComponent<Points>(); //estoy metiendo al objeto points dentro de la variable point. el objeto es other... qu een este caso es al objeto con el que colisione y tenga tag Point
            if (point != null)
            {
                point.GetPoints(); //con esto voy al objeto point, busco su metodo get point y dentro del script points este metodo solo hace el agregar un valor al texto, que es manejado por el gamemanager
            }
        }

        //if(other.gameObject.CompareTag("Meta"))  //desactive esto porque lo estoy activando directamente desde el momento que lo toca y tiene las gemas
        //{
        //    if(GameManager.instance.TodasLasGemasRecogidas()) //has cogido todas las gemas
        //    {
        //        Victoria();                
        //    }
        //}
    }

    private void Victoria()
    {
        this.enabled = false; // Desactiva este script para evitar más inputs ,sin esto detectaba error ya que seguia recibiendo una velocidad lineal
        rb.isKinematic = true; //con esto lo friseo
        sonidoVictoria.Play(); 
        fuegosArtificiales.Play();

        Camera.main.GetComponent<CameraOrbit>().enabled = true; //con esto activamos el script de la camara girando //NO ESTA GIRANDO!!!

        //esperandoInput = true;

        panelUI.SetActive(false);
        panelDeVictoria.SetActive(true);

        //hacer girar la camara alrededor de la plataforma
        //activar "paneldevictoria" despues de el jugador haga clic o aprete alguna tecla (no olvidar desactivar panel UI desde aqui tambien

    }
}
