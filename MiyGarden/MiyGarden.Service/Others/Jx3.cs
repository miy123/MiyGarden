using MiyGarden.Service.Pattrens;

namespace MiyGarden.Service.Others
{
    public class Jx3
    {
        public void Start()
        {
            // test
            var player = new Player("蒼小冥", "純陽", 100);
            var player2 = new Player("李小生", "純陽", 100);
            player.UseSkill("八荒歸元", player2);
            player.UseSkill("萬劍歸宗", player2);
        }
    }
}
