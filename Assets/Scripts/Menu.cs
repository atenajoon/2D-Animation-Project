using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Menu : MonoBehaviour
{
    // void Awake()
    // {
        //  // Instantiate the actions wrapper class
        // playerActionControls = new PlayerActionControls();
        // _rigidbody = GetComponent<Rigidbody2D>();

        // // For the Play & Quit actions, adding a callback method for the "performed" phase
        // menuControl.Menu.Play.performed += OnPlay;

        // menuControl.Menu.Quit.performed += OnQuit;
    // }
    
    
    // private void OnPlay(InputAction.CallbackContext context)
    // {
    //     // This is the "Play" action callback method
    //     SceneManager.LoadScene(2);
    // }

    // private void OnQuit(InputAction.CallbackContext context)
    // {
    //     // This is the "Quit" action callback method
    //     SceneManager.LoadScene(0);
    // }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OnQuitButton()
    {
        SceneManager.LoadScene(0);
    }
}
