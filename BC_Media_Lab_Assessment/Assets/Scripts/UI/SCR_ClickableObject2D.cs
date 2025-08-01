using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ClickableObject2D : MonoBehaviour
{
    public AudioClip _hoverSound;
    public AudioClip _clickSound;
    public AudioSource _audioSource;

    [Range(0f, 1f)] public float _hoverOpacity = 0.5f;
    private float _originalOpacity;
    public SpriteRenderer _spriteRenderer;
    public bool _IsExit;

    public Light2D _light2D;
 
    public SCR_GameManager_GM _gameManager;

    private void Start()
    {
        if (_spriteRenderer != null)
            _originalOpacity = _spriteRenderer.color.a;
        _light2D.intensity = 0f;
    }

    private void OnMouseEnter()
    {
       
            Color c = _spriteRenderer.color;
            c.a = _hoverOpacity;
            _spriteRenderer.color = c;

            _light2D.intensity = 4.75f;
            _audioSource.PlayOneShot(_hoverSound);
    }

    private void OnMouseExit()
    {
        _light2D.intensity = 0f;
        Color c = _spriteRenderer.color;
            c.a = _originalOpacity;
            _spriteRenderer.color = c;
        
    }

    private void OnMouseDown()
    {
        
            _audioSource.PlayOneShot(_clickSound);

      if(!_IsExit)
        {

            _gameManager.StartGame();
            _IsExit = true;
        }
      else
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

    }
}
