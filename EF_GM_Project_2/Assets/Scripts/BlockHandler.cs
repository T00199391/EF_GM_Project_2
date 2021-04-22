using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHandler : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    //When the ball/projectile hit the block it will set the score and destroy the game object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BallHandler>())
        {
            int chance = Random.Range(0, 101);
            if (chance <= 20)
            {
                Object prefab = Resources.Load("Prefabs/PowerUps/PowerUp");
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gm.SetScore("Ball");
            Destroy(gameObject);
        }
    }
}
