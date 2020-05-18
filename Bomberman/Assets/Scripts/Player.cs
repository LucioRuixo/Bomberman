using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int maxPlaceableBombs;

    float movementSpeed;
    float bombPositionY;

    Vector3 initialPosition;
    Vector3 movement;

    new Rigidbody rigidbody;

    List<GameObject> placedBombs;

    [HideInInspector] public int lives;
    [HideInInspector] public int bombRange;

    [HideInInspector] public float positionY;

    public GameObject bombPrefab;

    void Start()
    {
        lives = 2;
        maxPlaceableBombs = 1;

        movementSpeed = 15f;
        bombPositionY = 0.75f;

        rigidbody = GetComponent<Rigidbody>();

        placedBombs = new List<GameObject>();

        bombRange = 1;

        positionY = 1f;

        initialPosition = new Vector3(0f, positionY, 0f);

        Explosion.onDamageDealtToPlayer += OnDamageReceived;
        Enemy.onDamageDealtToPlayer += OnDamageReceived;
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
            movement.x += Input.GetAxisRaw("Horizontal") * movementSpeed * Time.deltaTime;

        if (Input.GetButton("Vertical"))
            movement.z += Input.GetAxisRaw("Vertical") * movementSpeed * Time.deltaTime;

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

    void OnDamageReceived()
    {
        lives--;

        ResetPosition();
    }

    void ResetPosition()
    {
        transform.position = initialPosition;
    }
}