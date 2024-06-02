using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public int damageAmount = -1;

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "Player") {
            PlayerController controller = collider.gameObject.GetComponent<PlayerController>();
            if (controller != null) {
                controller.ChangeHealthWithCoolDown(damageAmount);
            }
        }
    }

}
