using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Launch(Vector2 direction, float force) {
        rb.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D collision) {
            Debug.Log("Projectile collision with " + collision.gameObject);
            Destroy(gameObject);
    }

}
