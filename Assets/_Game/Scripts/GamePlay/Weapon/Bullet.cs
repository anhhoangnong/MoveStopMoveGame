using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    protected Character character;
    [SerializeField] protected float moveSpeed = 6f;
    protected bool isRunning;

    //di chuyển viên đạn
    public virtual void OnInit(Character character, Vector3 target, float size)
    {
        this.character = character;
        TF.forward = (target - TF.position).normalized; 
        isRunning = true;
    }

    //despawn viên đạn khi va chạm
    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    protected virtual void OnStop() { }

    //xử lý va chạm 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Const.TAG_CHARACTER))
        {
            //Lấy IHit từ đối tượng va chạm
            IHit hit = Cache.GetIHit(other);
            //Lấy Character từ đối tượng va chạm
            if (hit != null && hit != (IHit)character) // tránh tự bắn trúng mình
            {
                hit.OnHit(
                    () => {
                        character.AddScore(1);
                        ParticlePool.Play(Utilities.RandomInMember(ParticleType.Hit_1, ParticleType.Hit_2, ParticleType.Hit_3), TF.position);
                        SimplePool.Despawn(this);
                    });
            }
        }

        if (other.CompareTag(Const.TAG_BLOCK))
        {
            OnStop();
        }

    }
}
