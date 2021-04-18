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
        transform.position -= new Vector3(0, 1f, 0) * Time.deltaTime;
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
