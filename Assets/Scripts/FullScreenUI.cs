using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FullScreenUI : MonoBehaviour
{
    public Texture2D gameFlashTexture;
    public Texture2D gameStartScreen;
    public float gameStartFlashDuration = 0.5f;
    public float gameStartFadeDuration = 1f;
    public float gameStartFadeDelay = 1f;
    public Texture2D gameOverScreen;
    public Texture2D gameWonScreen;

	private Texture2D texture;

    private bool stretch = false;
    private bool fading = false;
    private float fadeValue = 0f;
    private float fadeDelay = 0f;
    private float fadeDuration = 1f;

	void Start()
    {
        GameFlash();
        Invoke("GameStart", gameStartFlashDuration + 0.5f);
	}

	void OnGUI()
    {
        Color c = GUI.color;
        c.a = 1;
        if (fading)
        {
            fadeValue += Time.deltaTime;
            float t = Mathf.Min(Mathf.Max(0, fadeValue-fadeDelay)/fadeDuration, 1);
            c.a = 1 + t*(t-2); // invert, quadratic ease out
            if (fadeValue >= fadeDelay + fadeDuration)
            {
                FadeFinished();
            }
        }
        GUI.color = c;

		if (texture != null)
        {
            Rect screenRect = stretch ? new Rect(0, 0, Screen.width, Screen.height) : new Rect((Screen.width - texture.width)/2, (Screen.height - texture.height)/2, texture.width, texture.height);
			GUI.DrawTexture(screenRect, texture, ScaleMode.StretchToFill);
		}
	}

    void GameFlash()
    {
		enabled = true;
        texture = gameFlashTexture;
        stretch = true;
        QueueFade(0, gameStartFlashDuration);
    }

    void GameStart()
    {
		enabled = true;
        texture = gameStartScreen;
        stretch = false;
        QueueFade(gameStartFadeDelay, gameStartFadeDuration);
    }

	void GameOver()
    {
        stretch = false;
        texture = gameOverScreen;
		enabled = true;
        Invoke("RestartLevel", 1.0f);
	}

	void GameWon()
    {
        stretch = false;
        texture = gameWonScreen;
		enabled = true;
        Invoke("RestartLevel", 1.0f);
	}

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void QueueFade(float delay, float duration)
    {
        fading = true;
        fadeValue = 0;
        fadeDelay = delay;
        fadeDuration = duration;
    }

	private void FadeFinished()
    {
        fading = false;
        texture = null;
		enabled = false;
	}
}
