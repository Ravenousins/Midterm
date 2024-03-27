using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float obstacleRange = 5.0f;
    [SerializeField] private float stopDistance = 7f;
    [SerializeField] private float strafeSpeed = 2f;


    public AudioClip[] fireballSounds;
    private AudioSource audioSource;

    private GameObject fireball;
    private bool isAlive;
    public int health = 3;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isAlive = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            HandleMovement();
            HandleFireball();
        }
    }

    private void HandleMovement()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the enemy is not within the stop distance of the player, move towards the player
        if (distanceToPlayer > stopDistance)
        {
            MoveTowardsPlayer(directionToPlayer);
        }
        // If the enemy is within the stop distance of the player, strafe around the player
        else
        {
            StrafeAroundPlayer(directionToPlayer);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SetAlive(false);
        }
    }

    // Moves the AI towards the player
    private void MoveTowardsPlayer(Vector3 directionToPlayer)
    {

        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);


        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    // Makes the AI strafe around the player
    private void StrafeAroundPlayer(Vector3 directionToPlayer)
    {

        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speed);

  
        Vector3 strafeDirection = transform.right;
        Ray strafeRay = new Ray(transform.position, strafeDirection);
        if (!Physics.Raycast(strafeRay, obstacleRange))
        {
  
            transform.Translate(strafeSpeed * Time.deltaTime, 0, 0);
        }
    }


    private void HandleFireball()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;

            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                    Destroy(fireball, 2.0f);

                    if (audioSource && fireballSounds.Length > 0)
                    {
                        AudioClip fireballSound = fireballSounds[Random.Range(0, fireballSounds.Length)];
                        audioSource.PlayOneShot(fireballSound);
                    }

                }
            }
            else if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
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