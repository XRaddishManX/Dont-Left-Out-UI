using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   private Camera _camera;
   private float _targetRotation = 90f;
   private Vector3 _targetPosition = new Vector3(0, 46, 0);
    private MoveCamController _MovementController;
 
   [Tooltip("Entre mayor sea este valor, menor será la velocidad de la animación")]
   [SerializeField, Min(1)] private float _transitionDuration = 1;
   

    private void Awake()
   {
      _camera = GetComponent<Camera>();
      _MovementController = GetComponent<MoveCamController>();
   }

    public void SetCameraTopDownView()
   {
        StartCoroutine(ElevateCameratoTopDownView());
   }

    private IEnumerator ElevateCameratoTopDownView()
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Vector3 startPosition = transform.position;

        while (elapsedTime < _transitionDuration)
        {
            float t = elapsedTime / _transitionDuration;
            transform.rotation = Quaternion.Lerp(startRotation, Quaternion.Euler(_targetRotation, 0, 0), t);
            transform.position = Vector3.Lerp(startPosition, _targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(_targetRotation, 0, 0);
        transform.position = _targetPosition;

        

        _camera.orthographicSize = 28;
        _camera.farClipPlane = 50;
        _camera.nearClipPlane = 20;
    }

   public void SetCameraHomeScreenView()
   {
      transform.Rotate(new Vector3(0,0,0));
      transform.position = new Vector3(0, 2, -10);
      _camera.orthographic = false;
      _camera.farClipPlane = 0.3f;
      _camera.nearClipPlane = 70;
   }
   
   public void MoveCameraToRoomScenario()
   {
      transform.position = new Vector3(-200, 46, 0);
   }
   
   public void MoveCameraToMenuScenario()
   {
      transform.position = new Vector3(0, 46, 0);
   }

}
