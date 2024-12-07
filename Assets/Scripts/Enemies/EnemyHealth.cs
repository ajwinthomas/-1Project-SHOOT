using UnityEngine;
using UnityEngine.UI;


public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    private float currentHealth;
    void Start()
    {

        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth;

        if(currentHealth <= 0 )
        {
            Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Die()
    {
        Destroy(transform.parent.gameObject);
    }
}
