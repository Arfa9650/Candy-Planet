using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{
    public bool canEndGame = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!canEndGame)
        {
            Debug.Log("can end now");
            canEndGame = true;
        }
    }
}
