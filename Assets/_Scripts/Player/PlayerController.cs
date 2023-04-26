using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    Animator m_Animator;
    CharacterController m_CharacterController;
    private void OnEnable()
    {
        GameManager.OnGameManagerStateChanged += OnGameManagerStateChanged;
    }

    private void OnDisable()
    {
        GameManager.OnGameManagerStateChanged -= OnGameManagerStateChanged;

    }

    private void OnGameManagerStateChanged(GameState state)
    {

        switch (state)
        {
            case GameState.Start:
                break;
            case GameState.Initialize:
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if (GameManager.CurrentState == GameState.Playing)
        {
            Move();
        }
    }

    void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (moveInput.magnitude > 0)
        {
            Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y) * moveSpeed * Time.deltaTime;
           // transform.Translate(movement, Space.World);
            m_CharacterController.Move(movement);
            ChangeAnimation("RunForward");
            transform.rotation = Quaternion.LookRotation(movement);
        }
        else
        {
            ChangeAnimation("Idle");
        }
    }

    void ChangeAnimation(string stateName)
    {
        m_Animator.Play(stateName);
    }
}
