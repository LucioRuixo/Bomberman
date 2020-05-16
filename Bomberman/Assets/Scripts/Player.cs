using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int placeableBombsAmount;
    int placedBombs;

    float movementSpeed;

    Vector3 movement;

    new Rigidbody rigidbody;

    [HideInInspector] public float positionY;

    public GameObject bombPrefab;

    void Start()
    {
        placeableBombsAmount = 1;
        placedBombs = 0;

        movementSpeed = 0.05f;

        rigidbody = GetComponent<Rigidbody>();

        positionY = 1f;

        Bomb.onExplotion += UpdatePlacedBombs;
    }

    void FixedUpdate()
    {
        if (movement != Vector3.zero)
            rigidbody.MovePosition(rigidbody.position + movement);

        if (rigidbody.position.y != positionY)
            rigidbody.position = new Vector3(rigidbody.position.x, positionY, rigidbody.position.z);
    }

    void Update()
    {
        CheckInput();

        if (transform.rotation != Quaternion.identity)
            transform.rotation = Quaternion.identity;
    }

    void CheckInput()
    {
        movement = Vector3.zero;

        if (Input.GetButton("Horizontal"))
            movement.x += Input.GetAxisRaw("Horizontal") * movementSpeed;

        if (Input.GetButton("Vertical"))
            movement.z += Input.GetAxisRaw("Vertical") * movementSpeed;

        if (Input.GetButtonDown("Place Bomb") && placedBombs < placeableBombsAmount)
            PlaceBomb();
    }

    void PlaceBomb()
    {
        float positionX = Mathf.Round(transform.position.x );
        float positionZ = Mathf.Round(transform.position.z );

        Vector3 position = new Vector3(positionX, Bomb.positionY, positionZ);

        Instantiate(bombPrefab, position, Quaternion.identity);
        UpdatePlacedBombs(true);
    }

    void UpdatePlacedBombs(bool bombPlaced)
    {
        if (bombPlaced)
            placedBombs++;
        else
            placedBombs--;
    }
}