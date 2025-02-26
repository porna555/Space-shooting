using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // ความเร็วของกระสุน
    public Vector2 direction = Vector2.up; // ทิศทางการยิง

    void Update()
    {
        // ให้กระสุนเคลื่อนที่ไปตามทิศทาง
        transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ทำลายกระสุนเมื่อออกนอกจอ
    }
}
