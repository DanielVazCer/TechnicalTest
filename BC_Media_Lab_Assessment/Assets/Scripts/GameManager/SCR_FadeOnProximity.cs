using UnityEngine;


public class SCR_FadeOnProximity : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 5f;
    public float fadeSpeed = 2f;
    public AudioClip sound;

    public SpriteRenderer _spriteRenderer;
    public AudioSource _audioSource;
    private bool _isVisible = false;
    private bool _wasVisible = false;

    void Start()
    {
        
        SetAlpha(0f);
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        _isVisible = distance <= detectionRadius;

        if (_isVisible && !_wasVisible)
        {
            
            PlaySound(1f); 
        }
        else if (!_isVisible && _wasVisible)
        {
            PlaySound(1.5f); 
        }

        float targetAlpha = _isVisible ? 1f : 0f;
        float currentAlpha = _spriteRenderer.color.a;
        float newAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);
        SetAlpha(newAlpha);

        _wasVisible = _isVisible;
    }

    void SetAlpha(float alpha)
    {
        Color c = _spriteRenderer.color;
        c.a = alpha;
        _spriteRenderer.color = c;
    }

    void PlaySound(float pitch)
    {
        if (sound == null) return;

        _audioSource.clip = sound;
        _audioSource.pitch = pitch;
        _audioSource.Play();
    }
}