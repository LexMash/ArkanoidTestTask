using UnityEngine;

namespace LevelEditor
{
    public class PositionToCellConverter : MonoBehaviour
    {
        [SerializeField] private Grid _grid;
        [SerializeField] private Camera _camera;

        public Vector3 GetCellCoordinates(Vector3 position)
        {
            Vector3 worldPosition = _camera.ScreenToWorldPoint(position);
            Vector3Int cellPosition = _grid.WorldToCell(worldPosition);

            return _grid.GetCellCenterWorld(cellPosition);
        }
    }
}
