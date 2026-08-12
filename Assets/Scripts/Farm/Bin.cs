using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class Bin : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        [SerializeField]
        private Sprite _binOpenSprite;
        [SerializeField]
        private Sprite _binClosedSprite;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void OnDrop(PointerEventData eventData)
        {


            if (DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.InventorySpore)
            {
                var item = DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<SporeItem>();
                EventManager.TriggerEvent<Genome>(EventManager.Events.OnTrash, item.Spore.Genome);
                DragAndDropHolderFSM.Instance.Drop();
            }
            else if (DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.Mushroom)
            {
                var mush = DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<Mushroom>();
                EventManager.TriggerEvent<Genome>(EventManager.Events.OnTrashMushroom, mush.Genome);
                DragAndDropHolderFSM.Instance.Drop();
            }
            else
                DragAndDropHolderFSM.Instance.Release();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (DragAndDropHolderFSM.Instance.IsDragging)
                _spriteRenderer.sprite = _binOpenSprite;
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _spriteRenderer.sprite = _binClosedSprite;
        }
    }
}
