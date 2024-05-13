using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallTrigger : MonoBehaviour
{
    [SerializeField]
    Transform Player;
    [SerializeField]
    Lives livesScript;

    private void Update()
    {
        transform.position = new Vector2(Player.position.x, -9.5f);    
    }

    private void OnTriggerEnter2D(Collider2D fall)
    {
        //when fall respawn
        if (fall.tag == "Player")
        {
            livesScript.reduceLives();
        }
    }
}
