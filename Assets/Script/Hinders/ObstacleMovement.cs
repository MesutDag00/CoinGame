using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    void Update() => this.gameObject.transform.Rotate(0, 0, 180 * Time.deltaTime);
}