using UnityEngine;

public class DummyEnemy : MonoBehaviour
{
    public int health = 100;
    private int maxHealth;

    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        maxHealth = health;
        if (healthBar != null)
        {
            healthBar.SetHealth(1f);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{name} took {amount} damage. Remaining HP : {health}");

        if (healthBar != null)
        {
            float percent = Mathf.Clamp01((float)health / maxHealth);
            healthBar.SetHealth(percent);
        }

        if ( health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
