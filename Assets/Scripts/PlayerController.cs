using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float turnSpeed = 3;

    float moveAxis;
    float turnAxis;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        moveAxis = value.Get<float>() * moveSpeed;
    }

    void OnTurn(InputValue value)
    {
        turnAxis = value.Get<float>() * turnSpeed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.up * moveAxis);
        rb.AddTorque(turnAxis);
    }

    private void OnDisable()
    {
        moveAxis = 0;
        turnAxis = 0;
        rb.linearVelocity = Vector2.zero;
    }

    void OnFire()
    {
        GameObject projectile = ObjectPoolManager.Instance.projectilePool.GetObject(
            transform.position + (transform.up * 0.75f), transform.rotation);
    }



}