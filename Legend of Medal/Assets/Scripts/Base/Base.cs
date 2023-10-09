using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;
    public Scrollbar healthBar; 
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth == 0)
        {
            Debug.Log("Dead");
            // Add death logic here
        }
    }

    void UpdateHealthBar()
    {
        healthBar.size = (float)currentHealth / maxHealth;
    }
}
