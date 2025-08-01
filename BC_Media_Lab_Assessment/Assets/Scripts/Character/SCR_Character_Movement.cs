using UnityEngine;
using System.Collections.Generic;
using System;

public enum PlayerState { IDLE, RUN }
public enum PlayerDirection { UP, DOWN, LEFT, RIGHT }

public class SCR_Character_Movement : MonoBehaviour
{
    public bool CanMove { get; set; } = false;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _shadowSpriteRenderer;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _gravity = -9.8f;
    private Vector3 _velocity;

    [SerializeField] private float _frameRate = 60f;
    private float _animationTimer;
    private int _currentFrame = 0;

    [SerializeField] private Sprite[] _idleUpDown;
    [SerializeField] private Sprite[] _idleRightLeft;
    [SerializeField] private Sprite[] _runUpDown;
    [SerializeField] private Sprite[] _runRightLeft;

    [Tooltip("Define en qué frames se debe reproducir el sonido")]
    public int[] soundFrames;

    private PlayerState _playerState = PlayerState.IDLE;
    private PlayerDirection _playerDirection = PlayerDirection.DOWN;

    private Dictionary<(PlayerState, PlayerDirection), Sprite[]> _animations;

    void Start()
    {
        _animations = new Dictionary<(PlayerState, PlayerDirection), Sprite[]>()
        {
            {(PlayerState.IDLE, PlayerDirection.UP), _idleUpDown},
            {(PlayerState.IDLE, PlayerDirection.DOWN), _idleUpDown},
            {(PlayerState.IDLE, PlayerDirection.LEFT), _idleRightLeft},
            {(PlayerState.IDLE, PlayerDirection.RIGHT), _idleRightLeft},

            {(PlayerState.RUN, PlayerDirection.UP), _runUpDown},
            {(PlayerState.RUN, PlayerDirection.DOWN), _runUpDown},
            {(PlayerState.RUN, PlayerDirection.LEFT), _runRightLeft},
            {(PlayerState.RUN, PlayerDirection.RIGHT), _runRightLeft},
        };
    }

    void Update()
    {
        Animate();
        if (!CanMove) return;
        Vector3 inputDir = HandleInput();
        ApplyMovement(inputDir);
        UpdateStateAndDirection(inputDir);
    }

    private Vector3 HandleInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputDir = new Vector3(h, 0, v);
        inputDir = Vector3.ClampMagnitude(inputDir, 1f);

        return inputDir;
    }

    private void ApplyMovement(Vector3 inputDir)
    {
        if (!_characterController.isGrounded)
            _velocity.y += _gravity * Time.deltaTime;
        else
            _velocity.y = 0f;

        Vector3 move = inputDir * _speed;
        move.y = _velocity.y;

        _characterController.Move(move * Time.deltaTime);
    }

    private void UpdateStateAndDirection(Vector3 inputDir)
    {
        if (inputDir.magnitude == 0)
        {
            _playerState = PlayerState.IDLE;

            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }
        else
        {
            _playerState = PlayerState.RUN;

            if (Mathf.Abs(inputDir.z) > Mathf.Abs(inputDir.x))
            {
                _playerDirection = (inputDir.z > 0) ? PlayerDirection.UP : PlayerDirection.DOWN;
            }
            else
            {
                _playerDirection = (inputDir.x > 0) ? PlayerDirection.RIGHT : PlayerDirection.LEFT;
            }
        }
    }

    private void Animate()
    {
        if (!_animations.TryGetValue((_playerState, _playerDirection), out Sprite[] currentAnim) || currentAnim.Length == 0)
            return;

        _animationTimer += Time.deltaTime;
        if (_animationTimer >= 1 / _frameRate)
        {
            _animationTimer = 0f;
            _currentFrame = (_currentFrame + 1) % currentAnim.Length;
            _spriteRenderer.sprite = currentAnim[_currentFrame];

    
            if (_playerState == PlayerState.RUN && Array.Exists(soundFrames, frame => frame == _currentFrame))
            {
                if (!_audioSource.isPlaying)
                {
                    _audioSource.pitch = UnityEngine.Random.Range(1f, 1.85f);
                    _audioSource.Play();
                }
            }
        }

        if (_playerDirection == PlayerDirection.LEFT)
        {
            _spriteRenderer.flipX = true;
            _shadowSpriteRenderer.flipX = true;
        }
        else if (_playerDirection == PlayerDirection.RIGHT)
        {
            _spriteRenderer.flipX = false;
            _shadowSpriteRenderer.flipX = false;
        }
    }
}
