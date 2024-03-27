using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    public int damage = 1;
    public void ReactToHit()
    {
        // Check for MeleeEnemyAI component
        MeleeEnemyAI meleeBehavior = GetComponent<MeleeEnemyAI>();
        if (meleeBehavior != null)
        {
            meleeBehavior.TakeDamage(damage);
            if (!meleeBehavior.IsAlive()) // Check if the enemy is alive
            {
                StartCoroutine(Die());
            }
        }

        // Check for WanderingAI component
        WanderingAI rangedBehavior = GetComponent<WanderingAI>();
        if (rangedBehavior != null)
        {
            rangedBehavior.TakeDamage(damage);
            if (!rangedBehavior.IsAlive()) // Check if the enemy is alive
            {
                StartCoroutine(Die());
            }
        }
    }

    public IEnumerator Die()
    {
        transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.5f);

        // Decrease the enemy count in SceneController before destroying the game object
        SceneController sceneController = FindObjectOfType<SceneController>();
        if (sceneController != null)
        {
            sceneController.DecreaseEnemyCount();
        }

        Destroy(this.gameObject);
    }

}
