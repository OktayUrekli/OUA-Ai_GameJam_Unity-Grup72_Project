using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuCanvasManager : MonoBehaviour
{
    [SerializeField] GameObject howToPlayPanel,creditsPanel;
  
    
    void Start()
    {

        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void SettingsButon()
    {
        howToPlayPanel.SetActive(true);
    }
    public void CreditsButon()
    {
        creditsPanel.SetActive(true);
    }
    public void QuitGameButon()
    {
        Application.Quit();
    }

}
