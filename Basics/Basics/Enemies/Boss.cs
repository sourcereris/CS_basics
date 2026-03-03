using System;
using System.Collections.Generic;
using System.Text;

namespace Basics
{   
    public enum BossState
    {
        Sleeping,
        Enraged,
        Dead
    }
    public class Boss : Enemy
    {
        Player? CurrentTarget;
        
        public BossState State 
        {
            get
            {
                return Health switch
                {
                    <= 0 => BossState.Dead,
                    < 50 => BossState.Enraged,
                    _ => BossState.Sleeping,
                };
            }
        }

        public override void Attack()
        {
            string attackMessage = State switch
            {
                BossState.Enraged => $"RAAAH! {CurrentTarget.Name ?? "Air"}",
                BossState.Sleeping => "Zzz...",
                _ => "..."
            };
        }
    }
}
