using UnityEngine;
using Unity.Netcode;

public class InfiniteBounds : NetworkBehaviour
{
    Rigidbody2D rb;
    Camera cam;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }


    private void FixedUpdate()
    {
        float sizeBuffer = transform.localScale.x + 0.5f;
        Vector3 topRightCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 bottomLeftCorner = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));

        bool moveObject = false;
        Vector3 newPosition = rb.position;

        if (newPosition.x > (topRightCorner.x + sizeBuffer))
        {
            newPosition.x = bottomLeftCorner.x - sizeBuffer;
            moveObject = true;
        }

        if (newPosition.x < (bottomLeftCorner.x - sizeBuffer))
        {
            newPosition.x = topRightCorner.x + sizeBuffer;
            moveObject = true;
        }

        if (newPosition.y > (topRightCorner.y + sizeBuffer))
        {
            newPosition.y = bottomLeftCorner.y - sizeBuffer;
            moveObject = true;
        }

        if (newPosition.y < (bottomLeftCorner.y - sizeBuffer))
        {
            newPosition.y = topRightCorner.y + sizeBuffer;
            moveObject = true;
        }

        if (moveObject)
        {
            rb.position = newPosition;
        }
    }
}