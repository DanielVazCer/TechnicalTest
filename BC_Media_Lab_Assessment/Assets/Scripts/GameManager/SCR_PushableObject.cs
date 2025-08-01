using UnityEngine;



public class SCR_PushableObject : MonoBehaviour
{
    [SerializeField] private float pushForce = 5f;

    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

      
        Vector3 pushDir = (transform.position - collision.transform.position).normalized;
        pushDir.y = 0f; 

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(pushDir * pushForce, ForceMode.Force);
    }
}