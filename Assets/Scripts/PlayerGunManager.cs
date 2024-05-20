using UnityEngine;

public class PlayerGunManager : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip fire;

    [SerializeField] Transform gunPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;

    [SerializeField] float timeBetweenFire;
    bool canFire;
    float timer;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        timer = 0;
        canFire = true;
    }

    
    void Update()
    {
        CanPlayerFire();
        FireTrigger();
    }

    void CanPlayerFire()
    {
        timer += 1*Time.deltaTime;
        if (timer > timeBetweenFire)
        {
            canFire = true;
            
        }else
        {
            canFire=false;
        }
    }

    void FireTrigger()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            animator.SetTrigger("Fire");
            audioSource.PlayOneShot(fire);
            timer = 0;
            GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(gunPoint.forward * bulletForce, ForceMode.Impulse);
          
        }
    }
}
