using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float minDmg = 5f;
    public float maxDmg = 10f;
    public float speed = 2f;
    public Image healthBar;

    private Transform target;
    private float timeBefore = 1.0f, curTime;
    private float initialHealth = 10f;
    private float health;

    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Awake()
    {
        curTime = timeBefore;
        health = initialHealth;
    }

    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (health <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player" && curTime <= 0)
        {
            col.gameObject.GetComponent<PlayerMovement>().health -= Random.Range(minDmg, maxDmg);
            curTime = timeBefore;
        }
    }

    void OnParticleCollision(GameObject col)
    {
        if(col.tag == "Player")
        {
            health -= Random.Range(0.5f, 1.5f);
            healthBar.fillAmount = health / initialHealth;
        }
          
    }
}
