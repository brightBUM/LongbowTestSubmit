using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    [SerializeField] ScoreManager scoreManager;
    Animator anim;
    bool gameOver = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.CurrentHealth <= 0 && !gameOver)
        {
            anim.SetTrigger("GameOver");
            scoreManager.CheckScore();
            gameOver= true;
        }
    }
}
