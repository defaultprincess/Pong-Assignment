using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public float speed = 10f;

    Rigidbody rb;
    Vector3 lastDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.linearDamping = 0f;
        rb.angularDamping = 0f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Start()
    {
        float dirY = Random.value < 0.5f ? -1f : 1f;
        lastDir = new Vector3(0f, dirY, 0f).normalized;
        rb.linearVelocity = lastDir * speed;
    }

    void FixedUpdate()
    {
        Vector3 v = rb.linearVelocity;

        if (v.sqrMagnitude > 0.0001f)
        {
            lastDir = v.normalized;
            speed = Mathf.Max(speed, v.magnitude);
        }

        rb.linearVelocity = lastDir * speed;
    }

    public void Serve(int towardY)
    {
        rb.position = rb.position;
        rb.linearVelocity = Vector3.zero;

        speed = Mathf.Max(1f, speed);
        float dirY = towardY == 0 ? (Random.value < 0.5f ? -1f : 1f) : Mathf.Sign(towardY);
        lastDir = new Vector3(0f, dirY, Random.Range(-0.2f, 0.2f)).normalized;

        rb.linearVelocity = lastDir * speed;
    }
}