using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    Animator animator;
    Vector2 moveDirection = new Vector2(1, 0);
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
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    { 
        if(!Mathf.Approximately(playerInput.x, 0.0f) || !Mathf.Approximately(playerInput.y,0.0f))
        {
           moveDirection.Set(playerInput.x, playerInput.y);
           moveDirection.Normalize();
        }

        animator.SetFloat("Look X", moveDirection.x);
        animator.SetFloat("Look Y", moveDirection.y);
        animator.SetFloat("Speed", playerInput.magnitude);

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
        if(amount < 0) {
            animator.SetTrigger("Hit");
        }
        if(currentHealth == 0) {
            Destroy(gameObject);
        }
    }
}
