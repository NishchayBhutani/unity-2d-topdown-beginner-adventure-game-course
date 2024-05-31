using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    private Vector2 playerInput;
    public float moveSpeed = 5.0f;
    private Rigidbody2D rb;
    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;
    public float cooldownTime = 2.0f;
    bool isInCoolDown;
    float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    { 
        if(isInCoolDown) {
            currentCooldown -= Time.deltaTime;
            if(currentCooldown < 0) {
                isInCoolDown = false;
                Debug.Log("No Longer in CoolDown");
            }
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + playerInput * moveSpeed * Time.deltaTime);
    }

    void OnWalk(InputValue inputValue) {
        playerInput = inputValue.Get<Vector2>();
    }

    public void ChangeHealthWithCoolDown(int amount) {
        if(isInCoolDown) {
            return;
        }
        isInCoolDown = true;
        currentCooldown = cooldownTime;
        ChangeHealth(amount);
    }

    public void ChangeHealth(int amount) {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHandler.instance.setHealthValue(currentHealth / (float)maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
