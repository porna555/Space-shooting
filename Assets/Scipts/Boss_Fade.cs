using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fade : MonoBehaviour
{
    public int hitCount = 0;  // จำนวนครั้งที่ถูกโจมตี
    public int hitLimit = 10; // จำนวนกระสุนที่ต้องโดนก่อนเปลี่ยนรูป
    public Sprite newShape;   // รูปภาพใหม่เมื่อบอสเปลี่ยนรูป

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        // เริ่มต้น spriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่ากระสุนจากผู้เล่นชนกับ BoxCollider ของบอส
        if (other.CompareTag("PlayerBullet"))  // ตรวจสอบแท็กของกระสุน
        {
            hitCount++; // เพิ่มจำนวนการโดน
            Destroy(other.gameObject); // ทำลายกระสุนเมื่อโดน

            if (hitCount >= hitLimit)
            {
                ChangeShape(); // เปลี่ยนรูปทรงของบอส
            }
        }
    }

    private void ChangeShape()
    {
        if (newShape != null)
        {
            // เปลี่ยน sprite เป็นรูปใหม่
            spriteRenderer.sprite = newShape;
            // หรือสามารถเพิ่มการเปลี่ยนแปลงอื่น ๆ เช่นการเปลี่ยนขนาด, การเปลี่ยนพฤติกรรม ฯลฯ
        }
    }
}