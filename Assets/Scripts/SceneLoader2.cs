using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader2 : MonoBehaviour
{
    // Start is called before the first frame update

    ///public Text TextLevel;

    public void Start()
    {
        //TextLevel.text = ToString();
    }

    public void LoadScene(int CurrentScene)
    {
        SceneManager.LoadScene(CurrentScene);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
