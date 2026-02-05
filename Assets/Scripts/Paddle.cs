using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    public Key upKey = Key.W;
    public Key downKey = Key.S;
    public float forceStrength = 10f;

    Rigidbody rBody;

    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float dir = 0f;
        if (Keyboard.current[upKey].isPressed) dir += 1f;
        if (Keyboard.current[downKey].isPressed) dir -= 1f;

        if (dir != 0f)
            rBody.AddForce(new Vector3(0f, 0f, dir * forceStrength), ForceMode.Force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody ballRigidbody = collision.rigidbody;
        if (ballRigidbody == null) return;

        float speed = ballRigidbody.linearVelocity.magnitude;
        Vector3 incoming = ballRigidbody.linearVelocity;
        Vector3 normal = collision.contacts[0].normal;

        Vector3 reflected = Vector3.Reflect(incoming, normal);
        ballRigidbody.linearVelocity = reflected.normalized * Mathf.Max(speed, 10f);
    }
}