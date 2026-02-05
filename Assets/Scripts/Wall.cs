using UnityEngine;

public class Wall : MonoBehaviour
{
    public float minSpeed = 10f;

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball")) return;

        Rigidbody rb = collision.rigidbody;
        if (rb == null) return;

        float speed = rb.linearVelocity.magnitude;
        Vector3 incoming = rb.linearVelocity;
        Vector3 normal = collision.contacts[0].normal;

        Vector3 reflected = Vector3.Reflect(incoming, normal);
        rb.linearVelocity = reflected.normalized * Mathf.Max(speed, minSpeed);
    }
}