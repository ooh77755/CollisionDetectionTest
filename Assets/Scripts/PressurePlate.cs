using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlate : MonoBehaviour
{
    float loadDelay = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PP"))
        {
            Invoke("LoadNextLevel", loadDelay);
        }

    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
}
