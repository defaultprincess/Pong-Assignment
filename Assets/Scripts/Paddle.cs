using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public Key upKey = Key.W;
    public Key downKey = Key.S;

    public float forceStrength = 10f;

    public float minBallSpeed = 10f;
    public float speedIncrease = 0.5f;

    public AudioSource audioSource;
    public AudioClip hitClip;
    public float minPitch = 0.9f;
    public float maxPitch = 1.3f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float dir = 0f;
        if (Keyboard.current[upKey].isPressed) dir += 1f;
        if (Keyboard.current[downKey].isPressed) dir -= 1f;

        if (dir != 0f)
            rb.AddForce(new Vector3(0f, 0f, dir * forceStrength), ForceMode.Force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody ballRb = collision.rigidbody;
        if (ballRb == null) return;

        Vector3 incoming = ballRb.linearVelocity;
        if (incoming.sqrMagnitude < 0.0001f) incoming = new Vector3(0f, 1f, 0f);

        Vector3 normal = collision.contacts[0].normal;

        float baseSpeed = Mathf.Max(incoming.magnitude, minBallSpeed);
        float newSpeed = baseSpeed + speedIncrease;

        Vector3 reflectedDir = Vector3.Reflect(incoming.normalized, normal).normalized;
        ballRb.linearVelocity = reflectedDir * newSpeed;

        if (audioSource != null && hitClip != null)
        {
            float speedT = Mathf.InverseLerp(minBallSpeed, minBallSpeed + 12f, newSpeed);
            float hitOffset = Mathf.Abs(collision.GetContact(0).point.z - transform.position.z);
            float hitT = Mathf.InverseLerp(0f, 1.5f, hitOffset);

            audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Max(speedT, hitT));
            audioSource.PlayOneShot(hitClip);
        }
    }
}