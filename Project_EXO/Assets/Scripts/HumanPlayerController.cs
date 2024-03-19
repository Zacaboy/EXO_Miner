using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanPlayerController : MonoBehaviour
{
    public float baseMoveSpeed = 5f; // Base speed of movement
    public float scaleIncreaseAmount = 0.1f; // Amount to increase scale when an enemy is killed
    public float speedIncreasePerKill = 1f; // Amount to increase speed per kill
    public float yIncreasePerKill = 0.1f; // Amount to raise player on Y axis per kill
    public AudioClip killSound; // Sound to play when the player kills an enemy

    private int killCount = 0; // Number of enemies killed
    private float currentMoveSpeed; // Current speed of movement
    private AudioSource audioSource; // Reference to the AudioSource component

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an enemy
        if (other.CompareTag("Enemy"))
        {
            // Increase player's scale, speed, and Y position when they kill an enemy
            IncreaseScale();
            IncreaseSpeed();
            RaisePlayer();
            // Play kill sound effect
            if (audioSource != null && killSound != null)
            {
                audioSource.PlayOneShot(killSound);
            }
        }
    }

    void Start()
    {
        currentMoveSpeed = baseMoveSpeed; // Initialize current speed to base speed
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    void Update()
    {
        // Get input from WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the GameObject
        transform.Translate(movement * currentMoveSpeed * Time.deltaTime);
    }

    void IncreaseScale()
    {
        // Increase the player's scale when they kill an enemy
        transform.localScale += new Vector3(scaleIncreaseAmount, scaleIncreaseAmount, scaleIncreaseAmount);
    }

    void IncreaseSpeed()
    {
        // Increase the player's speed when they kill an enemy
        currentMoveSpeed += speedIncreasePerKill;
        killCount++;
        Debug.Log("Kill Count: " + killCount);
        Debug.Log("Current Move Speed: " + currentMoveSpeed);
    }

    void RaisePlayer()
    {
        // Raise the player on the Y axis when they kill an enemy
        transform.position += new Vector3(0f, yIncreasePerKill, 0f);
    }
}