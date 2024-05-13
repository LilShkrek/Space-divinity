using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        // Анимация получения урона
        animator.SetTrigger("HurtTrigger");

        // Если противник умер
        if (currentHealth <= 0) {
            Die();
        }

    }

    void Die () {
        Debug.Log("Вмер");

        // Анимация смерти
        animator.SetBool("IsDead", true);

        // Выключение врага
        Destroy(gameObject);
        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

}
