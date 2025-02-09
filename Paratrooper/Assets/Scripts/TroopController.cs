using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TroopController : MonoBehaviour
{
    public float moveSpeed = 2f;
    private bool hasLanded = false;
    private Transform player;
    bool stop = false;


    private bool isStacked = false;

    SpriteRenderer sprite;
    Rigidbody2D rb;

    [SerializeField]
    Sprite parachute;
    Sprite og;

    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeSprite());
        og = sprite.sprite;

        
    }

    void Update()
    {
        if (hasLanded )
        {
            MoveTowardsTurret();
        }
    }

    IEnumerator ChangeSprite()
    {
        float rand = Random.Range(0.5f, 0.75f);
        yield return new WaitForSeconds(rand);
        sprite.sprite = parachute;
        rb.drag = 5f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            sprite.sprite = og;
            hasLanded = true;
        }
        
        else if (collision.gameObject.CompareTag("Player") && !isStacked)
        {
            hasLanded = false;
            isStacked = true;

            
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;

    
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            stop = true;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("Destroy");
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            hasLanded = true;
        }
        else if (collision.gameObject.CompareTag("Player") && !isStacked)
        {
            isStacked = true;
            

        }
    }
    float distance;
    void MoveTowardsTurret()
    {
        distance = Vector2.Distance(transform.position, player.position);

      
        if (distance > 0.5f && !stop) 
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
           StartCoroutine(ClimbStack());
           stop = false;
        }
    }


    IEnumerator ClimbStack()
    {
        yield return new WaitForSeconds(0.5f); 
        
        Collider2D[] nearbyTroops = Physics2D.OverlapCircleAll(transform.position, 1f);
        int count = 0;
        
        foreach (Collider2D troop in nearbyTroops)
        {
            if (troop.CompareTag("Enemy"))
            {
                count++;
            }
        }
        
        print(count);

        if (count >= 2) 
        {
            Vector3 newPosition = transform.position + new Vector3(0,0.82f, 0);
            transform.position = newPosition;
            Time.timeScale = 0;
            print("Game Over");
        }

    }
}
