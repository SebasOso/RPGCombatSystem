using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cinemachineTargetGroup;

    private List<Target> targets = new List<Target>();

    public Target currentTarget {get; private set;}

    private void OnTriggerEnter(Collider other) 
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
           return;
        }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }
    private void OnTriggerExit(Collider other) 
    {
        if (!other.TryGetComponent<Target>(out Target target))
        {
            return;
        } 
        RemoveTarget(target);
    }
    public bool SelectTarget()
    {
        if(targets.Count == 0){return false;}

        currentTarget = targets[0];
        cinemachineTargetGroup.AddMember(currentTarget.transform, 1f, 2f);

        return true;
    }
    public void Cancel()
    {
        if(currentTarget == null){return;}
        cinemachineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }
    private void RemoveTarget(Target targerToRemove)
    {
        if(currentTarget == targerToRemove)
        {
            cinemachineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }
        targerToRemove.OnDestroyed -= RemoveTarget;
        targets.Remove(targerToRemove);
    }
}
