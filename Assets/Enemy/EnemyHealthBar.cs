using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBar : MonoBehaviour
{
    public Animator anim;
    public float maxHealth = 100f;
    public float currentHealth = 100f;
    public GameObject Health;
   public Slider healthSlider;
    private float damageAmount;

    private void Start()
    {
        Health.SetActive(false);
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        anim = GetComponent<Animator>();
    }

    public void UpdateHealth(float newHealth)
    {
        currentHealth = newHealth;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        healthSlider.value = currentHealth;
    }

    public void TakeDamageFromPlayer(float damageAmount)
    {
        Health.SetActive(true);
        anim.SetTrigger("EnemyHit");
        currentHealth -= damageAmount;
        UpdateHealth(currentHealth);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        Health.SetActive(false);
        // Delay for a certain amount of time before destroying the enemy object
        StartCoroutine(DestroyAfterDelay(1.5f)); // Replace 3f with the desired delay in seconds
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Destroy the enemy object
        Destroy(gameObject);
    }

    public void ApplyKnockBack(Vector3 knockBackDirection)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(knockBackDirection, ForceMode.Impulse);
        }
    }
}
