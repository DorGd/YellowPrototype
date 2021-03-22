using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship : MonoBehaviour, IInteractable
{
    public Action[] CalcInteractions()
    {
        return new Action[] { Escape };
    }

    public void Escape()
    {
        SceneManager.LoadScene(1);
    }
}
