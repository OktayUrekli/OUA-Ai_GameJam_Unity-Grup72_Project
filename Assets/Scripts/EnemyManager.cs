using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] float enemySpeed;

    Vector3 targetPos;
    [SerializeField] GameObject ways;
    [SerializeField] Transform[] wayPoints;

    int pointIndex, pointCount, speedMultiplier = 1, direction = 1;

    [SerializeField] int waitDuration;

    bool canSeePlayer;

    //Animator animator;

    [SerializeField] Transform gunPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;

    [SerializeField] float timeBetweenFire;
    bool canFire;
    float fireTimer;

    private void Awake()
    {
        wayPoints = new Transform[ways.transform.childCount];
        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            wayPoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }
    private void Start()
    {
        //animator = GetComponent<Animator>();
        
        fireTimer = 0;
        canFire = true;

        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;

    }

    void Update()
    {
        CanPlayerFire();
        RotateEnemy();
        MoveToTargerPos();
        FireTrigger();
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
            //animator.SetTrigger("Fire");
            fireTimer = 0;
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(gunPoint.up * bulletForce, ForceMode.Impulse);           
        }

    }

    void MoveToTargerPos()
    {
        var step = speedMultiplier * enemySpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
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
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
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


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSeePlayer = true;
            targetPos= other.transform.position;
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




    /*

    Animator animator;

    [SerializeField] Transform gunPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;

    [SerializeField] float timeBetweenFire;
    bool canFire;
    float fireTimer;

    bool canSeePlayer;

    [SerializeField] float enemySpeed;

    Vector3 targetPos;
    [SerializeField] GameObject ways;
    [SerializeField] Transform[] wayPoints;

    int pointIndex, pointCount, speedMultiplier = 1, direction = 1;

    [SerializeField] int waitDuration;


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
        animator = GetComponent<Animator>();
        fireTimer = 0;
        canFire = true;
        pointCount = wayPoints.Length;
        pointIndex = 1;
        targetPos = wayPoints[pointIndex].transform.position;
    }


    void Update()
    {
        CanPlayerFire();
        MoveToTargerPos();
    }

    void MoveToTargerPos()
    {
        var step = speedMultiplier * enemySpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
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
        Transform tar = wayPoints[pointIndex].transform;
        RotateEnemy(tar);
        StartCoroutine(WaitForNextPoint());
    }

    IEnumerator WaitForNextPoint()
    {
        speedMultiplier = 0;
        waitDuration = Random.Range(3, 6);
        yield return new WaitForSeconds(waitDuration);
        speedMultiplier = 1;
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

        if (canFire&& canSeePlayer)
        {
            animator.SetTrigger("Fire");
            fireTimer = 0;
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(gunPoint.up * bulletForce, ForceMode.Impulse);
        }

    }


    void RotateEnemy(Transform pos)
    {
        Vector3 target = pos.position;
        Vector3 lookPos = target - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        Vector3 aimDirection = new Vector3(target.x, 0, target.z);

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.7f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSeePlayer = true;
            RotateEnemy(other.transform);
        }else  canSeePlayer = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSeePlayer = false;
            NextPoint();
        }
    }*/
}
