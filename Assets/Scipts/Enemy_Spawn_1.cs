using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn_1 : MonoBehaviour
{
    public GameObject enemyPrefab; // ตัวแบบศัตรู (Prefab)
    public int enemyCount = 4; // จำนวนศัตรูที่ต้องการสร้าง
    public float spawnRadius = 5f; // รัศมีที่สามารถเสกศัตรูได้
    private Transform playerTransform; // เป้าหมายของศัตรู (ผู้เล่น)

    private void Start()
    {
        // ค้นหาตัวผู้เล่นในฉาก
        playerTransform = GameObject.FindWithTag("Player").transform;

        // เสกศัตรู
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            // สร้างตำแหน่งสุ่มภายในรัศมีที่กำหนด
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // เสกศัตรูที่ตำแหน่งสุ่ม
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // ตั้งค่า target ของศัตรูเป็นผู้เล่น
            enemy.GetComponent<Enemy_1>().playerTag = "Player";
        }
    }
}
