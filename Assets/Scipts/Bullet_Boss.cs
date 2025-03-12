using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Boss : MonoBehaviour
{
    private Vector2 moveDirection;
    private float speed;
    private float expandRate;

    public void SetDirection(Vector2 direction, float initialSpeed, float expandSpeed)
    {
        moveDirection = direction;
        speed = initialSpeed;
        expandRate = expandSpeed;
    }

    void Update()
    {
        speed += expandRate * Time.deltaTime; // ทำให้กระสุนขยายออกเรื่อยๆ
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
