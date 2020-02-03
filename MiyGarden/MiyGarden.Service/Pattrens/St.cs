using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Pattrens
{
    public abstract class 門派
    {
        public List<Skill> Skills { set; get; } = new List<Skill>();
        public int Attack { set; get; }
        public abstract int GetOuterAttack(int 外攻傷害, int 身法);
        public abstract int GetInnerAttack(int 內攻傷害, int 根骨);
    }

    public class 純陽 : 門派
    {
        public override int GetOuterAttack(int 外攻傷害, int 身法) => Convert.ToInt16(外攻傷害 * 1.2 + 身法 * 5);

        public override int GetInnerAttack(int 內攻傷害, int 根骨)
        {
            throw new NotImplementedException();
        }

        public 純陽(int 外攻傷害, int 身法)
        {
            this.Attack = this.GetOuterAttack(外攻傷害, 身法);
            Skills.Add(new 八荒歸元(this.Attack));
            Skills.Add(new 萬劍歸宗(this.Attack));
        }
    }

    public class 萬花 : 門派
    {
        public override int GetInnerAttack(int 內攻傷害, int 根骨)
        {
            throw new NotImplementedException();
        }

        public override int GetOuterAttack(int 外攻傷害, int 身法)
        {
            throw new NotImplementedException();
        }

        public 萬花(int 內攻傷害, int 根骨)
        {
            this.Attack = this.GetInnerAttack(內攻傷害, 根骨);

        }
    }

    public class Player
    {
        public string Name { get; set; }
        public int HP { set; get; }
        public int Level { set; get; }
        public int 外攻傷害 { set; get; }
        public int 身法 { set; get; }
        public int 根骨 { set; get; }

        public int 攻擊力 { set; get; }
        public 門派 Job { set; get; }

        public void UseSkill(string skillName, params Player[] players)
        {
            this.Job.Skills.FirstOrDefault(x => x.Name == skillName).Use(this, players);
        }

        public Player(string name, string jobName, int level)
        {
            this.Name = name;
            this.身法 = level * 5;
            this.根骨 = level * 5;
            this.外攻傷害 = level * 50;
            this.Job = new JobSelector().GetJob(jobName, 外攻傷害, 身法);
            this.攻擊力 = this.Job.Attack;
        }
    }

    public abstract class Skill : IEffect, IAttack
    {
        public string Name { set; get; }
        public string EffectName { set; get; }

        public abstract int GetDamage();

        public void Attack(Player user, params Player[] players)
        {
            foreach (var player in players)
            {
                Console.WriteLine(user.Name + "使用" + this.Name + "對" + player.Name + "造成" + GetDamage() + "點傷害");
            }
        }

        public void EffectOthers(Player user, params Player[] players)
        {
            foreach (var player in players)
            {
                Console.WriteLine(user.Name + "使用" + this.Name + "使" + player.Name + "獲得了" + EffectName + "效果");
            }
        }

        public void EffectSelf(Player user)
        {
            Console.WriteLine(user.Name + "使用" + this.Name + "使" + user.Name + "獲得了" + EffectName + "效果");
        }

        public abstract void Use(Player user, params Player[] players);
    }

    public interface IAttack
    {
        int GetDamage();
        void Attack(Player user, params Player[] players);
    }

    public interface IEffect
    {
        string EffectName { set; get; }
        void EffectSelf(Player user);
        void EffectOthers(Player user, params Player[] players);
    }

    public class 八荒歸元 : Skill, IAttack
    {
        private readonly int _攻擊力;
        public override int GetDamage()
        {
            return this._攻擊力 * 3;
        }

        public override void Use(Player user, params Player[] players)
        {
            this.Attack(user, players);
        }

        public 八荒歸元(int 攻擊力)
        {
            this._攻擊力 = 攻擊力;
            this.Name = "八荒歸元";
        }
    }

    public class 萬劍歸宗 : Skill, IEffect, IAttack
    {
        private readonly int _攻擊力;
        public override int GetDamage()
        {
            return this._攻擊力 + 500;
        }

        public override void Use(Player user, params Player[] players)
        {
            this.Attack(user, players);
            this.EffectOthers(user, players);
        }

        public 萬劍歸宗(int 攻擊力)
        {
            this._攻擊力 = 攻擊力;
            this.Name = "萬劍歸宗";
            this.EffectName = "鎖足";
        }
    }

    public class 蕨陰指 : Skill, IEffect, IAttack
    {
        public 蕨陰指()
        {
            this.EffectName = "沉默";
        }

        public override int GetDamage()
        {
            throw new NotImplementedException();
        }

        public override void Use(Player user, params Player[] players)
        {
            throw new NotImplementedException();
        }
    }

    public class JobSelector
    {
        public 門派 GetJob(string name, int 外攻傷害, int 身法)
        {
            switch (name)
            {
                case "純陽":
                    return new 純陽(外攻傷害, 身法);
                default:
                    return null;
            }
        }
    }
}
