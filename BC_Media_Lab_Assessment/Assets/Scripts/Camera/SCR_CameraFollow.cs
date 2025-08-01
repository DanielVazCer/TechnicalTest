using UnityEngine;

public class SCR_CameraFollow : MonoBehaviour
{
    public SCR_Character_Movement player;

    public Vector2 minLimit;
    public Vector2 maxLimit;

    public float smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        if (!player.CanMove || player == null) return;

       
        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

    
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, smoothTime);

      
        float clampedX = Mathf.Clamp(smoothedPosition.x, minLimit.x, maxLimit.x);

    
        transform.position = new Vector3(clampedX, transform.position.y, smoothedPosition.z);
    }
}
