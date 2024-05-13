using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if player bottom no collider
        if (Player.transform.position.y < transform.position.y)
        {
            collider.enabled = false;
        }

        //if player top yes collider
        if (Player.transform.position.y > transform.position.y)
        {
            collider.enabled = true;
        }

        //if press down no collider
        if (Input.GetAxis("Vertical") < 0)
        {
            collider.enabled = false;
        }
    }
}
