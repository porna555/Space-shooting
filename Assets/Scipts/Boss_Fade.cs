using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
public class Boss_Fade : MonoBehaviour
{
    public int hitCount = 0;   // จำนวนครั้งที่โดนโจมตี
    public int changeShapeLimit = 10; // จำนวนกระสุนที่ต้องโดนก่อนเปลี่ยนรูป
    public int deathLimit = 30; // จำนวนกระสุนที่ต้องโดนก่อนบอสตาย
    public Sprite newShape;    // รูปภาพใหม่เมื่อบอสเปลี่ยนรูป
    public Sprite deathShape;  // **รูปภาพสุดท้ายก่อนบอสหายไป**
    public TextMeshProUGUI bossDefeatedTMP; // TextMeshPro สำหรับแสดงข้อความ

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Collider2D col;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        // ซ่อนข้อความ "BOSS DEFEATED!" ตอนเริ่มเกม
        if (bossDefeatedTMP != null)
        {
            bossDefeatedTMP.gameObject.SetActive(false);
        }
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
                StartCoroutine(Die()); // บอสตาย (ใช้ Coroutine)
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

    private IEnumerator Die()
    {
        Debug.Log("Boss Died!"); // แสดงข้อความใน Console

        // หยุดการเคลื่อนที่ของบอส
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // หยุด Rigidbody2D
            rb.isKinematic = true; // ปิด Physics
        }

        // ปิด Collider เพื่อไม่ให้ชนกับอะไรอีก
        if (col != null)
        {
            col.enabled = false;
        }

        // แสดงข้อความ "BOSS DEFEATED!"
        if (bossDefeatedTMP != null)
        {
            bossDefeatedTMP.gameObject.SetActive(true);
            bossDefeatedTMP.text = "!!! YOU WIN !!! Congratulations, you survived Grade F";
        }

        // เปลี่ยนรูปร่างสุดท้ายก่อนบอสหายไป
        if (deathShape != null)
        {
            spriteRenderer.sprite = deathShape;
        }

        // รอ 1 วินาที
        yield return new WaitForSeconds(1f);

        // ลบบอสออกจากเกม
        Destroy(gameObject);
    }
}