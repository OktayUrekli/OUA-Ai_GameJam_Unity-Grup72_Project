using UnityEngine;
using UnityEngine.UI;

public class PlayerAgeController : MonoBehaviour
{
    int playerStarterAge;
    [SerializeField] Image ageBar;
    Animator animator;

    AudioSource audioSource;
    [SerializeField] AudioClip dead;

    [SerializeField] GameObject agePanel;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agePanel.SetActive(false);
        animator = GetComponent<Animator>();
        playerStarterAge = 0;
        UpdateBar();
    }


    void PlayerDeadControl()
    {
        if (playerStarterAge>=100)
        {
            audioSource.PlayOneShot(dead);
            animator.SetBool("isDead", true);
            agePanel.SetActive(true);
            Time.timeScale = 0.3f;
            gameObject.GetComponent<PlayerMovementManager>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            playerStarterAge += 25;
            PlayerDeadControl();
            UpdateBar();
        }
    }

    void UpdateBar()
    {
        ageBar.fillAmount = (float)playerStarterAge/100;
    }

}
