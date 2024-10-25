using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangCollection : MonoBehaviour
{
    public PlayerController PlayerController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.BoomerangAcquired = true;
        }
    }
}
