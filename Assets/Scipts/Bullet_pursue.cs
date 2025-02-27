using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_pursue : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วของกระสุน
    public float lifetime = 1.5f; // เวลาที่กระสุนจะหายไปหลังจากยิง

    private void Start()
    {
        // ทำให้กระสุนหายไปหลังจากเวลาที่กำหนด
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // เคลื่อนที่กระสุนไปในทิศทางที่มันหมุน
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
}