using UnityEngine;

public class LockKnob : MonoBehaviour
{
    private Transform knob;
    private int knobRotation;
    private int currentNumber;
    public int[] unlockNumber;
    [SerializeField] private int sequenceLength = 3;
    private int sequenceNumber = 0;


    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        knob = GetComponent<Transform>();
        unlockNumber = new int[sequenceLength];
        foreach (int i in unlockNumber)
        {
            unlockNumber[i] = Random.Range(0,10);
        }
    }

    public int targetNumber()
    {
        return unlockNumber[sequenceNumber];
    }

    //private void KnobRotated(GameObject theObject)
    //{
    //    if (theObject != this.gameObject) return;
    //    currentNumber = (int)knob.rotation.y / 36;
    //    knobRotation = currentNumber * 36;
    //    knob.Rotate(0f, knobRotation - knob.rotation.y, 0f);
    //    if (currentNumber == targetNumber())
    //    {
    //        sequenceNumber++;
    //        if (sequenceNumber == sequenceLength)
    //            OnFinished();
    //    }
    //}

    
    private void OnFinished()
    {
        Debug.LogWarning("Lock box opened!");
    }
}
