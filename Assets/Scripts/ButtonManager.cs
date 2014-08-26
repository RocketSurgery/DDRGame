using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour 
{
    public void GoToInstructions()
    {
        Application.LoadLevel("InstructionsMenu");
    }

    public void GoMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }

    public void GoToGamePlay()
    {
        Application.LoadLevel("TheRealLevel");
    }
}
