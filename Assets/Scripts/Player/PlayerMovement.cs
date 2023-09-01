using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
using UnityEditor.Rendering;

public class PlayerMovement : MonoBehaviour
{
    //this code sucks ass and it keeps getting worse

    //TODO: Fix water layering issue
    private SpriteRenderer spriteRenderer;

    private Vector2 movementDirection;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float acceleration = 1.0f;
    [SerializeField] private float maxSpeed = 7;
    private float currentSpeed = 0;

    //Lost Indicator 
    private bool inIslandRange = false;
    [SerializeField] private GameObject lostIndicator;





    private void Start()
    {
        lostIndicator.SetActive(false);
    }

    private void Update()
    {
        if (!inIslandRange)
        {
            lostIndicator.SetActive(true);
        }
        else if(inIslandRange)
        {
            lostIndicator.SetActive(false);
        }



        //gets string value of ink variable
        int speedLevel = ((Ink.Runtime.IntValue)DialogueManager.getInstance().GetVariablesState("player_speed")).value;

        if (speedLevel == 2)
        {
            maxSpeed = 10;
        }

    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementDirection = new Vector2(horizontalInput, verticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        movementDirection.Normalize();

        //stop if player is in dialogue
        if (DialogueManager.getInstance().dialogueIsPlaying)
        {
            movementDirection = Vector2.zero;
            return;
        }

        //movement
        transform.Translate(movementDirection * currentSpeed * inputMagnitude * Time.deltaTime, Space.World);

        //ramp up speed
        currentSpeed += acceleration;
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
    }
    private void RotatePlayer()
    {
        if (movementDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("entered range");
        }

        if (other.gameObject.CompareTag("IslandArea"))
        {
            inIslandRange = true;
            Debug.Log("entered island area");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Left range");
        }

        if (other.gameObject.CompareTag("IslandArea"))
        {
            inIslandRange = false;
            Debug.Log("left island area");
        }
    }

}