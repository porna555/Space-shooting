using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Met : MonoBehaviour
{
    public float rotationSpeed = 100f; // ความเร็วในการหมุน

    private void Update()
    {
        // หมุนก้อนหินทุก ๆ เฟรม
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime); // หมุนรอบแกน Z
    }
}
