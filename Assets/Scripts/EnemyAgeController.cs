using UnityEngine;

public class EnemyAgeController : MonoBehaviour
{
    Material enemyStateColor;
    [SerializeField] GameObject civilDeadPanel;
    [SerializeField] GameObject door;

    private void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        enemyStateColor = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && enemyStateColor.color!=Color.blue)
        {
            door.GetComponent<DoorControl>().OneEnemyDead();
            gameObject.GetComponent<EnemyManager>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else if (collision.gameObject.CompareTag("Bullet") && enemyStateColor.color == Color.blue)
        {
            Time.timeScale = 0f;
            civilDeadPanel.SetActive(true);
        }
    }
}
