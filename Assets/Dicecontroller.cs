using UnityEngine;

public class DiceController : MonoBehaviour
{
    public Dice[] dices; // Array of dices (4 dices in this case)

    private float[] lastValues; // Array to store the last values of each dice
    private Quaternion[] initialRotations; // Store initial rotations of each dice

    // Special numbers and their corresponding actions
    public float[] specialNumbers = new float[4]; // Array of 4 different numbers

    void Start()
    {
        lastValues = new float[dices.Length];
        initialRotations = new Quaternion[dices.Length];

        // Capture initial rotations of each dice
        for (int i = 0; i < dices.Length; i++)
        {
            if (dices[i] != null)
            {
                initialRotations[i] = dices[i].transform.rotation; // Save initial rotation
                Dice dice = dices[i]; // Capture the dice in a local variable for the closure
                dice.onLanded.AddListener((value) => UpdateDiceValue(dice, value));
            }
        }
    }

    private void UpdateDiceValue(Dice dice, float value)
    {
        int diceIndex = System.Array.IndexOf(dices, dice); // Get the index of the dice
        lastValues[diceIndex] = value;
        Debug.Log($"Dice {diceIndex + 1} landed on: {value}");
        RotateDiceToNumber(dice, specialNumbers[diceIndex]); // Rotate the dice to the chosen number
    }

    private void RotateDiceToNumber(Dice dice, float targetNumber)
    {
        // Reset the dice's rotation to a base known state before applying the new rotation
        dice.transform.localRotation = Quaternion.identity; // Reset to zero rotation

        // Define the target rotation based on the number
        Quaternion targetRotation = Quaternion.identity;

        switch (targetNumber)
        {
            case 1:
                // Front face (1) pointing up
                targetRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
                break;
            case 2:
                // Right face (2) pointing up
                targetRotation = Quaternion.LookRotation(Vector3.right, Vector3.up);
                break;
            case 3:
                // Top face (3) pointing up
                targetRotation = Quaternion.LookRotation(Vector3.up, Vector3.forward);
                break;
            case 4:
                // Bottom face (4) pointing up
                targetRotation = Quaternion.LookRotation(-Vector3.up, Vector3.forward);
                break;
            case 5:
                // Left face (5) pointing up
                targetRotation = Quaternion.LookRotation(-Vector3.right, Vector3.up);
                break;
            case 6:
                // Back face (6) pointing up
                targetRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
                break;
            default:
                Debug.LogWarning("Invalid number for dice rotation.");
                return;
        }

        // Apply the target rotation to the dice
        dice.transform.localRotation = targetRotation;
    }

    public float GetLastValue(int diceIndex)
    {
        return lastValues[diceIndex];
    }
}
