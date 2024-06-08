using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    bool aggressive = true;
    Animator animator;
    public float moveSpeed = 2.0f;
    Rigidbody2D rb;
    public bool vertical;
    public float flipDirectionMaxTime = 2.0f;
    float flipDirectionCurrTime;
    int direction = 1;
    public int damageAmountOnCollision = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            flipDirectionCurrTime -= Time.deltaTime;
            if(flipDirectionCurrTime < 0) {
                direction = -direction;
                flipDirectionCurrTime = flipDirectionMaxTime;
                vertical = Random.value > 0.5f;
            }
    }

    // FixedUpdate has the same call rate as the physics system
    void FixedUpdate() {
        
        if(!aggressive) {
            return;
        }
        
        Vector2 position = rb.position;

        if(vertical) {
            position.y = position.y + moveSpeed * direction * Time.deltaTime;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        } else {
            position.x = position.x + moveSpeed * direction * Time.deltaTime;
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }        
        rb.MovePosition(position);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            PlayerController controller = collider.gameObject.GetComponent<PlayerController>();
            if(controller != null) {
                Debug.Log("collided");
                controller.ChangeHealth(-damageAmountOnCollision);
            }
        }
    }

    public void Fix() {
        aggressive = false;
        rb.simulated = false;
        animator.SetTrigger("Fixed");
    }
}
