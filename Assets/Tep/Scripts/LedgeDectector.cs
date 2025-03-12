using System;
using UnityEngine;

public class LedgeDectector : MonoBehaviour
{
    public event Action<Vector3> OnLedgeDetect;
    private void OnTriggerEnter(Collider other)
    {
        OnLedgeDetect?.Invoke(other.transform.forward); 
    }
}
