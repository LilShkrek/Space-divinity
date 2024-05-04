using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour {

    Animator anim;
    Rigidbody2D rigidbody2D;

    private void Awake() {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) 
        && rigidbody2D.velocity.y == 0) {

            anim.SetTrigger("MoveTrigger");

        }

        if (rigidbody2D.velocity.y != 0) {

            anim.SetTrigger("FallTrigger");

        }

        if (Input.GetKey(KeyCode.A)) {
            rigidbody2D.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey(KeyCode.D)) {
            rigidbody2D.transform.localScale = new Vector3(+1, 1, 1);
        }
        
    }
}
