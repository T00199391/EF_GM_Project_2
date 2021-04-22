using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerHandler : MonoBehaviour
{
    private GameManager gm;
    private GameObject barrel;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        barrel = gameObject.transform.GetChild(0).gameObject;
        InvokeRepeating("InstantiateProjectile", 0f, 0.5f);
    }

    private void InstantiateProjectile()
    {
        Object prefab = Resources.Load("Prefabs/PowerUps/Projectile");
        Instantiate(prefab, barrel.transform.position, Quaternion.identity);
    }

    public void DestroyComponment()
    {
        Destroy(this);
    }
}
