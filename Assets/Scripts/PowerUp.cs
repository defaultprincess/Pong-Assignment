using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type { BallSpeedUp, PaddleGrow }
    public Type type;

    public float ballSpeedMultiplier = 1.5f;
    public float paddleGrowMultiplier = 1.5f;
    public float duration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        if (type == Type.BallSpeedUp)
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = rb.linearVelocity * ballSpeedMultiplier;
        }
        else if (type == Type.PaddleGrow)
        {
            PaddlePower target = FindNearestPaddle(other.transform.position);
            if (target != null) target.Grow(paddleGrowMultiplier, duration);
        }

        Destroy(gameObject);
    }

    PaddlePower FindNearestPaddle(Vector3 pos)
    {
        PaddlePower[] paddles = FindObjectsOfType<PaddlePower>();
        if (paddles.Length == 0) return null;

        PaddlePower best = paddles[0];
        float bestD = Vector3.Distance(pos, best.transform.position);

        for (int i = 1; i < paddles.Length; i++)
        {
            float d = Vector3.Distance(pos, paddles[i].transform.position);
            if (d < bestD) { bestD = d; best = paddles[i]; }
        }
        return best;
    }
}