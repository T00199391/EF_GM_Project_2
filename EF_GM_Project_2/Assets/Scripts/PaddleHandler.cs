using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleHandler : MonoBehaviour
{
    private GameManager gm;

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
    }

    //Will increase the paddle size when the user finishes the reward ad
    public void Reward()
    {
        transform.localScale = new Vector3(0.14f, 0.03f, 1);
    }
}
