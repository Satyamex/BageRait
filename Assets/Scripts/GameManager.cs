using TMPro;
using UnityEngine;

internal sealed class GameManager : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private PlayerController player;

    private void Update()
    {
        playerScoreText.text = "score : " + player.score.ToString();
    }
}
