using UnityEngine;
using UnityEngine.SceneManagement;
using static MainMenu;

public class EndGame : MonoBehaviour
{
    [SerializeField] private AudioSource endGameMusic;
    void Start()
    {
        if (needToTurnMusic)
        {
            endGameMusic.Play();
        }
    }
    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LeaveGameButton()
    {
        Application.Quit();
    }
}
