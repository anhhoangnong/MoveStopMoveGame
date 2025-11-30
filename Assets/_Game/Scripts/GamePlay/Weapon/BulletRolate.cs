using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRolate : Bullet
{
    // Thời gian tồn tại của viên đạn
    public const float TIME_ALIVE = 1f;
    [SerializeField] Transform child;
    // Tốc độ quay của viên đạn
    CounterTime counterTime = new CounterTime();

    public override void OnInit(Character character, Vector3 target, float size)
    {
        base.OnInit(character, target, size);
        counterTime.Start(OnDespawn, TIME_ALIVE * size);
    }

    void Update()
    {
        //counterTime update để đếm thời gian sống của viên đạn
        counterTime.Execute();
        if (isRunning) 
        {
            TF.Translate(TF.forward * moveSpeed * Time.deltaTime, Space.World);
            child.Rotate(Vector3.up * -6, Space.Self);
        }
    }

    protected override void OnStop()
    {
        base.OnStop();
        isRunning = false;
    }
}
