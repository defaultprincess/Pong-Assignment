using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int leftScore;
    public int rightScore;

    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    public Rigidbody ball;
    public Transform ballSpawn;
    public float serveSpeed = 10f;

    public int winningScore = 11;

    void Start()
    {
        UpdateUI();
        ResetBall(Random.value < 0.5f ? -1 : 1);
    }

    public void PointScored(Goal.Side goalHit)
    {
        if (goalHit == Goal.Side.Left)
        {
            rightScore++;
            Debug.Log($"Right scored! Score: Left {leftScore} - Right {rightScore}");
        }
        else
        {
            leftScore++;
            Debug.Log($"Left scored! Score: Left {leftScore} - Right {rightScore}");
        }

        if (leftScore >= winningScore || rightScore >= winningScore)
        {
            string winner = leftScore > rightScore ? "Left" : "Right";
            Debug.Log($"Game Over, {winner} Paddle Wins");

            leftScore = 0;
            rightScore = 0;
            UpdateUI();
            ResetBall(Random.value < 0.5f ? -1 : 1);
            return;
        }

        UpdateUI();
        ResetBall(goalHit == Goal.Side.Left ? -1 : 1);
    }

    void UpdateUI()
    {
        if (leftText) leftText.text = leftScore.ToString();
        if (rightText) rightText.text = rightScore.ToString();
    }

    void ResetBall(int towardY)
    {
        ball.position = ballSpawn.position;
        ball.linearVelocity = Vector3.zero;

        float dirY = Mathf.Sign(towardY);
        ball.linearVelocity = new Vector3(0f, dirY, 0f) * serveSpeed;
    }
}