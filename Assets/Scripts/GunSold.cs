using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSold : MonoBehaviour
{
    public GameObject gun;
    public void OnTriggerEnter(Collider other)
    {
        
        if (ScoreManager.scoreCount >= 500)
        {
            

            ScoreManager.scoreCount = 0;
            gun.SetActive(true);

        }
       
        else
        {
            Debug.Log("You dont have enought money");
        } 
           
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
