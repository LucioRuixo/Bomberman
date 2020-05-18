using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject controlsMenu;

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GoToControls()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void Return()
    {
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}