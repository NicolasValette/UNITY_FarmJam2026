using UnityEngine;

namespace FarmJam2026
{
    public interface IFSMActions
    {
        bool IsDragging { get; }
        bool IsDraggingInCanvas { get; }
        GameObject DraggedElement { get; set; }
        GameObject CanvasDraggedElement { get; }
        Vector2 DeltaPosition { get; set; }

        void UpdatePositionOfDraggedElement();
        void UpdatePositionOfCanvasDraggedElement();
    }
}
