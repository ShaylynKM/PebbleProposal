using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    // How long it will take for the platform to fall
    private float _fallingDelay = 1.5f;

    // How long it will take for the platform to be destroyed
    private float _destroyingDelay = 2f;

    // Reference to the platform's rigid body
    [SerializeField]
    private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the object is the player...
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Falling());
        }
    }

    // This coroutine allows the platform to fall and then be destroyed.
    private IEnumerator Falling()
    {
        yield return new WaitForSeconds(_fallingDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, _destroyingDelay);
    }
}