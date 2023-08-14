using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float speed = 20.0f;
    public float turnSpeed = 150.0f;
    public float minTurn = -30.0f;
    public float maxTurn = 30.0f;
    public TrailRenderer trail;

    [SerializeField]
    private float coinCollectionRadius = 2.0f;

    [Header("Special Modes")]
    public bool coinMagnet = false;
    public bool shield = false;
    public bool annihilator = false;
    public bool ghost = false;

    private ScreenBounds screenBounds;
    private GameObject ground;
    [SerializeField]
    private GameObject explosionParticle;

    public Vector3 currentCamPosition;

    private Vector3 _offsetBounds;
    private Camera _cam;
    private Vector3 _offsetCam;
    private Vector3 _offsetGround;

    private float _rotation = 0;

    public bool alive { get; private set; } = true;

    private bool firstTap = false;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Start()
    {
        screenBounds = FindObjectOfType<ScreenBounds>();
        ground = GameObject.FindGameObjectWithTag("Respawn");

        _offsetBounds = screenBounds.transform.position - transform.position;
        _offsetCam = _cam.transform.position - transform.position;
        _offsetGround = ground.transform.position - transform.position;

        turnSpeed = -turnSpeed;
    }

    private void Update()
    {
        if (!alive) return;

        //Turn();
        //Move();

        currentCamPosition = FollowPlayer(_cam.transform, _offsetCam);
        FollowPlayer(screenBounds.transform, _offsetBounds);
        FollowPlayer(ground.transform, _offsetGround);

        CollectCoin();
        CoinMagnet();
    }

    public void Move()
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

    public void Turn()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            firstTap = true;
            turnSpeed = -turnSpeed;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    firstTap = true;
                    turnSpeed = -turnSpeed;
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    break;
            }
        }

        if (!firstTap) return;

        _rotation += turnSpeed * Time.deltaTime;
        _rotation = Mathf.Clamp(_rotation, minTurn, maxTurn);

        transform.localEulerAngles = new Vector3(0, _rotation, 0);
    }

    private Vector3 FollowPlayer(Transform t, Vector3 offset)
    {
        Vector3 pos = transform.position + offset;
        pos.x = t.position.x;
        pos.y = t.position.y;
        t.position = pos;
        return pos;
    }

    private void CollectCoin()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, coinCollectionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Coin"))
                Messenger<Transform, Transform>.Broadcast(GameEvent.COIN_PULLED, this.transform, col.transform);
        }
    }

    private void CoinMagnet()
    {
        if (!coinMagnet) return;

        Ray ray = new Ray(transform.position, transform.right);
        RaycastHit[] hits;

        hits = Physics.SphereCastAll(ray, 1.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.transform.CompareTag("Coin"))
            {
                Messenger<Transform, Transform>.Broadcast(GameEvent.COIN_PULLED, this.transform, hit.transform);
            }
        }

        Ray rayLeft = new Ray(transform.position, -transform.right);
        RaycastHit[] hitsLeft;

        hitsLeft = Physics.SphereCastAll(rayLeft, 1.0f);

        for (int i = 0; i < hitsLeft.Length; i++)
        {
            RaycastHit hit = hitsLeft[i];
            if (hit.transform.CompareTag("Coin"))
            {
                Messenger<Transform, Transform>.Broadcast(GameEvent.COIN_PULLED, this.transform, hit.transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (annihilator)
                Annihilate(other.gameObject);
            else if (!shield && !ghost)
                PlayerDead();
        }
    }

    private void Annihilate(GameObject obstacle)
    {
        obstacle.SetActive(false);
        Messenger<int>.Broadcast(GameEvent.OBSTACLE_DESTROYED, 5);
    }

    private void PlayerDead()
    {
        alive = false;
        Instantiate(explosionParticle, transform.position, Quaternion.identity);

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer r in renderers)
        {
            if (r.gameObject.GetComponent<Collider>() == null)
            {
                r.gameObject.AddComponent(typeof(BoxCollider));
                r.gameObject.AddComponent(typeof(Rigidbody));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Ray ray = new Ray(transform.position, transform.right);
        Gizmos.DrawRay(ray);

        Ray ray2 = new Ray(transform.position, -transform.right);
        Gizmos.DrawRay(ray2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, coinCollectionRadius);
    }
}
