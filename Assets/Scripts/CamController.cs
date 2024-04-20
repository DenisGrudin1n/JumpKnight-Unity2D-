using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private int speed;
    [SerializeField] private float offsetX, offsetY;
    private Vector3 temp;

    void Update()
    {
        temp = player.position;
        temp.x += offsetX;
        temp.y += offsetY;
        temp.z = -10f;
        if (temp.x < -4.6f) temp.x = -4.6f;
        if (temp.x > 4.6f) temp.x = 4.6f;
        if (temp.y < 0f) temp.y = 0f;
        if (temp.y > 71f) temp.y = 71f;
        transform.position = Vector3.Lerp(transform.position, temp, speed * Time.deltaTime);
    }
}
