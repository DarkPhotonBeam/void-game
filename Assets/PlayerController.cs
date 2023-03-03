using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraTransform;
    public float acceleration = 100f;
    public float speedLimit = 10f;
    public float torque = 10f;
    public float rotationLimit = 10f;
    public float cameraStiffness = 1f;
    public float cameraRange = 75f;

    private Rigidbody2D _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.magnitude <= speedLimit) // Thrust
        {
            Vector2 force = new Vector2(Mathf.Sin(_rigidbody.rotation * (Mathf.PI / 180)), -Mathf.Cos(_rigidbody.rotation * (Mathf.PI / 180))) * (Time.deltaTime * acceleration * Input.GetAxis("Throttle"));
            _rigidbody.AddForce(force);
        }
        
        if (_rigidbody.velocity.magnitude > speedLimit)
            _rigidbody.velocity = _rigidbody.velocity.normalized * speedLimit;

        if (Mathf.Abs(_rigidbody.angularVelocity) <= rotationLimit) // Rotation
        {
            //Debug.Log("BELOW");
            _rigidbody.AddTorque(-Input.GetAxis("Horizontal") * (Time.deltaTime * torque));
            
        }
        
        if (_rigidbody.angularVelocity > rotationLimit) _rigidbody.angularVelocity = rotationLimit;
        if (_rigidbody.angularVelocity < -rotationLimit) _rigidbody.angularVelocity = -rotationLimit;

        //Debug.Log(_rigidbody.angularVelocity);
        
        // Camera
        // Vector3 newPosition = _rigidbody.position + (_rigidbody.velocity.normalized * cameraRange);
        // Vector3 oldPosition = cameraTransform.position;
        // oldPosition = new Vector2(
        //         Mathf.Lerp(oldPosition.x, newPosition.x, cameraStiffness),
        //         Mathf.Lerp(oldPosition.y, newPosition.y, cameraStiffness)
        //     );
        // cameraTransform.position = oldPosition;
        cameraTransform.position = _rigidbody.position;
    }
}
