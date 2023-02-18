using System;
using PlayerManger.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public static bool Movement;
    private float LimitValue = 1f;

    public void PlayButton() => Movement = true;

    void Update()
    {
        if (gameObject.GetComponent<Rigidbody>() != null && Movement)
            PlayerMove();
    }

    private void PlayerMove()
    {
        float halfScreen = Screen.width / 2;
        float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
        float finalXPos = xPos * LimitValue;
        Debug.Log(xPos);
        transform.localPosition = new Vector3(finalXPos, 0.660f, transform.position.z);
        transform.localPosition += -transform.right * Time.deltaTime * 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.GetComponent<IProcess>() != null)
            other.gameObject.transform.GetComponent<IProcess>().CollisionEffective(this.gameObject);
    }
}