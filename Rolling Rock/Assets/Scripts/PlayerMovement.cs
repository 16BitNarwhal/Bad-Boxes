using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float accel = 5f;
    public float decel = 5f;
    public GameObject atkParticle;
    public float atkDelay = 1f;
    public float health = 100f;
    public Image playerHealthBar;
    public GameObject gameOverScreen;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private Vector2 input;
    private Animator anim;
    private float atkTimeLeft = 0f;

    void Start()
    {
        velocity = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMovement();
        HandleTurning();
        HandleAttack();

        playerHealthBar.fillAmount = health / 100f;
        // Debug.Log(health);
        if(health <= 0)
        {
            GameOver();
        }
    }

    private void HandleMovement()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        if (input.x != 0 || input.y != 0)
            anim.SetBool("IsWalking", true);
        else
            anim.SetBool("IsWalking", false);
        /*
        if (input.x < 0 && velocity.x > -maxSpeed)
            velocity.x -= accel * Time.deltaTime;
        else if (input.x > 0 && velocity.x < maxSpeed)
            velocity.x += accel * Time.deltaTime;
        else if (input.x == 0)
        {
            if (velocity.x > decel * Time.deltaTime)
                velocity.x -= decel * Time.deltaTime;
            else if (velocity.x < -decel * Time.deltaTime)
                velocity.x += decel * Time.deltaTime;
            else
                velocity.x = 0;
        }
        */
        if (input.y > 0 && velocity.y > -maxSpeed)
            velocity.y -= accel * Time.deltaTime;
        else if (input.y < 0 && velocity.y < maxSpeed)
            velocity.y += accel * Time.deltaTime;
        else if (input.y == 0)
        {
            if (velocity.y > decel * Time.deltaTime)
                velocity.y -= decel * Time.deltaTime;
            else if (velocity.y < -decel * Time.deltaTime)
                velocity.y += decel * Time.deltaTime;
            else
                velocity.y = 0;
        }
        Vector3 movement = transform.rotation * Vector2.up;
        rb.velocity = movement * velocity.y;
    }

    private void HandleTurning()
    {
        /*
        float angle;
        if (velocity.x < 0)
            angle = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg + 180;
        else if (velocity.x > 0)
            angle = Mathf.Atan(velocity.y / velocity.x) * Mathf.Rad2Deg;
        else if (velocity.y > 0)
            angle = 90;
        else if (velocity.y < 0)
            angle = 270;
        else
            return;
        */
        float angle;
        angle = rb.rotation - input.x * 5;
        angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, angle, 570 * Time.deltaTime);
        rb.SetRotation(angle);
    }

    private void HandleAttack()
    {
        atkTimeLeft -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && atkTimeLeft <= 0f)
        {
            Instantiate(atkParticle, transform.position, transform.rotation);
            atkTimeLeft = atkDelay;
        }
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(true);
    }
}

