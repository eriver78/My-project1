using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartHost()
    {
        PlayerPrefs.SetInt("mode",1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("game1",LoadSceneMode.Single);  
    }

    public void StartClient()
    {
        PlayerPrefs.SetInt("mode", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("game1", LoadSceneMode.Single);
    }
}
