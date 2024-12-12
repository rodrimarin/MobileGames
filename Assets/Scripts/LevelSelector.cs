using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update


    public TextMeshProUGUI TextLevel;

    public int Level;
    
    void Start()
    {
        TextLevel.text = Level.ToString();
    }


    public void LoadLevelScene()
    {
        SceneManager.LoadScene("Level " + Level.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
