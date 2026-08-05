using UnityEngine;

namespace FarmJam2026
{
    public interface IFSMActions
    {
        bool IsDragging { get; set; }
        bool HasDrop { get; set; }
        bool HasReleased { get; set; }
        Vector3 InitialPosition { get; }
        bool IsDraggingInCanvas { get; set; }
        DragElement DraggedElement { get; set; }
        GameObject CanvasDraggedElement { get; }
        Vector2 DeltaPosition { get; set; }
        DragTypeObject ObjectType { get; set; }

        void UpdatePositionOfDraggedElement();
        void UpdatePositionOfCanvasDraggedElement();
    }
}
