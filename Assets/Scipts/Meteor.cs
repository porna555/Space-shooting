using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20f; // ความเร็วในการหมุนของก้อนหิน
    [SerializeField] private GameObject obstaclePrefab; // พรีแฟบของสิ่งกีดขวาง
    [SerializeField] private int obstacleCount = 30; // จำนวนสิ่งกีดขวาง
    [SerializeField] private Vector2 spawnArea = new Vector2(10f, 10f); // ขอบเขตการเกิดของสิ่งกีดขวาง
    [SerializeField] private float minimumDistance = 2f; // ระยะห่างขั้นต่ำระหว่างสิ่งกีดขวาง

    private List<Vector2> spawnedPositions = new List<Vector2>(); // เก็บตำแหน่งที่สิ่งกีดขวางถูกสร้างขึ้น

    void Start()
    {
        SpawnObstacles();
    }

    void Update()
    {
        // ทำให้ก้อนหินหมุน
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();

            // สร้างสิ่งกีดขวางที่ตำแหน่งที่สุ่มได้
            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            spawnedPositions.Add(spawnPosition); // เพิ่มตำแหน่งที่สร้างไว้ในลิสต์
        }
    }

    // ฟังก์ชันสุ่มตำแหน่งที่ไม่มีการชน
    Vector2 GetRandomSpawnPosition()
    {
        Vector2 spawnPosition;
        bool positionFound = false;

        do
        {
            spawnPosition = new Vector2(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            positionFound = true;

            // ตรวจสอบระยะห่างจากสิ่งกีดขวางที่ถูกสร้างแล้ว
            foreach (var pos in spawnedPositions)
            {
                if (Vector2.Distance(spawnPosition, pos) < minimumDistance)
                {
                    positionFound = false;
                    break;
                }
            }

        } while (!positionFound); // สุ่มใหม่จนกว่าจะเจอตำแหน่งที่ไม่ชน

        return spawnPosition;
    }
}
