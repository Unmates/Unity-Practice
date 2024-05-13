using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    Score scorescript;

    private void OnTriggerEnter2D(Collider2D coin)
    {
        //when coin get coin gone
        if (coin.tag == "Player")
        {
            scorescript.addScore();
            Destroy(this.gameObject);
        }
    }
}
