using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public enum DragTypeObject
    {
        None,
        Mushroom,
        InventorySpore
    }
    public class DragAndDropHolderFSM : MonoBehaviour, IFSMActions
    {


        private State _currentState;
        
        public State CurrentState { get { return _currentState; } }
        [SerializeField]
        private GameObject _canvasDragElement;
        [SerializeField]
        private bool _isDebugMode = true;

        public Vector3 InitialPosition { get; private set; }

        private static DragAndDropHolderFSM _instance;

        public static DragAndDropHolderFSM Instance => _instance;

        public bool IsDragging { get; set; } = false;
        public bool HasDrop { get; set; } = false;
        public bool HasReleased { get; set; } = false;

        public DragElement DraggedElement { get; set; }

        public Vector2 DeltaPosition { get; set; }

        public bool IsDraggingInCanvas { get; set; } = false;
        public GameObject CanvasDraggedElement { get => _canvasDragElement; private set => _canvasDragElement = value; }
        public DragTypeObject ObjectType { get; set; }

        private void Awake()
        {
            _instance = this;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            InitSFM();
        }

        // Update is called once per frame
        void Update()
        {
            
            _currentState.Execute();
            State _nextState = _currentState.GetNextState();
            if (_nextState != null)
            {
                Transition(_nextState);
            }
        }

        public void RegisteredDraggedElement(DragElement draggedObject)
        {
            InitialPosition = draggedObject.transform.position;
            DraggedElement = draggedObject;
            IsDragging = true;
            if (draggedObject.GetComponent<Mushroom>() != null)
            {
                ObjectType = DragTypeObject.Mushroom;

            }
            else if (draggedObject.GetComponent<SporeItem>() != null)
            {
                ObjectType = DragTypeObject.InventorySpore;
            }
            else
            {
                ObjectType = DragTypeObject.None;
            }
        }
        public void UnRegisteredDraggedElement()
        {
            IsDragging = false;
        }
        private void InitSFM()
        {
            _currentState = new IdleState(this);
        }
        private void Transition(State nextState)
        {
            string prevState = _currentState.ToString();

            _currentState.ExitState();
            _currentState = nextState;
            _currentState.EnterState();

            string debugStr = $"### Change state from ({prevState}) to ({_currentState}) for FSM ###";
            if (_isDebugMode) Debug.Log(debugStr);
        }
        public void UpdatePositionOfDraggedElement()
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            DraggedElement.transform.position = new Vector3(pos.x, pos.y, DraggedElement.transform.position.z);
        }
        public void UpdatePositionOfCanvasDraggedElement()
        {
            CanvasDraggedElement.transform.position = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, CanvasDraggedElement.transform.position.z);
        }
        public void SwitchDropMode()
        {
            IsDragging = !IsDragging;
            IsDraggingInCanvas = !IsDraggingInCanvas;
        }
        public void Drop()
        {
            HasDrop = true;
        }
        
    }
}
