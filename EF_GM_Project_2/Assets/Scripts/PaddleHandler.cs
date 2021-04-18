using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleHandler : MonoBehaviour
{
    private GameManager gm;
    public GameObject lazer1, lazer2;
    private float timer = 0;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            //If the user moves their finger on the screen the paddle will move to that position
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

                transform.position = new Vector3(pos.x, transform.position.y, 0);
            }
        }

        if(gm.GetPowerUpActive() && timer <= 5)
        {
            timer += Time.deltaTime;
            if(!lazer1.GetComponent<LazerHandler>())
            {
                lazer1.AddComponent<LazerHandler>();
                lazer2.AddComponent<LazerHandler>();
            }
        }
        else
        {
            if (lazer1.GetComponent<LazerHandler>())
            {
                lazer1.GetComponent<LazerHandler>().DestroyComponment();
                lazer2.GetComponent<LazerHandler>().DestroyComponment();
            }
            timer = 0;
            gm.SetPowerUpActive(false);
        }
    }

    //Will increase the paddle size when the user finishes the reward ad
    public void Reward()
    {
        transform.localScale = new Vector3(0.14f, 0.03f, 1);
    }
}
