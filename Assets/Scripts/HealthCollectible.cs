using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

    public int healAmount = 1;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            PlayerController controller = collider.GetComponent<PlayerController>();
            
            if(controller != null && controller.health < controller.maxHealth) {
                controller.ChangeHealth(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
