using System.Collections.Generic;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;


public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;
    private List<Target> targets = new List<Target>();
    public Target CurrentTarget { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
         
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target);
    }
    public bool SelectTarget()
    {
        if (targets.Count == 0)
        {
            return false;
        }
        CurrentTarget = targets[0];
        cineTargetGroup.AddMember(CurrentTarget.transform, 1, 2);
        return true;
    }
    public void Cancel()
    {
        if(CurrentTarget == null) { return; }
        CurrentTarget = null;
        cineTargetGroup.RemoveMember(CurrentTarget.transform);
    }
    private void RemoveTarget(Target target)
    {
      
        if (CurrentTarget == target)
        {
           cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}

