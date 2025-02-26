using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    public string playerTag = "Player"; // Tag ของผู้เล่นที่ศัตรูจะไล่ล่า
    public float moveSpeed = 3f; // ความเร็วในการเคลื่อนที่
    public float stoppingDistance = 1f; // ระยะห่างที่ศัตรูจะหยุดเมื่อใกล้ผู้เล่น

    private Transform target; // เป้าหมายที่ศัตรูจะไล่ล่า (ผู้เล่น)

    private void Start()
    {
        // ค้นหาผู้เล่นด้วย Tag "Player"
        GameObject player = GameObject.FindWithTag(playerTag);
        if (player != null)
        {
            target = player.transform; // กำหนดให้ target เป็น Transform ของผู้เล่น
        }
    }

    private void Update()
    {
        // ตรวจสอบว่ามี target (ผู้เล่น) หรือไม่
        if (target != null)
        {
            MoveTowardsPlayer(); // ให้ศัตรูเคลื่อนที่ไปหาผู้เล่น
        }
    }

    // ฟังก์ชันเคลื่อนที่ไปหาผู้เล่น
    void MoveTowardsPlayer()
    {
        // คำนวณทิศทางจากศัตรูไปหาผู้เล่น
        Vector2 direction = (target.position - transform.position).normalized;

        // ตรวจสอบระยะห่างจากผู้เล่น
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            // เคลื่อนที่ไปยังทิศทางที่คำนวณ
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}