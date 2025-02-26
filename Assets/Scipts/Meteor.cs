using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject rockPrefab;  // ก้อนหินที่เราใช้ในการสร้าง
    public Vector2 spawnAreaMin;   // ขอบล่างซ้ายของพื้นที่ (ต่ำสุด)
    public Vector2 spawnAreaMax;   // ขอบบนขวาของพื้นที่ (สูงสุด)
    public int totalRocks = 100;   // จำนวนก้อนหินที่ต้องการสร้าง
    public float spawnDelay = 0.1f; // เวลาหน่วงระหว่างการสปอนแต่ละก้อนหิน

    private int currentRockCount = 0;  // ตัวนับจำนวนก้อนหินที่ถูกสร้าง

    private void Start()
    {
        InvokeRepeating("SpawnRock", 0f, spawnDelay);  // เรียกใช้ฟังก์ชัน SpawnRock ทุกๆ spawnDelay วินาที
    }

    void SpawnRock()
    {
        // ตรวจสอบจำนวนก้อนหินที่ถูกสร้างแล้ว
        if (currentRockCount >= totalRocks)
        {
            // หยุดการสร้างก้อนหินเมื่อถึงจำนวนที่กำหนด
            CancelInvoke("SpawnRock");
            return;
        }

        // สุ่มตำแหน่งภายในพื้นที่ 4 เหลี่ยม
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // สุ่มขนาดของก้อนหินระหว่าง 0.5 ถึง 1.5
        float randomScale = Random.Range(0.5f, 1.5f);

        // สร้างก้อนหินใหม่ที่ตำแหน่งที่สุ่มได้
        GameObject newRock = Instantiate(rockPrefab, spawnPosition, Quaternion.identity);

        // เปลี่ยนขนาดของก้อนหินตามค่าที่สุ่มได้
        newRock.transform.localScale = new Vector3(randomScale, randomScale, 1f); // ถ้าเป็น 2D ค่าของ z จะเป็น 1

        // เพิ่มจำนวนก้อนหินที่ถูกสร้าง
        currentRockCount++;

        // ทำให้ก้อนหินหมุน
        Rotate_Met rockRotation = newRock.AddComponent<Rotate_Met>();
        rockRotation.rotationSpeed = Random.Range(50f, 150f);  // กำหนดความเร็วในการหมุนแบบสุ่ม
    }
}