using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField]
    string scenename;

    private void OnTriggerEnter2D(Collider2D fin)
    {
        if (fin.tag == "Player")
        {
            SceneManager.LoadScene(scenename);
        }
    }
}
