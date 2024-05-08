using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamController : MonoBehaviour
{
    public float CameraVelocity;

    private int HorizontalValue = 3;
    private int VerticalValue = 3;

    // Update is called once per frame
    void FixedUpdate()
    {
        StartMovement();
    }

    public void StartMovement()
    {
        Random.InitState((int)Time.time);

        // Se obtienen valores random en un rango definido entre las variables HorizontalValue y VerticalValue
        int Move_X = Random.Range(0, HorizontalValue - 1);
        int Move_Z = Random.Range(0, VerticalValue - 1);

        // Mueve la camara en los ejes X y Y según los valores random de las variables Move_X y Move_Y
        Vector3 Move = new Vector3(Move_X, transform.position.y, Move_Z);

        transform.Translate(Move * Time.deltaTime * CameraVelocity);
    }
}
