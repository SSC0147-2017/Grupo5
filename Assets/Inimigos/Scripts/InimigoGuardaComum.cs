using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoGuardaComum : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float maxHealth;
    
    private float pivot;
    private float health;
    private Image filler;
    private GameObject healthBar;
    private Rigidbody2D rigid;
    private Quaternion healthRotation;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        filler = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        healthBar = transform.GetChild(0).GetChild(0).gameObject;
        healthRotation = healthBar.transform.rotation;
        health = maxHealth;
        filler.fillAmount = health / maxHealth;
        pivot = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate() {
        testHealthBar();
        filler.fillAmount = health / maxHealth;
        transform.Translate(Vector3.right * speed);
        if (transform.position.x > maxDistance + pivot || transform.position.x < pivot - maxDistance) {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);            
            healthBar.transform.rotation = healthRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.tag.Contains("Bullet")) {
            if (health > 0)
                health -= 10f;//a definir
        }
    }

    /* test */
    private void testHealthBar() {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            if (health > 0)
                health -= 10f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            if (health < maxHealth)
                health += 10f;
        }
        if (health <= 0)
            GameObject.Destroy(this.gameObject);
    }
}
