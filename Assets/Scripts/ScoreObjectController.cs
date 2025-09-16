using UnityEngine;

internal sealed class ScoreObjectController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private ParticleSystem scoreCollectParticles;
    [SerializeField] private SpriteRenderer scoreSpriteRenderer;
    [SerializeField] private PolygonCollider2D scorePointCollider;

    public void Die() 
    {
        scoreSpriteRenderer.enabled = false;
        scoreCollectParticles.Play();
        scorePointCollider.enabled = false;
        Invoke(nameof(DieTrue), 2f);
    }

    private void DieTrue() 
    {
        Destroy(gameObject);
    }
}
