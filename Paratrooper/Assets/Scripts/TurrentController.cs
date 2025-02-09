using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentController : MonoBehaviour
{
    
    public GameObject bulletPrefab; 
    public Transform firePoint;     
    public float bulletSpeed = 10f;  

    private Vector3 currentRotation;
    public float sensitivity = 2f;
    bool checkVisibleCursor = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        RotateTurretWithMouse();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
            checkVisibleCursor = true;
        }

        // Fire bullet on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            if (checkVisibleCursor)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Confined;
            }
            Shoot();

        }
    }

    void RotateTurretWithMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.z -= mouseX;
        currentRotation.z = Mathf.Clamp(currentRotation.z, -90f, 90f);
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation.z);
    }

    void Shoot()
    {
      
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);

        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;  
        Destroy(bullet,5f);
    }
    


    


}
