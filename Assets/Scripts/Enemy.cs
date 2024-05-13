using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    // Update is called once per frame
    void Update()
    {
        //if player left look left
        if (Player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        //if player right look right
        if (Player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
