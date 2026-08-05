using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    public class Field : MonoBehaviour, IField, IDropHandler
    {
        private bool _isCropFull = false;



        public void PlantCrop(Genome genome)
        {
            if (!_isCropFull)
            {
                var mushroomGO = GameObject.Instantiate(PrefabLibrary.Instance.MushroomPrefab, Vector2.zero, Quaternion.identity, transform);
                mushroomGO.transform.localPosition = new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

                var mushroom = mushroomGO.GetComponent<Mushroom>();
                mushroom.Genome = genome;

                _isCropFull = true;
                EventManager.TriggerEvent(EventManager.Events.OnPlant, genome);
            }
            else
            {
                Debug.Log("Field is full");
            }
        }
        /// <summary>
        /// Used when all mushroom in this field decay
        /// </summary>
        public void SetFieldEmpty()
        {
            Debug.Log("Field Empty");
            _isCropFull = false;
        }
        public void OnDrop(PointerEventData eventData)
        {
            var spore = DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<SporeItem>();

            if (spore != null)
            {
                PlantCrop(spore.Spore.Genome);
                
            }
            DragAndDropHolderFSM.Instance.Drop();
        }

    }
}
