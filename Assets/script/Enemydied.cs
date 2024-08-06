﻿using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject player; // Đối tượng người chơi
    public GameObject enemyPrefab; // Prefab của quái vật mới
    public float spawnRadius = 5f; // Bán kính sinh ra quái vật mới
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu đối tượng va chạm là người chơi
        if (other.gameObject == player)
        {
            // Xử lý khi người chơi va chạm với kẻ địch
            Die();
        }
    }

    private void Die()
    {
        // Hủy đối tượng kẻ địch
        Destroy(gameObject);
    }
    private void SpawnNewEnemy()
    {
        // Tính toán vị trí sinh ra ngẫu nhiên quanh kẻ địch
        Vector3 spawnPosition = transform.position + (Vector3)Random.insideUnitCircle * spawnRadius;

        // Sinh ra quái vật mới
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
