using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour, IDamagable
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;

    public UnityEvent onTakeDamage;

    private void Start()
    {
        health.currentValue = health.startValue;
        hunger.currentValue = hunger.startValue;
        thirst.currentValue = thirst.startValue;
        sleep.currentValue = sleep.startValue;
    }

    private void Update()
    {
        //decay needs over time
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);
        sleep.Subtract(sleep.decayRate * Time.deltaTime);

        if (hunger.currentValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }
        if (thirst.currentValue == 0.0f)
        {
            health.Subtract(noThirstHealthDecay * Time.deltaTime);
        }

        //updating UI bards
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        thirst.uiBar.fillAmount = thirst.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();

        if (health.currentValue == 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Drinkg(float amount)
    {
        thirst.Add(amount);
    }

    public void Sleep(float amount)
    {
        sleep.Add(amount);
    }

    public void TakePhysicaldamage(int amount)
    {
        health.Subtract(amount);
        onTakeDamage?.Invoke(); // Events. "?" oznacza, ze jesli ine mamy nic przypisanego do onTakeDamage to daje nam error (jesli jest null)
    }

    public void Die()
    {
        Debug.Log("Died");
    }

}

[System.Serializable]
public class Need
{
    public float currentValue;
    public float maxValue;
    public float startValue;
    public float regenRaet;
    public float decayRate;
    public Image uiBar;

    public void Add (float amount)
    {
        currentValue = Mathf.Min(currentValue + amount, maxValue); //hp never bigger than maxValue
    }

    public void Subtract (float amount)
    {
        currentValue = Mathf.Max(currentValue - amount, 0,0f); //hp never less than 0
    }

    //return percentage value 0.0 - 1.0
    public float GetPercentage()
    {
        return currentValue / maxValue;
    }
}

public interface IDamagable
{
    void TakePhysicaldamage(int damageAmount);
}
