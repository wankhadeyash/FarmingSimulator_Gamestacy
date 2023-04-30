using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Player Controller script -- uses CharacterController to control the player's movement.
public class PlayerController : MonoBehaviour
{
    // Declare a field to store the player's movement speed.
    [SerializeField] float moveSpeed = 5f;

    // Declare fields to store the player's Animator and CharacterController components.
    Animator m_Animator;
    CharacterController m_CharacterController;

    // Register event handlers for the GameManager's OnGameManagerStateChanged event when this script is enabled.
    private void OnEnable()
    {
        GameManager.OnGameManagerStateChanged += OnGameManagerStateChanged;
    }

    // Unregister event handlers for the GameManager's OnGameManagerStateChanged event when this script is disabled.
    private void OnDisable()
    {
        GameManager.OnGameManagerStateChanged -= OnGameManagerStateChanged;
    }

    // Event handler that is fired when the game manager's state changes.
    private void OnGameManagerStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Initialize:
                // Get the Animator and CharacterController components when the game is initializing.
                m_Animator = GetComponent<Animator>();
                m_CharacterController = GetComponent<CharacterController>();
                break;
            case GameState.Playing:
                break;
            case GameState.Paused:
                break;
            case GameState.Resume:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is in the "Playing" state, move the player.
        if (GameManager.CurrentState == GameState.Playing)
        {
            Move();
        }
    }

    // Move the player based on input from the user.
    void Move()
    {
        // Get input from the user to determine the player's movement direction.
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput.magnitude > 0)
        {
            // Calculate the player's movement and move them using the CharacterController component.
            Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
            m_CharacterController.Move(movement);

            // Change the player's animation state to "RunForward".
            ChangeAnimation("RunForward");

            // Rotate the player to face their movement direction.
            transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            // Change the player's animation state to "Idle" if they are not moving.
            ChangeAnimation("Idle");
        }
    }

    // Change the player's animation state.
    void ChangeAnimation(string stateName)
    {
        m_Animator.Play(stateName);
    }
}






