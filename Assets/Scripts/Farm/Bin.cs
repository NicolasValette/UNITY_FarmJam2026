using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmJam2026
{
    public class Bin : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private Animator _animator;


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
                SoundManager.Instance.PlaySFX(ESoundSFX.TrashCan);
            }
            else if (DragAndDropHolderFSM.Instance.ObjectType == DragTypeObject.Mushroom)
            {
                var mush = DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<Mushroom>();
                EventManager.TriggerEvent<Genome>(EventManager.Events.OnTrashMushroom, mush.Genome);
                DragAndDropHolderFSM.Instance.Drop();
                SoundManager.Instance.PlaySFX(ESoundSFX.TrashCan);
            }
            else
                DragAndDropHolderFSM.Instance.Release();

            _animator.SetBool("Isopen", false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (DragAndDropHolderFSM.Instance.IsDragging)
                _animator.SetBool("Isopen", true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _animator.SetBool("Isopen", false);
        }
    }
}
