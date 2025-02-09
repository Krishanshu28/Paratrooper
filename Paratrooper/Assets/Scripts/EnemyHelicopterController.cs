using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHelicopterController : MonoBehaviour
{

    [SerializeField]
    Transform dropPos;

    public GameObject troop;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(DeployTroops());
        Destroy(this.gameObject, 10f);
    }


    IEnumerator DeployTroops()
    {
        float rand = Random.Range(4f, 6f);
        yield return new WaitForSeconds(rand);
        Instantiate(troop, dropPos.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            print("Destroy");
            Destroy(this.gameObject);
        }
    }
}
