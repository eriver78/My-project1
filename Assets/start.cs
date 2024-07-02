using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class start : MonoBehaviour
{
    
    void Start()
    {
        if (PlayerPrefs.GetInt("mode") == 0)
        NetworkManager.Singleton.StartClient();
        else NetworkManager.Singleton.StartHost();
    }

    


}
