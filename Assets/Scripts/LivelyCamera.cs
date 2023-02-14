using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivelyCamera : MonoBehaviour
{
    [SerializeField, Min(0f)]
    float 
        jostleStrength = 40f, 
        pushStrength = 1f,
        springStrength = 100f,
        dampingStrength = 10f,
        maxDeltaTime = 1f / 60f;

    public Vector2 test;

    public CarController player;

    Vector3 anchorPosition, velocity;

    private void Awake()
    {
        anchorPosition = transform.localPosition;
    }

    public void JostleY() => velocity.y += jostleStrength;

    public void PushXZ(Vector2 impulse)
    {
        velocity.x += pushStrength * impulse.x;
        velocity.z += pushStrength * impulse.y;
    }

    private void Update()
    {
        anchorPosition = player.currentCamPosition;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            PushXZ(test);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            JostleY();
        }
    }

    private void LateUpdate()
    {
        float dt = Time.deltaTime;
        while (dt > maxDeltaTime)
        {
            TimeStep(maxDeltaTime);
            dt -= maxDeltaTime;
        }
        TimeStep(dt);
    }

    private void TimeStep(float dt)
    {
        Vector3 displacement = anchorPosition - transform.localPosition;
        Vector3 acceleration = springStrength * displacement - dampingStrength * velocity;
        velocity += acceleration * dt;
        transform.localPosition += velocity * dt;
    }
}
