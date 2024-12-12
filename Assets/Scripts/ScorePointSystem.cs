using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScorePointSystem : MonoBehaviour
{


    public GameObject objc;
    
    public void OnTriggerEnter(Collider other)
    {


            ScoreManager.scoreCount = +500;
           objc.SetActive(false);
            
        }
        
    }
