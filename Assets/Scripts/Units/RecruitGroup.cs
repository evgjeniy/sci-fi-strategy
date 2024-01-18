using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecruitGroup : MonoBehaviour
{
    [SerializeField]
    private List<Recruit> recruits;

    [field:SerializeField]
    public GuardPost GuardPost { get; set; }

    private void OnEnable()
    {
        GuardPost.OnValuesChanged += UpdateRecruits;
    }

    [Button("Update")]
    private void UpdateRecruits()
    {
        Vector3[] positions = GetGuardPositions();

        for(int i = 0; i < recruits.Count; i++)
        {
            recruits[i].UpdatePosition(positions[i]);
        }
    }

    public Vector3[] GetGuardPositions() 
    {
        List<Vector3> positions = new List<Vector3>(recruits.Count);

        for (float i = 0; i < Mathf.PI * 2; i += Mathf.PI * 2 / recruits.Count)
        {
            positions.Add(new Vector3(
                GuardPost.Position.x + GuardPost.Radius * Mathf.Sin(i),
                GuardPost.Position.y,
                GuardPost.Position.z + GuardPost.Radius * Mathf.Cos(i)
            ));
        }
        return positions.ToArray();
    }

    private void OnDrawGizmos()
    {
        foreach (var position in GetGuardPositions())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(position, Vector3.one);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(GuardPost.Position, GuardPost.Radius);
        }
    }
}
