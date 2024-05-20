using System.Collections;
using UnityEngine;

public class HumanEnemyMovementManager : MonoBehaviour
{
    Animator humanAnimator;

    AudioSource audioSource;
    [SerializeField] AudioClip  enemyFire;

    [SerializeField] float enemySpeed;
    [SerializeField] GameObject ways;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] int waitDuration;

    Vector3 targetPos;
    int pointIndex, pointCount, speedMultiplier = 1, direction = 1;


    [SerializeField] Transform gunPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    [SerializeField] float timeBetweenFire;

    bool canFire;
    float fireTimer;
    bool canSeePlayer;

    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        humanAnimator = GetComponent<Animator>();
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CanPlayerFire();
        RotateEnemy();
        MoveToTargerPos();
        FireTrigger();
    }

    void MoveToTargerPos()
    {
        if (canSeePlayer==false)
        {
            var step = speedMultiplier * enemySpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
        }
        
        if (transform.position == targetPos)
        {
            NextPoint();
        }
    }

    void NextPoint()
    {
        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }
        if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetPos = wayPoints[pointIndex].transform.position;

        StartCoroutine(WaitForNextPoint());
    }

    IEnumerator WaitForNextPoint()
    {
        speedMultiplier = 0;
        waitDuration = Random.Range(3, 6);
        humanAnimator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
        humanAnimator.SetFloat("Speed", 1);
    }

    void RotateEnemy()
    {
        Vector3 lookPos = targetPos - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        Vector3 aimDirection = new Vector3(targetPos.x, 0, targetPos.z);

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.7f);
        }
    }

    void CanPlayerFire()
    {
        fireTimer += 1 * Time.deltaTime;
        if (fireTimer > timeBetweenFire)
        {
            canFire = true;
            
        }
        else
        {
            canFire = false;
        }
    }

    public void FireTrigger()
    {

        if (canFire && canSeePlayer)
        {
            Debug.Log("ateþ ettim");
            humanAnimator.SetTrigger("Fire");
            audioSource.PlayOneShot(enemyFire);
            fireTimer = 0;
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(gunPoint.forward * bulletForce, ForceMode.Impulse);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSeePlayer = true;
            targetPos = other.transform.position;
            speedMultiplier = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSeePlayer = false;
            speedMultiplier = 1;
        }
    }
}
