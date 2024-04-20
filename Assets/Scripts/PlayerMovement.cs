using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    // Ссылка на объект игрока
    public CharacterController characterController;

    // Скорость движения
    public float speed = 12f;

    // Значение g
    public float gravity = -0.3f;

    // Скорость
    Vector2 velocity;

    // Передвижение
    Vector2 move;

    public Transform GroundCheck;

    // Расстояние до земли, на котором триггерится невидимый квадрат
    public Vector3 groundDistance = new Vector3(0.51f, 0.005f, 0.1f);

    // Маска для распознавания земли
    public LayerMask groundMask;

    // Проверка того, на земле ли персонаж
    public static bool isGrounded;

    // Переменная силы прыжка
    public float jumpForce = 1500f;

    // Полоска ХП
    public Image healthBar;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {


        // ДВИЖЕНИЕ

        // Распознавание нажатия AD
        float x = Input.GetAxis("Horizontal");

        move = transform.right * x;

        characterController.Move(move * speed * Time.deltaTime);


        //Debug.Log(isGrounded);

        // ГРАВИТАЦИЯ

        if (isGrounded && velocity.y <= 0) {

            velocity.y = 0f;

        } else {

            // Изменение координаты Y в векторе velocity
            velocity.y += gravity + Time.deltaTime;

        }

        // Движение в падении
        characterController.Move(velocity * Time.deltaTime / 2);

        // Проверка нахождения игрока на земле
        isGrounded = Physics.CheckBox(GroundCheck.position, groundDistance, Quaternion.identity, groundMask);
        

        // ПРЫЖОК
        // При нажатии Space по умолчанию, СС подпрыгивает
        if (Input.GetButtonDown("Jump") && isGrounded) {

            velocity.y = MathF.Sqrt(jumpForce * -2f * gravity);
            isGrounded = false;

        }


        // Отзеркаливание модельки персонажа
        if (Input.GetKeyDown(KeyCode.A)) {
            characterController.transform.localScale = new Vector3(-1, 1, 1);
        } 
        if (Input.GetKeyDown(KeyCode.D)) {
            characterController.transform.localScale = new Vector3(1, 1, 1);
        }

    }


    private void OnDrawGizmos() {
        // groundDistance * 2, потому что в Physics.CheckBox задаётся половины длины стороны,
        // а тут вся сторона
        Gizmos.DrawCube(GroundCheck.position, groundDistance * 2);

    }

    private void OnTriggerEnter (Collider other) {
        if(other.tag == "Spike") {
            healthBar.fillAmount -= 0.05f;
        }
    }

}
