using UnityEngine;
using Random = UnityEngine.Random;

namespace FarmJam2026
{
    public class Field : MonoBehaviour, IField
    {
        private bool _isCropFull = false;
       
        public void PlantCrop(GenomeData genome)
        {
            if (!_isCropFull)
            {
                var mushroomGO = GameObject.Instantiate(PrefabLibrary.Instance.MushroomPrefab, Vector2.zero, Quaternion.identity, transform);
                mushroomGO.transform.localPosition = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

                var mushroom = mushroomGO.GetComponent<Mushroom>();
                mushroom.Genome = genome;

                _isCropFull = true;
                EventManager.TriggerEvent<GenomeData>(EventManager.Events.OnPlant, genome);
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

    }
}
