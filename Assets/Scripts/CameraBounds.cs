using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 topRightCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector3 bottomLeftCorner = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));

        bool moveObject = false;
        Vector3 newPosition = transform.position;

        if (transform.position.x > topRightCorner.x)
        {
            newPosition.x = topRightCorner.x;
            moveObject = true;
        }

        if (transform.position.y > topRightCorner.y)
        {
            newPosition.y = topRightCorner.y;
            moveObject = true;
        }

        if (transform.position.x < bottomLeftCorner.x)
        {
            newPosition.x = bottomLeftCorner.x;
            moveObject = true;
        }

        if(transform.position.y < bottomLeftCorner.y)
        {
            newPosition.y = bottomLeftCorner.y;
            moveObject = true;
        }

        if (moveObject)
        {
            transform.position = newPosition;
        }

    }




}
