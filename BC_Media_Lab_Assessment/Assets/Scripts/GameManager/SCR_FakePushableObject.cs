using UnityEngine;

public class SCR_FakePushableObject : MonoBehaviour
{
    public Transform _player;           
    public float _detectionRange = .05f;  
    public float _moveSpeed = 2f;      
    public float _maxDisplacement = 3f; 

    private Vector3 initialPosition;
    private bool isMoving = false;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer < _detectionRange)
        {
            isMoving = true;

            float direction = Mathf.Sign(transform.position.x - _player.position.x);

         
            Vector3 targetPosition = transform.position + new Vector3(direction * _moveSpeed * Time.deltaTime, 0, 0);

            
            float displacement = Mathf.Abs(targetPosition.x - initialPosition.x);

          
            if (displacement <= _maxDisplacement)
            {
                transform.position = targetPosition;
            }
        }
        else
        {
            isMoving = false;
        }
    }
}
