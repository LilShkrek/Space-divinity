using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    // Ссылка на объект игрока
    public CharacterController characterController;

    // Скорость движения
    public float speed = 12f;

    // Значение g
    public float gravity = -0.8f;

    // Скорость
    Vector2 velocity;

    // Передвижение
    Vector2 move;

    public Transform GroundCheck;

    // Расстояние до земли, на котором триггерится невидимый квадрат
    public Vector2 groundDistance = new Vector2(0.51f, 0.005f);

    // Маска для распознавания земли
    public LayerMask groundMask;

    // Проверка того, на земле ли персонаж
    private bool isGrounded;

    // Переменная силы прыжка
    public float jumpForce = 500f;

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


        Debug.Log(isGrounded);

        // ГРАВИТАЦИЯ

        if (isGrounded & velocity.y <= 0) {

            velocity.y = 0f;

        } else {

            // Изменение координаты Y в векторе velocity
            velocity.y += gravity + Time.deltaTime;

        }

        // Движение в падении
        characterController.Move(velocity * Time.deltaTime / 2);

        // Проверка нахождения игрока на земле
        //isGrounded = Physics.CheckBox(GroundCheck.position, groundDistance, Quaternion.identity, groundMask);
        


    }


    private void OnDrawGizmos() {
        // groundDistance * 2, потому что в Physics.CheckBox задаётся половины длины стороны,
        // а тут вся сторона
        Gizmos.DrawCube(GroundCheck.position, groundDistance * 2);

    }

}
