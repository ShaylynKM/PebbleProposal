using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
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
        if (collision.CompareTag("Player"))
        {
            playerController.TakeDamage();
            Debug.Log("be careful bruh");
        }
    }
}
