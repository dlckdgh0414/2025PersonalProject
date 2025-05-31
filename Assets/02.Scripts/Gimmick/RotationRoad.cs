using UnityEngine;

public class RotationRoad : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Euler(0,-90,0);
        }
    }
}
