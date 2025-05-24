using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeOutPanel : MonoBehaviour
{
    public Image panelImage;        // Arrastra el Image del panel
    public float fadeDuration = 2f; // Duración del fade
    public AudioSource audioSource; // Arrastra el AudioSource
    public string nextScene = "MainMenu"; // Nombre de la escena a cargar

    void Start()
    {
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // FADE OUT (panel se hace transparente)
        yield return StartCoroutine(Fade(1f, 0f)); // de opaco a transparente

        // Reproducir sonido
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

        // Esperar 1 segundo adicional
        yield return new WaitForSeconds(1f);

        // FADE IN (panel se hace visible nuevamente)
        yield return StartCoroutine(Fade(0f, 1f)); // de transparente a opaco

        // Cargar la nueva escena
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator Fade(float fromAlpha, float toAlpha)
    {
        float elapsed = 0f;
        Color color = panelImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, elapsed / fadeDuration);
            panelImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        panelImage.color = new Color(color.r, color.g, color.b, toAlpha); // asegurar valor final
    }
}
