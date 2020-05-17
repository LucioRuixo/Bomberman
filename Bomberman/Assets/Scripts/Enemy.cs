using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool canTurn;

    float movementSpeed;
    float movementDecimalSpace;
    float cellDetectionRange;
    float rayDistance;

    Vector2 gridPosition;

    EnemyManager enemyManager;

    public static event Action onDamageDealtToPlayer;

    void Start()
    {
        canTurn = true;

        movementSpeed = 1.25f;
        rayDistance = 0.5f;

        gridPosition = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.z));

        enemyManager = GetComponentInParent<EnemyManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            onDamageDealtToPlayer();
    }

    void Update()
    {
        Vector3 movement = transform.forward;

        cellDetectionRange = 0.025f;

        gridPosition.x = Mathf.Round(transform.position.x);
        gridPosition.y = Mathf.Round(transform.position.z);
        transform.position += movement * movementSpeed * Time.deltaTime;

        CheckProximity();
        UpdateMovementDecimalPlace();

        if (gridPosition.x % 2 == 0 && gridPosition.y % 2 == 0)
            CheckTurn();
    }

    void CheckProximity()
    {
        bool wallInProximity = false;
        bool columnInProximity = false;
        bool destroyableColumnInProximity = false;

        Vector3 rotation;

        Ray ray;

        RaycastHit raycastHit;

        ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out raycastHit, rayDistance);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.yellow);

        if (raycastHit.transform != null)
        {
            wallInProximity = raycastHit.transform.gameObject.CompareTag("Wall");

            columnInProximity = raycastHit.transform.gameObject.CompareTag("Column");

            destroyableColumnInProximity = raycastHit.transform.gameObject.CompareTag("DestroyableColumn");
        }

        if (wallInProximity || columnInProximity || destroyableColumnInProximity)
        {
            rotation = GetSidewaysTurn();

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotation);
        }
    }

    void UpdateMovementDecimalPlace()
    {
        if (transform.forward.x == 0f)
            movementDecimalSpace = Mathf.Abs(transform.position.z - (int)transform.position.z);
        else
            movementDecimalSpace = Mathf.Abs(transform.position.x - (int)transform.position.x);
    }

    void CheckTurn()
    {
        Vector3 rotation;

        if (transform.forward.x == 0f)
        {
            if (movementDecimalSpace > cellDetectionRange)
            {
                canTurn = true;

                return;
            }
        }
        else
        {
            if (movementDecimalSpace > cellDetectionRange)
            {
                canTurn = true;

                return;
            }
        }

        if (canTurn)
        {
            rotation = GetRandomDirection();

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotation);

            canTurn = false;
        }
    }

    Vector3 GetSidewaysTurn()
    {
        float eulerRotationBase = 90f;
        float rotationY;

        float[] eulerRotationMultipliers = { 1, 3 };

        rotationY = eulerRotationBase * eulerRotationMultipliers[UnityEngine.Random.Range(0, eulerRotationMultipliers.Length)];

        return new Vector3(0, rotationY, 0);
    }

    public static Vector3 GetRandomDirection()
    {
        float eulerRotationBase = 90f;
        float rotationY;

        float[] eulerRotationMultipliers = { 0, 1, 3 };

        rotationY = eulerRotationBase * eulerRotationMultipliers[UnityEngine.Random.Range(0, eulerRotationMultipliers.Length)];

        return new Vector3(0, rotationY, 0);
    }
}