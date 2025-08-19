using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressurePlate : MonoBehaviour
{
    int loadDelay = 3;
    int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PP"))
        {
            StartCoroutine(LoadNextLevel());
        }

    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(currentSceneIndex+1);
    }
}
