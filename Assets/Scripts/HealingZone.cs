using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour {

    public int healAmount = 1;

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            PlayerController controller = collider.gameObject.GetComponent<PlayerController>();
            if (controller != null) {
                controller.ChangeHealthWithCoolDown(healAmount);
            }
        }
    } 
}