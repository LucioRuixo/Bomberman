using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int maxPlaceableBombs;

    float movementSpeed;
    float bombPositionY;

    Vector3 movement;

    new Rigidbody rigidbody;

    List<GameObject> placedBombs;

    [HideInInspector] public int bombRange;

    [HideInInspector] public float positionY;

    public GameObject bombPrefab;

    void Start()
    {
        maxPlaceableBombs = 1;
        bombRange = 3;

        movementSpeed = 0.05f;
        bombPositionY = 0.75f;

        rigidbody = GetComponent<Rigidbody>();

        placedBombs = new List<GameObject>();

        positionY = 1f;
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

        if (Input.GetButtonDown("Place Bomb") && placedBombs.Count < maxPlaceableBombs)
            PlaceBomb();
    }

    void PlaceBomb()
    {
        float positionX = Mathf.Round(transform.position.x);
        float positionZ = Mathf.Round(transform.position.z);

        Vector3 position = new Vector3(positionX, bombPositionY, positionZ);

        placedBombs.Add(Instantiate(bombPrefab, position, Quaternion.identity));
        placedBombs[placedBombs.Count - 1].GetComponent<Bomb>().onExplosion += OnBombExplosion;
    }

    void OnBombExplosion()
    {
        placedBombs.RemoveAt(0);
    }
}