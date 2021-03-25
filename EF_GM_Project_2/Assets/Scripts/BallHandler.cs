using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallHandler : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gm;
    private Vector2 vel;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartGame", 3);
        vel = new Vector2(0, 4);
    }

    void Update()
    {
        if (gm.GetCurrentState() == GameManager.GameStates.WON || gm.GetCurrentState() == GameManager.GameStates.OVER)
            rb.velocity = new Vector2(0, 0);
    }

    //Will end the game when the ball hits the bottom of the screen
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bottom"))
        {
            gm.SetGameOver();
        }
    }

    //After 3 seconds the game will call this method to make the ball move
    private void StartGame()
    {
        gm.StartGame();
        rb.velocity = vel;
    }

    public void PauseGame()
    {
        gm.SetPauseGame();

        if (gm.GetCurrentState() == GameManager.GameStates.PAUSED)
        {
            vel = rb.velocity;
            rb.velocity = new Vector2(0,0);
        }
        else
        {
            Invoke("StartGame", 3);
        }
    }
}
