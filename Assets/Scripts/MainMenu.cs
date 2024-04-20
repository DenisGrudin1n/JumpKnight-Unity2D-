using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource mainMenuMusic;
    [SerializeField] private Text sounds;
    [SerializeField] private Text music;
    [SerializeField] private Button soundSelectionYes;
    [SerializeField] private Button soundSelectionNo;
    [SerializeField] private Button musicSelectionYes;
    [SerializeField] private Button musicSelectionNo;  

    public static bool needToTurnMusic = true;
    public static bool needToTurnSound = true;

    public void StartButton()
    {
        if (needToTurnMusic)
        {
            mainMenuMusic.Play();
        }
        SceneManager.LoadScene("Game");
    }

    public void OptionsButton()
    {
        if (sounds.IsActive())
        {
            sounds.gameObject.SetActive(false);
            music.gameObject.SetActive(false);
        }
        else
        {
            sounds.gameObject.SetActive(true);
            music.gameObject.SetActive(true);
        }
    }

    public void SoundsSelectionYesButton()
    {
        needToTurnSound = false;
        soundSelectionYes.gameObject.SetActive(false);
        soundSelectionNo.gameObject.SetActive(true);
    }

    public void SoundsSelectionNoButton()
    {
        needToTurnSound = true;
        soundSelectionYes.gameObject.SetActive(true);
        soundSelectionNo.gameObject.SetActive(false);
    }

    public void MusicSelectionYesButton()
    {
        needToTurnMusic = false;
        mainMenuMusic.volume = 0;
        musicSelectionYes.gameObject.SetActive(false);
        musicSelectionNo.gameObject.SetActive(true);
    }

    public void MusicSelectionNoButton()
    {
        needToTurnMusic = true;
        mainMenuMusic.volume = 0.4f;
        musicSelectionYes.gameObject.SetActive(true);
        musicSelectionNo.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
