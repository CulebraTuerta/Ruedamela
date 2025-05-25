using Unity.VisualScripting;
using UnityEngine;

public class Points : MonoBehaviour
{
    public float velocidadRotacion = 0.005f;

    public AudioClip pointSound;
    public AudioClip lastPointSound;
    public ParticleSystem pointParticles;

    // Update is called once per frame
    void Update()
    {
        RotatePoint();
    }

    void RotatePoint()
    {
        transform.Rotate(new Vector3(14,45,30)*velocidadRotacion); //tambien puede ser Time.deltatime()
    }

    public void GetPoints()
    {
        //vamos a hacer referncia a un sonido y a particulas
        //Instanciar Sonido
        if(pointSound != null)
        {
            GameObject audioObject = new GameObject("PointEffectSound"); //aqui lo que se busca es poder crear un objeto de sonido que luego sea destruible... aun no se el motivo.
            AudioSource audioSource = audioObject.AddComponent<AudioSource>(); //con esto le damos el audio source al gameobject que acabamos de crear

            if (GameManager.instance.GetUltimoPunto())
            {
                audioSource.clip = lastPointSound; //aqui decimos que el sonido es el que determinamos manualmente para la variable pointSound en la interfaz de unity
            }
            else
            {
                audioSource.clip = pointSound; //aqui decimos que el sonido es el que determinamos manualmente para la variable pointSound en la interfaz de unity
            }
            audioSource.volume = 0.2f; //esto se lo puse yo, para bajar el volumen del sonido.
            audioSource.Play(); //aqui lo reproducimos
            Destroy(audioObject, pointSound.length); //y aqui destruimos el objeto con el sonido, pero esperando el mismo tiempo que la duracion del sonido
        }
        //Instanciar Particulas
        if(pointParticles != null)
        {
            ParticleSystem particulas = Instantiate(pointParticles, transform.position, Quaternion.identity); //con esto creamos un objeto de particulas llamado particulas y lo iniciamos
            particulas.Play(); //con esto la ejecutamos
            Destroy(particulas.gameObject,particulas.main.duration); //y luego borramos como lo anterior, pero con la duracion del clip de particulas como tiempo de espera.
        }

        GameManager.instance.AddPoint(1); //recordar que el instance, es el gamemanager actual
        Destroy(gameObject);

    }




}
