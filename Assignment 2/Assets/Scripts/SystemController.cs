using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SystemController : MonoBehaviour
{
    public CommandPrompt command;
    // public ExitPanel exitPanel;
    private InputActions inputActions;
    private InputAction console;
    private InputAction exit;

    private void Awake() {
        command.ExitConsole();
        inputActions = new InputActions();
        console = inputActions.System.Console;
        exit = inputActions.System.Exit;
        console.performed += OnConsole;
        exit.performed += OnExit;
    }

    private void OnEnable() {
        console.Enable();
        exit.Enable();
    }

    private void OnDisable() {
        console.Disable();
        exit.Disable();
    }

    void OnConsole(InputAction.CallbackContext obj) {
        command.OpenConsole();
    }

    void OnExit(InputAction.CallbackContext obj) {
        command.ExitConsole();
    }
}
