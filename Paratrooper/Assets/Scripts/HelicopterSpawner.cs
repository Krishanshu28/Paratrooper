using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSpawner : MonoBehaviour
{
    public GameObject helicopterPrefab; 
    public float spawnRate = 5f; 
    public float helicopterSpeed = 3f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnHelicopter), 0f, spawnRate);
    }

    void SpawnHelicopter()
    {
        
        GameObject helicopter = Instantiate(helicopterPrefab, transform.position, transform.rotation);

        
        Rigidbody2D rb = helicopter.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * helicopterSpeed; 
    }
}
