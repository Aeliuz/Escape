using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallPuzzle : MonoBehaviour
{
    public BallPuzzleTracker tracker;


    public int row;
    public int column;
    public int diagonal;



    public void IncreaseRowAndColumnValue()
    {
        tracker.ChangeRowValue(row, 1);    
        tracker.ChangeColumnValue(column, 1);
    }

    public void DecreaseRowAndColumnValue() 
    {
        tracker.ChangeRowValue(row, -1);
        tracker.ChangeColumnValue(column, -1);
    }


    public void IncreaseDiagValue()
    {
        tracker.ChangeDiagonalValue(diagonal, 1);
    }

    public void DecreaseDiagValue()
    {
        tracker.ChangeDiagonalValue(diagonal, -1);
    }

    

}
