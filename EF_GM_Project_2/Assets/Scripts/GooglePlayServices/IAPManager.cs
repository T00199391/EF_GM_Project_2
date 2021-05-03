using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string removeAds = "com.t00199391.blockbreaker.removeads";
    private GameManager gm;
    public GameObject restoreButton;

    void Awake()
    {
        if(Application.platform != RuntimePlatform.IPhonePlayer)
        {
            restoreButton.SetActive(false);
        }
    }

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == removeAds)
        {
            gm.SetNoAds(true);
            Debug.Log("All ads removed");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(product.definition.id + " failed because " + failureReason);
    }
}
