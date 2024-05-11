using Arkanoid.Infrastracture.Pool;
using System;
using UnityEngine;

namespace Arkanoid.Capsules
{
    /// <summary>
    /// Капсулы с бонусами и модификаторами
    /// </summary>
    public class CapsuleView : MonoBehaviour, IReusable
    {
        [field: SerializeField] public ModType FxType { get; private set; }
        [SerializeField] private float _fallSpeed;

        public event Action<IReusable> Released;

        private void Update()
        {
            transform.position += _fallSpeed * Time.deltaTime * Vector3.down;
        }
    }
}
