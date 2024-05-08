using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyancyController : MonoBehaviour
{
    [SerializeField] private Transform[] floatingPoints;
    [SerializeField] private float underWaterDrag;
    [SerializeField] private float underWaterAngularDrag;
    [SerializeField] private float floatingPower;
    [SerializeField] private float waterHeight;
    private int _floatingPointsUnderWater;
    private float _airDrag;
    private float _airAngularDrag;

    private Rigidbody _rigidbody;

    private bool _underWater;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _floatingPointsUnderWater = 0;
        for (int i = 0; i < floatingPoints.Length; i++)
        {
            float difference = floatingPoints[i].position.y - waterHeight;
            if (difference < 0)
            {
                _rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floatingPoints[i].position, ForceMode.Force );
                _floatingPointsUnderWater++;
                if (!_underWater)
                {
                    _underWater = true;
                    SwitchState(true);
                }
            }
        }
        if (_underWater && _floatingPointsUnderWater == 0)
        {
            _underWater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderWater)
    {
        if (isUnderWater)
        {
            _rigidbody.drag = underWaterDrag;
            _rigidbody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            _rigidbody.drag = _airDrag;
            _rigidbody.angularDrag = _airAngularDrag;
        }
    }
}
