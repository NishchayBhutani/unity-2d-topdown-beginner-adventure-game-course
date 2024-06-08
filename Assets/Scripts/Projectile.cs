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

    void Update() {
        if(transform.position.magnitude > 100.0f) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void Launch(Vector2 direction, float force) {
        rb.AddForce(direction * force);
    }

    void OnTriggerEnter2D(Collider2D collider) {
         Debug.Log("Entered Trigger");
            EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
            if(enemy != null) {
                enemy.Fix();
            }
            Destroy(gameObject);
    }

}
