using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpHandler : MonoBehaviour
{
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if(gm.GetCurrentState() == GameManager.GameStates.RUNNING)
            transform.position -= new Vector3(0, 1.3f, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PaddleHandler>())
        {
            if (!gm.GetPowerUpActive())
            {
                gm.SetPowerUpActive(true);
                Destroy(gameObject);
            }
        }
    }
}
