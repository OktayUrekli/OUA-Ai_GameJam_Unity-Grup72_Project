using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasCountDownManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerTxt;
    public float remainingTime;

    [SerializeField] GameObject timePanel;

    public static GameObject instance;

    private void Awake()
    {
        remainingTime = 300;
    }

    private void Start()
    {
         timePanel.SetActive(false);
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (remainingTime>0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0) 
        {
            remainingTime = 0;
            timerTxt.color = Color.red;
            timePanel.SetActive(true);
        }

       
        int minutes=Mathf.FloorToInt(remainingTime/60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerTxt.text=string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        remainingTime = 300;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
