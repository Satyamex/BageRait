using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

internal sealed class GameManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private TextMeshProUGUI playerScoreText, gameFpsText;
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator transitionAnimatior;

    private float fps = default;

    private void Update()
    {
        playerScoreText.text = "score : " + player.score.ToString();
        if (player.playerWon) 
        { 
            Invoke(nameof(ChangeSceneToMain), 2f);
            transitionAnimatior.SetTrigger("Transition");
        }
        if (Random.Range(1, 60) <= 2)
            LazyUpdate();
    }

    private void LazyUpdate() 
    {
        if (Time.smoothDeltaTime <= 0f) return;
        fps = Mathf.RoundToInt(1f / Time.smoothDeltaTime);
        gameFpsText.text = "fps : " + fps;
    }

    private void ChangeSceneToMain() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        player.playerWon = false;
        player.score = default;
    }
}
