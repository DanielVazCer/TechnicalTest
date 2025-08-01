using UnityEngine;
using System.Collections;

public class SCR_GameManager_GM : MonoBehaviour
{
    
    public Camera mainCamera;
    public Vector3 targetLocalPosition;
    public float targetSize = 5f;
    public float transitionDuration = 2f;
    public SCR_Character_Movement _playerMovement;

    public AudioSource _audioSource;

    
    void Start()
    {
        _audioSource.Play();
    }
    void Update()
    {

    }

    public void StartGame()
    {
        _playerMovement.CanMove = true;

        StartCoroutine(TransitionCamera());
    }

    public void QuitGame()
    {

    }
    private IEnumerator TransitionCamera()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        Vector3 startLocalPos = mainCamera.transform.localPosition;
        float startSize = mainCamera.orthographicSize;

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;
            float smoothedT = SmoothStep(t);

            float newY = Mathf.Lerp(startLocalPos.y, targetLocalPosition.y, smoothedT);
            float newZ = Mathf.Lerp(startLocalPos.z, targetLocalPosition.z, smoothedT);

            mainCamera.transform.localPosition = new Vector3(startLocalPos.x, newY, newZ);
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, smoothedT);

            elapsed += Time.deltaTime;
            yield return null;
        }

    
        mainCamera.transform.localPosition = new Vector3(startLocalPos.x, targetLocalPosition.y, targetLocalPosition.z);
        mainCamera.orthographicSize = targetSize;
    }
    private float SmoothStep(float t)
    {
        return t * t * (3f - 2f * t);

    }
}




