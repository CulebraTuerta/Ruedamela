using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText; //se llama de inmediato a tmpro como libreria
    public static GameManager instance;  //con esto verificamos que exista solo un game manager, en caso de haber un segundo este se eliminaria... (como puede pasar que se duplique?)
    public int TotalScore;
    public GameObject puntos; //esto lo hice yo, agarre un gameobjetc con todos los puntos a recoger, y este gameobject lo relacione con esta variable puntos. Asi despues en el 
    //awake, puedo contar cuantos hijos tiene y eso ponerlo como el puntaje total.
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //con esto hago referencia a este mismo objeto
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        int cantidadHijos = puntos.transform.childCount;
        TotalScore= cantidadHijos;

    }

    private void Start()
    {
        UpdateScoreText();
    }

    private void Update()
    {
        UpdateScoreText();
    }

    public void AddPoint(int point)
    {
        score += point;
        UpdateScoreText();
    }

    public bool GetUltimoPunto() //con esto puedo saber si queda una gema por recoger
    {
        if(TotalScore-score==1)
        {
            return true;
        }
        return false;
    }

    void UpdateScoreText()
    {
        if(scoreText != null) //simplemente como buena practica para verificar si esta funcionando.
        {
            scoreText.text = score.ToString() + "/" +TotalScore.ToString(); //recordar que todos las variables a escribir tiene que estar en string, no puede ser en entero u otros
        }
    }
}
