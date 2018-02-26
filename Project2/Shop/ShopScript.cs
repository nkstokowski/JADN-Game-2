using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

    public GameObject shopCanvas;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("triggered by player");
            OpenShop();
        }
    }

    void OpenShop()
    {
        shopCanvas.SetActive (true);
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shopCanvas.SetActive (false);
        Time.timeScale = 1;
    }
}
