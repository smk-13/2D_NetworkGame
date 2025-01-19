using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerController : NetworkBehaviour
{
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float turnSpeed = 3;

    float moveAxis;
    float turnAxis;

    [SerializeField] GameObject projectilePrefab;

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
        SpawnProjectileRpc();
    }

    // this works, but the latency is crazy high
    [Rpc(SendTo.Server)]
    private void SpawnProjectileRpc()
    {
        GameObject projectile = Instantiate(projectilePrefab,
            transform.position + (transform.up * 1.5f), transform.rotation);

        NetworkObject projectileNetworkObject = projectile.GetComponent<NetworkObject>();
        projectileNetworkObject.Spawn();
    }







}