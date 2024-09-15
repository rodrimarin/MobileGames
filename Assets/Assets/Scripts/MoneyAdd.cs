using UnityEngine;

public class MoneyAdd : MonoBehaviour
{
    public GameObject objc;
    public void OnTriggerEnter(Collider other)
    {

        SaveManager.instance.money += 1000;

        SaveManager.instance.Save();
            objc.SetActive(false);
    }

   
}