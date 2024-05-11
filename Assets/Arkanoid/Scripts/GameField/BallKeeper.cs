using UnityEngine;

namespace Arkanoid.GameField
{
    public class BallKeeper : MonoBehaviour
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}