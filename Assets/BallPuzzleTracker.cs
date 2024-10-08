using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BallPuzzleTracker : MonoBehaviour
{

    public GameObject winChecker;

    public int[] rows = new int[5];
    public int[] cols = new int[5];
    public int[] diag = new int[2];

    public void ChangeRowValue(int row, int value)
    {
        rows[row] += value;
    }

    public void ChangeColumnValue(int  column, int value) 
    { 
        cols[column] += value;
        CheckComplete();
    }

    public void ChangeDiagonalValue(int diagonal, int value) 
    { 
        diag[diagonal] += value;
    }


    void CheckComplete()
    {
        if ((rows[0] == 1) &&
            (rows[1] == 4) &&
            (rows[2] == 2) &&
            (rows[3] == 2) &&
            (rows[4] == 1) &&
            (cols[0] == 1) &&
            (cols[1] == 3) &&
            (cols[2] == 2) &&
            (cols[3] == 3) &&
            (cols[4] == 1) &&
            (diag[0] == 2) &&
            (diag[1] == 2))
        {
            Debug.Log("Win");
            winChecker.GetComponent<PuzzleManager>().BallPuzzleWin();
        }
    }

}
