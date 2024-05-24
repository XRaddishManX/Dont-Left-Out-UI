using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public float MoveSpeed = 5f;                                // Velocidad de movimiento de la cámara
    public float StopDuration = 2f;                             // Lapso de tiempo en que se detiene
    public Vector2 MovementLimits = new Vector2(10f, 10f);      // Límites del movimiento en X y Z

    private Vector3 TargetPosition;
    private bool isMoving = true;
    private float MoveTimer;

    void Start()
    {
        SetRandomTargetPosition();
        MoveTimer = StopDuration;
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }

        MoveTimer -= Time.deltaTime;

        if (MoveTimer <= 0f)
        {
            isMoving = !isMoving;
            MoveTimer = StopDuration;

            if (isMoving)
            {
                SetRandomTargetPosition();
            }
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (TargetPosition - transform.position).normalized;
        transform.position += direction * MoveSpeed * Time.deltaTime;

        // Verificar si el objeto ha alcanzado la posición objetivo
        if (Vector3.Distance(transform.position, TargetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-MovementLimits.x, MovementLimits.x);
        float randomZ = Random.Range(-MovementLimits.y, MovementLimits.y);
        TargetPosition = new Vector3(randomX, transform.position.y, randomZ);
    }
}