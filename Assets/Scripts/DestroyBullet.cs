using UnityEngine;

public class DestroyBullet : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        DestroyThisBullet();
    }

    void Start()
    {
      Invoke("DestroyThisBullet", 1.5f);
    }


    private void DestroyThisBullet()
    {
        Destroy(gameObject);
    }

}
