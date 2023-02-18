using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMovement : MonoBehaviour
{
    void Update() => transform.localPosition += -transform.up * 0.01f;
}