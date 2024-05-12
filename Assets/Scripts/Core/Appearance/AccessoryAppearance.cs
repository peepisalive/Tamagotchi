using UnityEngine;

namespace Core
{
    public sealed class AccessoryAppearance : MonoBehaviour
    {
        [field: SerializeField] public AccessoryType Type { get; private set; }
        [SerializeField] private MeshRenderer _meshRenderer;

        public void SetColor(Color color)
        {
            if (color == Color.clear)
                return;

            var material = Type != AccessoryType.Crown
                ? _meshRenderer.materials[0]
                : _meshRenderer.materials[1];

            material.color = color;
        }
    }
}