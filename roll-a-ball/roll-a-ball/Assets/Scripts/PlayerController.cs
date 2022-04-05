using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winText;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int numJumps;
    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winText.SetActive(false);
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
        if (isGrounded())
        {
            numJumps = 1;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue jumpValue)
    {
        if (jumpValue.isPressed && numJumps > 0)
        {
            rb.velocity = Vector2.up * speed;
            numJumps--;
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        // collects and counts pickups
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;

            SetCountText();
        }
    }

    void SetCountText()
    {
        // Displays pickup counts and displays win text if necessary
        countText.text = "Count: " + count.ToString();

        if (count >= 12)
        {
            winText.SetActive(true);
        }
    }
}
