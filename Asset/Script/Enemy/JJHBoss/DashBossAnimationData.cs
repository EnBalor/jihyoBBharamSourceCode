using UnityEngine;

[System.Serializable]
public class DashBossAnimationData
{
    [SerializeField] private string groundParameterName = "@Ground";
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string chaseParameterName = "Chase";

    [SerializeField] private string attackParameterName = "@Attack";
    [SerializeField] private string dashParameterName = "Dash";
    [SerializeField] private string chopParameterName = "Chop";

    public int GroundParameterHash { get; private set; }
    public int IdleParameterHash { get; private set; }
    public int ChaseParameterHash { get; private set; }

    public int AttackParameterHash { get; private set; }
    public int DashParameterHash { get; private set; }
    public int ChopParameterHash { get; private set; }

    public void Initialize()
    {
        GroundParameterHash = Animator.StringToHash(groundParameterName);
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        ChaseParameterHash = Animator.StringToHash(chaseParameterName);

        AttackParameterHash = Animator.StringToHash(attackParameterName);
        DashParameterHash = Animator.StringToHash(dashParameterName);
        ChopParameterHash = Animator.StringToHash(chopParameterName);
    }
}