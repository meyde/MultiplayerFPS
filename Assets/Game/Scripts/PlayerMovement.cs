using System;
using UnityEngine;
using FishNet.Object;
using UnityEngine.InputSystem;

/// <summary>
/// Déplacement FPS : marcher et sauter.
/// Seul le propriétaire (IsOwner) lit les touches et bouge son personnage.
/// Le NetworkTransform envoie ensuite la position aux autres joueurs.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : NetworkBehaviour
{
    [Header("Références")]
    [SerializeField] private Transform cameraPivot;
    [Header("Vitesses")]
    [SerializeField] private float walkSpeed = 6f;
    [Header("Saut et gravité")]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;
    [Header("Souris")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    
    private CharacterController _controller;
    private float _pitch;
    private float _verticalVelocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!IsOwner)
            return;
        Look();
        Move();
    }

    private void Look()
    {
        Vector2 mouse = Mouse.current.delta.ReadValue() * mouseSensitivity;
        // On lit les déplacement de la souris, pas sa position.
        transform.Rotate(0f, mouse.x, 0f);
        _pitch = Mathf.Clamp(_pitch - mouse.y, -89f, 89f);
        cameraPivot.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }

    private void Move()
    {
        Keyboard kb = Keyboard.current;
        Vector2 input = Vector2.zero;
        //lecture des inputs:
        if (kb.wKey.isPressed) input.y += 1f;
        if (kb.sKey.isPressed) input.y -= 1f;
        if (kb.dKey.isPressed) input.x += 1f;
        if (kb.aKey.isPressed) input.x -= 1f;
        bool jumpPressed = kb.spaceKey.wasPressedThisFrame;
        
        
        
    }
    
}
