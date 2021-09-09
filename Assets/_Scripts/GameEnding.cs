using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{

    public float fadeDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBagraundImageCanvasGroup;
    public CanvasGroup caugthBackgroundImageCanvasGroup;
    public float displayImagenDuration = 1f;
    private bool isPlayerAtExit, isPlayerCaught;
    private float timer;
    public AudioSource exitAudio, caughtAudio;
    private bool hasAudioPlayed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLevel(exitBagraundImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLevel(caugthBackgroundImageCanvasGroup, true, caughtAudio);
        }
    }

    /// <summary>
    /// Lanza la imagen de fin de la partida
    /// </summary>
    /// <param name="imageCanvasGroup">Imagen de fin de partida correspondiente</param>
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        { 
            audioSource.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;
        imageCanvasGroup.alpha = Mathf.Clamp(timer / fadeDuration, 0, 1);

        if (timer > fadeDuration + displayImagenDuration)
        {

            if (doRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            { 
            Application.Quit();
            }
        }    
    }

    public void CatchPlayer()
    {
        isPlayerCaught = true;
    }
}
