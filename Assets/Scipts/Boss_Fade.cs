using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fade : MonoBehaviour
{
    public int hitCount = 0;   // จำนวนครั้งที่โดนโจมตี
    public int changeShapeLimit = 10; // จำนวนกระสุนที่ต้องโดนก่อนเปลี่ยนรูป
    public int deathLimit = 30; // จำนวนกระสุนที่ต้องโดนก่อนบอสตาย
    public Sprite newShape;    // รูปภาพใหม่เมื่อบอสเปลี่ยนรูป
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // ดึง SpriteRenderer ของบอส
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่ากระสุนที่ชนเป็นของผู้เล่น
        if (other.CompareTag("PlayerBullet"))
        {
            hitCount++; // เพิ่มจำนวนครั้งที่โดนกระสุน
            Destroy(other.gameObject); // ทำลายกระสุนเมื่อชนกับบอส

            if (hitCount == changeShapeLimit)
            {
                ChangeShape(); // เปลี่ยนรูปทรงของบอส
            }

            if (hitCount >= deathLimit)
            {
                Die(); // บอสตาย
            }
        }
    }

    private void ChangeShape()
    {
        if (newShape != null)
        {
            spriteRenderer.sprite = newShape; // เปลี่ยน Sprite ของบอส
        }
    }

    private void Die()
    {
        Debug.Log("Boss Died!"); // แสดงข้อความใน Console
        Destroy(gameObject); // ลบบอสออกจากเกม
    }
}