using UnityEngine;

public class DiceController : MonoBehaviour
{
    public Dice dice1; // Reference to the first dice
    public Dice dice2; // Reference to the second dice

    private float lastValueDice1;
    private float lastValueDice2;

    // Special numbers and their corresponding actions
    public float specialNumber1 = 6f; // Example special number
    public float specialNumber2 = 1f; // Another example special number

    void Start()
    {
        // Subscribe to the onLanded events of each dice
        if (dice1 != null)
            dice1.onLanded.AddListener(UpdateDice1Value);

        if (dice2 != null)
            dice2.onLanded.AddListener(UpdateDice2Value);
    }

    private void UpdateDice1Value(float value)
    {
        lastValueDice1 = value;
        Debug.Log("Dice 1 landed on: " + lastValueDice1);
        CheckSpecialNumbers(lastValueDice1, 1); // Check for special numbers
    }

    private void UpdateDice2Value(float value)
    {
        lastValueDice2 = value;
        Debug.Log("Dice 2 landed on: " + lastValueDice2);
        CheckSpecialNumbers(lastValueDice2, 2); // Check for special numbers
    }

    // Method to check for special numbers
    private void CheckSpecialNumbers(float value, int diceIndex)
    {
        if (value == specialNumber1)
        {
            Debug.Log($"Dice {diceIndex} landed on a special number: {value}");
            // Add your special action for specialNumber1 here
        }
        else if (value == specialNumber2)
        {
            Debug.Log($"Dice {diceIndex} landed on a special number: {value}");
            // Add your special action for specialNumber2 here
        }
        // You can add more special number checks here as needed
    }

    // Method to reroll the dice
    public void RerollDices()
    {
        // You can add logic here to force the dice to reroll if needed
        // For example, setting their position or applying a force
    }

    public float GetLastValueDice1()
    {
        return lastValueDice1;
    }

    public float GetLastValueDice2()
    {
        return lastValueDice2;
    }
}
