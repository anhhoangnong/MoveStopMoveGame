using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    Vector3 destination;
    [SerializeField] protected NavMeshAgent agent;
    protected IState<Bot> currentState;


    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;

    private bool IsCanRunning => (GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Revive) || GameManager.Ins.IsState(GameState.Setting));

    private void Update()
    {
        if (IsCanRunning && currentState != null && !IsDead)
        {
            currentState.OnExecute(this);

            // TỰ ĐỘNG TÌM MỤC TIÊU GẦN NHẤT
            FindAndAttackNearbyTarget();
        }
    }


    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }

    }
    public override void OnInit()
    {
        base.OnInit();

        SetMask(false);
        ResetAnim();

        indicator.SetName(NameBot.GetRandomName());


        if (agent != null)
        {
            agent.enabled = false; // Tắt tạm

            // Tìm vị trí hợp lệ trên NavMesh
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }
    }

    public override void WearSkin()
    {
        base.WearSkin();

        //change random 
        ChangeSkin(SkinType.SKIN_Normal);
        ChangeWeapon(Utilities.RandomEnumValue<WeaponType>());
        ChangeHat(Utilities.RandomEnumValue<HatType>());
        ChangeAccessory(Utilities.RandomEnumValue<AccessoryType>());
        ChangePant(Utilities.RandomEnumValue<PantType>());
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
        CancelInvoke();
    }

    public override void OnDeath()
    {
        ChangeState(null);
        OnMoveStop();
        base.OnDeath();
        SetMask(false);
        Invoke(nameof(OnDespawn), 2f);
    }

    public override void OnMoveStop()
    {
        base.OnMoveStop();

        // Kiểm tra trước khi tắt agent
        if (agent != null && agent.enabled && agent.isOnNavMesh)
        {
            agent.isStopped = true;

        }

        ChangeAnim(Const.ANIM_IDLE);
    }

    public bool IsDestination => Vector3.Distance(TF.position, destination) - Mathf.Abs(TF.position.y - destination.y) < 0.1f;

    public void SetDestination(Vector3 point)
    {
        destination = point;

        // Kiểm tra agent hợp lệ trước khi set destination
        if (agent != null)
        {
            if (!agent.isOnNavMesh)
            {
                // Nếu không trên NavMesh, tìm vị trí gần nhất
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position, out hit, 10f, NavMesh.AllAreas))
                {
                    transform.position = hit.position;
                }
                else
                {
                    Debug.LogWarning($"Bot {name} không tìm thấy NavMesh!");
                    return;
                }
            }

            agent.enabled = true;
            agent.isStopped = false;
            agent.SetDestination(destination);
        }

        ChangeAnim(Const.ANIM_RUN);
    }

    public override void AddTarget(Character target)
    {
        base.AddTarget(target);

        if (!IsDead && Utilities.Chance(50, 100) && IsCanRunning)
        {
            ChangeState(new AttackState());
        }
    }

    private float lastTargetCheckTime;
    private const float TARGET_CHECK_INTERVAL = 0.5f; // Kiểm tra 2 lần/giây

    private void FindAndAttackNearbyTarget()
    {

        if (Time.time - lastTargetCheckTime < TARGET_CHECK_INTERVAL)
            return;

        lastTargetCheckTime = Time.time;

        // Tìm tất cả Character trong tầm tấn công
        Collider[] hits = Physics.OverlapSphere(TF.position, Character.ATT_RANGE);

        Character nearestTarget = null;
        float nearestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            Character target = hit.GetComponent<Character>();

            // Bỏ qua bản thân và target đã chết
            if (target == null || target == this || target.IsDead)
                continue;


            if (target is Player || target is Bot)
            {
                float distance = Vector3.Distance(TF.position, target.TF.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = target;
                }
            }
        }

        // Nếu tìm thấy target và chưa đang tấn công
        if (nearestTarget != null && !(currentState is AttackState))
        {
            AddTarget(nearestTarget);
        }
    }
}