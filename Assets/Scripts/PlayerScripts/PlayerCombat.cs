using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update() {

        if(Input.GetKeyDown(KeyCode.Mouse0)) {
            Attack();
        }
        
    }

    void Attack() {

        // Воспроизведение анимации атаки
        animator.SetTrigger("AttackTrigger");

        // Считывание противников, задетых атакой
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Нанесение урона задетым противникам
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }

    }

    void OnDrawGizmos() {

        if(attackPoint == null) 
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }

}
