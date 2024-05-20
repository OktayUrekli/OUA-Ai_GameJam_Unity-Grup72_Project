using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollow : MonoBehaviour
{
    public Vector3 offset;
    private float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    private void Awake()
    {
        target=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
