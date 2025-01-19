using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 15;

    private void Update()
    {
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }

}
