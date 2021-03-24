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

    //When the ball hit the blcok it will set the score and destroy the game object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<BallHandler>())
        {
            gm.SetScore();
            Destroy(gameObject);
        }
    }
}
