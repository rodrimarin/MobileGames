using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopUpTrigger : MonoBehaviour
{


    public GameObject UIObject;
    public void Start()
    {
        UIObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        UIObject.SetActive(true);
    }


    public void OnTriggerExit(Collider other)
    {
        UIObject.SetActive(false);
    }

}
