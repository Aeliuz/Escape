using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    bool ballPuzzleWin = false;
    bool safeWin = false;
    bool pipeWin = false;
    bool lionWin = false;
    bool orreryWin = false;

    public AudioSource win;
    public GameObject winText;
    public GameObject crystalBall;

    private void OnEnable()
    {
        Orrery.OnOrreryFinished += OrreryWin;
        LockButtons.LockBoxOpen += SafeWin;
        LionPuzzle.OnLionPuzzleComplete += LionWin;
    }

    private void OnDisable()
    {
        Orrery.OnOrreryFinished -= OrreryWin;
        LockButtons.LockBoxOpen -= SafeWin;
        LionPuzzle.OnLionPuzzleComplete -= LionWin;
    }

    public void BallPuzzleWin()
    {
        ballPuzzleWin=true;
        win.Play();
        WinALL();
    }

    public void SafeWin() 
    { 
        safeWin=true; 
        win.Play();
        WinALL();
    }

    public void PipeWin()
    {
        pipeWin=true;
        win.Play();
        WinALL();
    }

    public void LionWin()
    {
        lionWin=true;
        win.Play();
        WinALL();
        crystalBall.SetActive(true);
    }

    public void OrreryWin() 
    {  
        orreryWin=true;
        win.Play();
        WinALL();
    }

    private void WinALL()
    {
        if (ballPuzzleWin && orreryWin && lionWin && pipeWin && safeWin) 
        {
            winText.SetActive(true);
            win.Play();
        }
    }
}
