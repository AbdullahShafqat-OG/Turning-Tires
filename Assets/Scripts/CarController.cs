using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnSpeed = 150.0f;
    public float minTurn = -30.0f;
    public float maxTurn = 30.0f;
    public ScreenBounds screenBounds;
    public TrailRenderer trail;

    [SerializeField]
    private GameObject ground;

    private Vector3 _offsetBounds;
    private Camera _cam;
    private Vector3 _offsetCam;
    private Vector3 _offsetGround;

    private float _rotation = 0;

    private bool _alive = true;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        _offsetBounds = screenBounds.transform.position - transform.position;
        _offsetCam = _cam.transform.position - transform.position;
        _offsetGround = ground.transform.position - transform.position;
    }

    private void Update()
    {
        //if (!_alive) return;

        if (Input.GetKeyDown(KeyCode.Space))
            turnSpeed = -turnSpeed;

        Turn();
        Move();

        CamFollow();
        BoundsFollow();
        GroundFollow();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        StartCoroutine(Wrap());
    }

    private IEnumerator Wrap()
    {
        if (screenBounds.AmIOutOfBounds(transform.position))
        {
            trail.emitting = false;
            yield return new WaitForEndOfFrame();
            transform.position = screenBounds.CalculateWrappedPosition(transform.position);
            yield return new WaitForEndOfFrame();
            trail.emitting = true;
        }
    }

    private void Turn()
    {
        _rotation += turnSpeed * Time.deltaTime;
        _rotation = Mathf.Clamp(_rotation, minTurn, maxTurn);

        transform.localEulerAngles = new Vector3(0, _rotation, 0);
    }

    private void CamFollow()
    {
        Vector3 pos = transform.position + _offsetCam;
        pos.x = _cam.transform.position.x;
        _cam.transform.position = pos;
    }

    private void BoundsFollow()
    {
        Vector3 pos = transform.position + _offsetBounds;
        pos.x = _cam.transform.position.x;
        screenBounds.transform.position = pos;
    }

    private void GroundFollow()
    {
        Vector3 pos = transform.position + _offsetGround;
        pos.x = ground.transform.position.x;
        ground.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
            _alive = false;
        else if (other.CompareTag("Coin"))
            Messenger.Broadcast(GameEvent.COIN_COLLECTED);
    }
}
