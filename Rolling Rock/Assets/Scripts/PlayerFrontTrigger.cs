using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrontTrigger : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject box = col.gameObject;
        if(box.layer == 8)
        {
            box.GetComponent<BoxController>().state = 4;
        }
    }
}
