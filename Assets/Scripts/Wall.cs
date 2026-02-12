using UnityEngine;

public class Wall : MonoBehaviour
{
    public float minSpeed = 10f;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody rb = collision.rigidbody;
        if (rb == null) return;

        Vector3 incoming = rb.linearVelocity;
        if (incoming.sqrMagnitude < 0.0001f) incoming = Vector3.up;

        float s = Mathf.Max(incoming.magnitude, minSpeed);
        Vector3 normal = collision.contacts[0].normal;

        rb.linearVelocity = Vector3.Reflect(incoming.normalized, normal).normalized * s;
    }
}