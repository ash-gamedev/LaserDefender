using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Serializable variables
    [Header("Input")]
    [SerializeField] private float moveSpeed = 0.3f;

    [Header("Boundaries Padding")]
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;

    // private variables
    private Vector2 rawInput;
    private Vector2 minBounds;
    private Vector2 maxBounds;

    void Start()
    {
        InitBounds();
    }

    void Update()
    {
        Move();
    }

    #region Movement
    void InitBounds()
    {
        // Get the boundaries of the screen through the camera
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));

        // Add padding so user can't go off screen
        minBounds += new Vector2(paddingLeft, paddingBottom);
        maxBounds -= new Vector2(paddingRight, paddingTop);
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void Move()
    {
        // multiply input by move speed, and deltaTime (for framerate independent)
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();

        // use Clamp to restrict the movement depending on the boundaries
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x, maxBounds.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y, maxBounds.y);

        transform.position = newPos;
    }

    #endregion
}
