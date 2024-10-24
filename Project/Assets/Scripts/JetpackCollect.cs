using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackCollect : MonoBehaviour
{
        public PlayerController PlayerController;
        public GameObject JetpackUI;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerController.JetpackAcquired = true;
                JetpackUI.SetActive(true);
                Destroy(gameObject);
            }
        }
}
