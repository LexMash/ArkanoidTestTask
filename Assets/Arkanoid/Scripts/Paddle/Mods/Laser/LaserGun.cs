using UnityEngine;

namespace Arkanoid.Paddle.FX.Laser
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private Transform _projectileOriginL;
        [SerializeField] private Transform _projectileOriginR;
        [SerializeField] private Transform _visualRoot;      
        
        private bool _isEnabled;
        private PaddleConfig _config;

        private Vector3 _initPosition;
        private Vector3 _enabledPosition;

        public void Construct(PaddleConfig config)
        {
            _config = config;

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
            if (!_isEnabled)
                return;
        }
    }
}
