using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void CloseApp() //esto cierra el juego
    {
        //Debug.Log("Cerrando Juego");
        Application.Quit();
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void pauseGame()
    {
        Time.timeScale = 0; //esto hace que la ejecucion del juego se fricee
    }
    public void playGame()
    {
        Time.timeScale = 1; //esto hace que la ejecucion del juego continue a ritmo normal
    }
}
