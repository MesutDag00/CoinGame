using PlayerManger;
using PlayerManger.Interfaces;
using UnityEngine;

public class AddingElements : MonoBehaviour, IProcess
{
    public void CollisionEffective(GameObject obj)
    {
        PlayerRanking.Instance.AddObject();
           Destroy(gameObject);
    }
}