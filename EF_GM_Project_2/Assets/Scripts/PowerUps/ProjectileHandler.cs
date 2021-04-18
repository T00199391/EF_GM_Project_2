using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.position += new Vector3(0, 0.05f, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Top"))
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.GetComponent<BlockHandler>())
        {
            int chance = Random.Range(0, 101);
            if (chance <= 30)
            {
                Object prefab = Resources.Load("Prefabs/PowerUps/PowerUp");
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gm.SetScore("Projectile");
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
