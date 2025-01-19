using UnityEngine;
using Unity.Netcode;


public class ProjectileLifeCycle : NetworkBehaviour
{

    float maxAge = 3f;
    float currentAge = 0;

    Collider2D[] hitColliders = new Collider2D[5];

    private void Update()
    {
        int numHit = Physics2D.OverlapBox(transform.position, transform.localScale,
            transform.rotation.z, new ContactFilter2D(), hitColliders);

        if (numHit > 0)
        {
            for (int i = 0; i < numHit; i++)
            {
                if (hitColliders[i].TryGetComponent(out Damageable damageable))
                {
                    Debug.Log("A damageable has been hit");
                    DespawnProjectileRpc();
                    break;
                }

            }
        }

        currentAge += Time.deltaTime;

        if (currentAge > maxAge)
        {
            DespawnProjectileRpc();
        }
    }


    // this works, but the latency is crazy high
    [Rpc(SendTo.Server)]
    private void DespawnProjectileRpc()
    {
        NetworkObject projectileNetworkObject = GetComponent<NetworkObject>();
        projectileNetworkObject.Despawn();
        Destroy(gameObject);
    }


    private void OnDisable()
    {
        currentAge = 0;
    }
}
