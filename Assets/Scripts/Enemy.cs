using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Vector3 posicionIncial;
    public GameObject player;

    public AudioClip enemySound;
    public ParticleSystem enemyParticles;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        posicionIncial = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        //Instanciar Sonido
        if (enemySound != null)
        {
            GameObject audioObject = new GameObject("PointEffectSound"); //aqui lo que se busca es poder crear un objeto de sonido que luego sea destruible... aun no se el motivo.
            AudioSource audioSource = audioObject.AddComponent<AudioSource>(); //con esto le damos el audio source al gameobject que acabamos de crear
            audioSource.clip = enemySound; //aqui decimos que el sonido es el que determinamos manualmente para la variable pointSound en la interfaz de unity
            audioSource.volume = 0.2f; //esto se lo puse yo, para bajar el volumen del sonido.
            audioSource.Play(); //aqui lo reproducimos
            Destroy(audioObject, enemySound.length); //y aqui destruimos el objeto con el sonido, pero esperando el mismo tiempo que la duracion del sonido
        }
        //Instanciar Particulas
        if (enemyParticles != null)
        {
            ParticleSystem particulas = Instantiate(enemyParticles, transform.position, Quaternion.identity); //con esto creamos un objeto de particulas llamado particulas y lo iniciamos
            particulas.Play(); //con esto la ejecutamos
            Destroy(particulas.gameObject, particulas.main.duration); //y luego borramos como lo anterior, pero con la duracion del clip de particulas como tiempo de espera.
        }

        if(player != null) //con esto reseteo la posicion //tambien servia desde el player pero mejor lo dejamos como script aparte.
        {
            player.transform.position = posicionIncial;
        }
    }

}
