using UnityEngine;

[ExecuteAlways]
public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraToLookAt;
    
    private void Update()
    {
        transform.LookAt(cameraToLookAt);
        transform.forward *= -1;
    }
}
