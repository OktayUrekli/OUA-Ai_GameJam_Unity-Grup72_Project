using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasButtonManager : MonoBehaviour
{

    [SerializeField] GameObject stopPanel,agePanel,civilDeadPanel;
    
    void Start()
    {
        stopPanel.SetActive(false);
        agePanel.SetActive(false);
        civilDeadPanel.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
           PauseGameButton();
        }
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void PauseGameButton()
    {
        Time.timeScale = 0f;
        stopPanel.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        stopPanel.SetActive(false);
    }

    public void ReturnMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartGameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
