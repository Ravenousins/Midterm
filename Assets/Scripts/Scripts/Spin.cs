using System.Collections;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spin = 5.0f;
    [SerializeField] private int healAmount = 1; 
    [SerializeField] private float healInterval = 45.0f; // Time interval between heals
    [SerializeField] private AudioClip[] healSounds;

    private Coroutine healCoroutine; // Store the reference to the coroutine
    private float remainingWaitTime = 0f; // Time remaining until the next heal
    private AudioSource audioSource;



    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the healing radius
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            // Start healing the player
            healCoroutine = StartCoroutine(HealPlayer(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player exited the healing radius
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            // Stop healing the player and store the remaining wait time
            StopCoroutine(healCoroutine);
            remainingWaitTime = Mathf.Max(0f, healInterval - Time.timeSinceLevelLoad % healInterval);
        }
    }

    private IEnumerator HealPlayer(PlayerCharacter player)
    {
        // Wait for the remaining wait time before the first heal
        yield return new WaitForSeconds(remainingWaitTime);
        remainingWaitTime = 0f;

        while (true)
        {
            // Only heal the player if they are still in the healing radius
            player.Heal(healAmount);

            if (healSounds.Length > 0 && audioSource != null)
            {
                AudioClip healSound = healSounds[Random.Range(0, healSounds.Length)];
                audioSource.PlayOneShot(healSound);
            }

            yield return new WaitForSeconds(healInterval);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, spin * Time.deltaTime, 0);
    }
}
