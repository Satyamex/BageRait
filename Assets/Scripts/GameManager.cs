using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

internal sealed class GameManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private TextMeshProUGUI playerScoreText, gameFpsText;
    [SerializeField] private PlayerController player;
    [SerializeField] private Animator transitionAnimatior;

    private void Update()
    {
        playerScoreText.text = "score : " + player.score.ToString();
        if (player.playerWon) 
        { 
            Invoke(nameof(ChangeSceneToMain), 2f);
            transitionAnimatior.SetTrigger("Transition");
        }
        if (Time.smoothDeltaTime == 0f) return;
        gameFpsText.text = "fps : " + 1f / Mathf.RoundToInt(Time.smoothDeltaTime);
    }

    private void ChangeSceneToMain() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        player.playerWon = false;
        player.score = default;
    }
}
