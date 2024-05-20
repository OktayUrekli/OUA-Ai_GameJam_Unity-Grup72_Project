using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorControl : MonoBehaviour
{
    Animator animator;

    AudioSource audioSource;
    [SerializeField] AudioClip doorCloseClip,doorOpenClip;

    [SerializeField] GameObject[] enemies;
    [SerializeField] int enemyCount;
    public int currentEnemyCount;

    bool doorOpen;

    private void Awake()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        currentEnemyCount = enemyCount;

    }

    void Update()
    {
        IsDoorOpen();
    }

    public void OneEnemyDead()
    { 
       currentEnemyCount--;
        animator.SetTrigger("TakeDamage");
    }

    void IsDoorOpen()
    {
        if (currentEnemyCount==0)
        {
            animator.SetBool("Open", true);
            doorOpen=true;
            audioSource.PlayOneShot(doorOpenClip);
        }
        else
        {
            animator.SetBool("Open", false);
            doorOpen=false;
        }
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && doorOpen)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.gameObject.CompareTag("Player") && !doorOpen)
        {
            audioSource.PlayOneShot(doorCloseClip);
        }
    }
   

   
}
