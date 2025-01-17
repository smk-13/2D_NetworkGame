using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 20;

    private void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

}
