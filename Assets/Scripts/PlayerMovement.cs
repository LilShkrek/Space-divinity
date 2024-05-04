using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private LayerMask groundLayerMask;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;

    [SerializeField] private float moveSpeed = 20f;
    [SerializeField] private float jumpVelocity = 50f;

    // Полоска ХП
    public Image healthBar;

    // Дес скрин
    public GameObject panelDeathScreen;

    private void Awake() {
        rigidbody2D = transform.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {
            rigidbody2D.velocity = Vector2.up * jumpVelocity;
        }

    }

    private void FixedUpdate() {

        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        if (Input.GetKey(KeyCode.A)) {
            rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
        } else {

            if (Input.GetKey(KeyCode.D)) {
                rigidbody2D.velocity = new Vector2(+moveSpeed, rigidbody2D.velocity.y);
            } else {
                // Не нажата ни одна клавиша
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                rigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            }

        }

    }

    private bool IsGrounded() {

        float extraHeightText = .02f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, groundLayerMask);

        Color rayColor;
        if (raycastHit.collider != null) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2D.bounds.extents.x * 2f), rayColor);

        //Debug.Log(raycastHit.collider);

        return raycastHit.collider != null;

    }
    

    private void OnTriggerEnter2D (Collider2D other) {
        Debug.Log("HIT");
        if (other.tag == "Spike") {
            healthBar.fillAmount -= 0.05f;
        }

        if (healthBar.fillAmount <= 0) {
            panelDeathScreen.SetActive(true);
        }

    }

}
