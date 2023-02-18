using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PlayerManger;
using PlayerManger.Interfaces;
using UnityEngine;

public class HandiCaps : MonoBehaviour, IProcess
{
    public void CollisionEffective(GameObject obj)
    {
        if (obj.gameObject.name != "Coin")
            PlayerRanking.Instance.DeleteObject();
        else
        {
            PlayerRanking.Instance.ObjectTransformDelete();
            obj.transform.Rotate(90 * 1, 0, 0);
            obj.transform.DOMove(new Vector3(obj.transform.position.x, 0.54f, obj.transform.position.z), 0.5f);
            Destroy(obj.GetComponent<Rigidbody>());
            PlayerRanking.Instance.DeadPlayerWaiting();
        }
    }
}