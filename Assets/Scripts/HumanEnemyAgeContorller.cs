using UnityEngine;

public class HumanEnemyAgeContorller : MonoBehaviour
{
    [SerializeField] GameObject mainBody;

    [SerializeField] Material enemyMat, humanMat;

    [SerializeField] Material currentMaterial;
    [SerializeField] GameObject civilDeadPanel;
    [SerializeField] GameObject door;

    Animator animator;
    bool stateHuman;

    AudioSource audioSource;
    [SerializeField] AudioClip civilKillClip,enemyDead;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        stateHuman = false;
        door = GameObject.FindGameObjectWithTag("Door");
        mainBody.GetComponent<Renderer>().material=enemyMat;
        currentMaterial = mainBody.GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && currentMaterial!= humanMat)
        {
            stateHuman=true;
            animator.SetBool("NormalState", stateHuman);
            mainBody.GetComponent<Renderer>().material = humanMat;
            currentMaterial = humanMat;
            door.GetComponent<DoorControl>().OneEnemyDead();
            gameObject.GetComponent<HumanEnemyMovementManager>().enabled = false;
            audioSource.PlayOneShot(enemyDead);
            
        }
        else if (collision.gameObject.CompareTag("Bullet") && currentMaterial== humanMat)
        {
            audioSource.PlayOneShot(civilKillClip);
            Time.timeScale = 0f;
            civilDeadPanel.SetActive(true);
        }
    }
}
