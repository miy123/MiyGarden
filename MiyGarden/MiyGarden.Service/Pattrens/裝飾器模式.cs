using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Pattrens
{
    public class DecroratorPattern
    {
        public void StartTest()
        {
            var character = new MyCharacter();
            var swordCharacterWrapper = new SwordCharacterWrapper(character);
            swordCharacterWrapper.Decrorator();
            //character.Show();
        }
    }

    public class SwordCharacterWrapper : CharacterWrapper
    {
        public SwordCharacterWrapper(Character character) : base(character)
        {
            this.Name = "神劍一把";
            this._character = character;
        }

        public override void Decrorator()
        {
            this._character.Characters.Add(this);
            base.Decrorator();
        }
    }

    public abstract class CharacterWrapper : Character
    {
        protected Character _character;

        public CharacterWrapper(Character character)
        {
            this._character = character;
        }

        public override void Decrorator()
        {
            _character.Decrorator();
            Console.WriteLine("裝備了" + this.Name);
        }

        public override void Show()
        {
            Console.WriteLine(this._character.Name);
        }
    }

    public class MyCharacter : Character
    {
        public MyCharacter()
        {
            this.Name = "小王";
        }

        public override void Decrorator()
        {
            Console.Write("小王");
        }

        public override void Show()
        {
            foreach (var chars in this.Characters)
            {
                Console.WriteLine(chars.Name);
            }
        }
    }

    public abstract class Character
    {
        public string Name { get; set; }

        public abstract void Decrorator();

        public abstract void Show();

        public List<Character> Characters { get; set; } = new List<Character>();
    }
}
