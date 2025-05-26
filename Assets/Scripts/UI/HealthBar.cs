using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int currentHealth;

    public int MaxHealth { get { return health; } }
    public int CurrentHealth { get { return currentHealth; } }

    [SerializeField] private int healthIncrement = 10;
    [SerializeField] private int healthDecreace = 10;

    private void Start()
    {
        currentHealth = health;
    }

    public void IncreaseHealth()
    {
        if(currentHealth >= MaxHealth)
        {
            return;
        }
        currentHealth += healthIncrement;
        GameManager.Instance.InvokeOnHealthChange();
    }

    public void DecreaseHealth()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance.LoadScene("StartMenu");
            return;
        }
        currentHealth -= healthDecreace;
        GameManager.Instance.InvokeOnHealthChange();

    }

}
