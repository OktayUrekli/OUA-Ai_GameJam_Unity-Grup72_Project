using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalSceneCanvasManager : MonoBehaviour
{
   public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGameMenu()
    {
        Application.Quit();
    }
}
