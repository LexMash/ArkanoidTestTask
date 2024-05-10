using UnityEngine;

namespace Arkanoid.Paddle.FX.Laser
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private Transform _projectileOriginL;
        [SerializeField] private Transform _projectileOriginR;
        [SerializeField] private Transform _visualRoot;      
        
        private bool _isEnabled;
        private PaddleConfig config;

        public void Construct(PaddleConfig config)
        {

        }

        public void Enable()
        {

        }

        public void Disable()
        {
            _isEnabled = false;
        }

        private void Update()
        {

        }
    }
}
