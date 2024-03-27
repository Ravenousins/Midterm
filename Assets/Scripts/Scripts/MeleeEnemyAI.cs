using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2; 
    public int health = 3; 
    public int damage = 1;

    private Transform player;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SetAlive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            HandleMovement();
        }

    }


    private void HandleMovement()
    {
        // Move towards the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If the enemy collides with the player, deal damage
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerCharacter player = other.gameObject.GetComponent<PlayerCharacter>();
            if (player != null)
            {
                player.Hurt(damage);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }



    public bool IsAlive()
    {
        return health > 0;
    }
}