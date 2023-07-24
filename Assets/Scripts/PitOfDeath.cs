using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitOfDeath : MonoBehaviour
{
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("PlayerPenguin").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered");
        if (collision.CompareTag("Player"))
        {
            playerController.Die();
            Debug.Log("you have fallen");
        }
    }
}
