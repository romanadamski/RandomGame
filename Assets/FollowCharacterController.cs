using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacterController : MonoBehaviour {

    public GameObject Player;
    private Vector3 Offset;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            Offset = transform.position - Player.transform.position;
        }
    }

    void LateUpdate()
    {
        if (Player != null)
        {

        }
        transform.position = Player.transform.position + Offset;
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
    }
}
