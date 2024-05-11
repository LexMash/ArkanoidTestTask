using Arkanoid.PowerUPs;

namespace Arkanoid.Bricks
{
    public class BrickData
    {
        public ModType ModType;
        public int Health;

        public BrickData(ModType modType, int health)
        {
            ModType = modType;
            Health = health;
        }
    }
}
