using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public AudioClip damageSound;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private int health;
    public int maxHealth = 4;
    public static event Action<int> OnHealthChanged = delegate { };

    // Start is called before the first frame update
    void Start()
    {
        health = 2;
        audioSource = GetComponent<AudioSource>();
    }

    public void Hurt(int damage)
    {

        health -= damage;
        OnHealthChanged(health);
        if (audioSource && damageSound) // Play the damage sound
        {
            audioSource.PlayOneShot(damageSound);
        }
        if (health <= 0 && audioSource && deathSound) // Play the death sound
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) // Prevent overhealing
        {
            health = maxHealth;
        }
        OnHealthChanged(health);
    }

    public int GetHealth()
    {
        return health;
    }
}
