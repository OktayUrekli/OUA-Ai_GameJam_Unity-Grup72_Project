using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadNextLevelAfterVideo : MonoBehaviour
{
    [SerializeField] VideoClip video;


    void Start()
    {
        float time = (float)video.length;
        Invoke("NextScene", time+1);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextScene();
        }
    }


    void NextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }else
        {
            SceneManager.LoadScene(0);
        }
        
    }
    
}
