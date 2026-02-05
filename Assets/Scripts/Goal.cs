using UnityEngine;

public class Goal : MonoBehaviour
{
    public enum Side { Left, Right }
    public Side whichSide;
    public ScoreManager scoreManager;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        scoreManager.PointScored(whichSide);
    }
}