using FarmJam2026.Assets.Scripts.Genetics.Genes;
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FarmJam2026
{
    public enum DragTypeObject
    {
        None,
        Mushroom,
        InventorySpore,
        SporeFromMutadex
    }
    public class DragAndDropHolderFSM : MonoBehaviour, IFSMActions
    {
        public enum DragType
        {
            Distance,
            Time
        }

        private State _currentState;
        [SerializeField]
        public DragType type = DragType.Distance;
        [field: SerializeField, Range(0f, 0.5f)]
        public float TimeToDrag { get; private set; }
        [field: SerializeField, Range(0f, 500f)]
        public float DistanceToDrag { get; private set; } = 10f;

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
        public DragElementInCanvas DraggedElementinCanvas { get; set; }

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
            var value = type == DragType.Distance ? DistanceToDrag : TimeToDrag;
            Debug.Log("Drag & Drop mode : " + type.ToString() + " / value : " + value);
            InitSFM();
        }

        // Update is called once per frame
        void Update()
        {
            
            _currentState.Execute();
        }
        private void LateUpdate()
        {
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
        public void RegisteredCanvasDraggedElement(DragElementInCanvas draggedObject)
        {
            InitialPosition = draggedObject.transform.position;
            InitialPosition = draggedObject.transform.position;
            DraggedElementinCanvas = draggedObject;
            IsDragging = true;
            IsDraggingInCanvas = true;
            ObjectType = DragTypeObject.SporeFromMutadex;
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
            if (DraggedElement != null)
                DraggedElement.transform.position = new Vector3(pos.x, pos.y, DraggedElement.transform.position.z);
        }
        public void UpdatePositionOfCanvasDraggedElement()
        {
            CanvasDraggedElement.transform.position = new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, CanvasDraggedElement.transform.position.z);
        }
        public void SwitchDropMode()
        {
            //IsDragging = !IsDragging;
            IsDraggingInCanvas = !IsDraggingInCanvas;
        }
        public void Drop()
        {
            HasDrop = true;
        }
        public void Release()
        {
            HasReleased = true;
        }
        public GenomeData GetGenomeData()
        {
            if (ObjectType == DragTypeObject.Mushroom)
            {
                return DraggedElement.GetComponent<Mushroom>().Genome.GenomeData;
            }
            else if (ObjectType == DragTypeObject.InventorySpore)
            {
                return DraggedElement.GetComponent<SporeItem>().Spore.Genome.GenomeData;
            }
            else if (ObjectType == DragTypeObject.SporeFromMutadex)
            {
                return DraggedElementinCanvas.GetComponent<MutadexElement>().Genome;
            }
            return null;
        }
       
    }
}
