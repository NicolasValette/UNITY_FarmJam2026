using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using static Unity.U2D.Physics.PhysicsBody;

namespace FarmJam2026
{
    public class GeneBook : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pageHolder;
        [SerializeField]
        private GameObject _geneBookGO;
        [SerializeField]
        private TMP_Text _pageText;

        private int _currentPage = 0;

        private Dictionary<BodyType, MutadexColorPage> _geneBookBodyType = new();

        private void OnEnable()
        {
            EventManager.StartListening<GenomeData>(EventManager.Events.OnScienceCollected, ProcessGenome);
            EventManager.StartListening<GenomeData>(EventManager.Events.OnMushroomAdult, CreateTypePage);
        }
        private void OnDisable()
        {
            EventManager.StopListening<GenomeData>(EventManager.Events.OnScienceCollected, ProcessGenome);
            EventManager.StopListening<GenomeData>(EventManager.Events.OnMushroomAdult, CreateTypePage);
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _geneBookGO.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void HideAllPages()
        {
            for (int i = 0; i < _pageHolder.transform.childCount; i++)
            {
                _pageHolder.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        private void ShowPage(int pageNumber)
        {
            HideAllPages();
            if (_currentPage < _pageHolder.transform.childCount)
            {
                _pageHolder.transform.GetChild(pageNumber).gameObject.SetActive(true);
                _pageText.text = $"Page {pageNumber + 1} / {_pageHolder.transform.childCount}";
            }
        }
        public void ToggleGeneBook()
        {
            if (_geneBookGO.activeSelf)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
        public void OpenMenu()
        {
            _geneBookGO.SetActive(true);
            EventManager.TriggerEvent(EventManager.Events.OnUIMenuOpen);
            _currentPage = 0;
            ShowPage(_currentPage);
            SoundManager.Instance.PlaySFX(ESoundSFX.MouseClick);
        }
        public void CloseMenu()
        {
            _geneBookGO.SetActive(false);
            EventManager.TriggerEvent(EventManager.Events.OnUIMenuClose);
        }
        public void NextPage()
        {
            _currentPage = (_currentPage + 1 >= _pageHolder.transform.childCount)? 0 : _currentPage + 1;
            ShowPage(_currentPage);
        }
        public void PreviousPage()
        {
            _currentPage = (_currentPage - 1 < 0) ? _pageHolder.transform.childCount - 1 : _currentPage - 1;
            ShowPage(_currentPage);
        }
        private void CreateTypePage(GenomeData genome) => CreateTypePage(genome.Genes.OfType<BodyTypeGene>().First());
        private void CreateTypePage(BodyTypeGene bodyType)
        {
            if (!_geneBookBodyType.TryGetValue(bodyType.BodyType, out var page))
            {
                var newPage = Instantiate(PrefabLibrary.Instance.MutadexColorPagePrefab, _pageHolder.transform);
                var mutadexPage = newPage.GetComponent<MutadexColorPage>();
                mutadexPage.SetMainInfos(bodyType.BodyTypeSprite, bodyType.BodyType.ToString());
                _geneBookBodyType.Add(bodyType.BodyType, mutadexPage);
            }
        }
        public void ProcessGenome (GenomeData genome)
        {
            var bodyType = genome.Genes.OfType<BodyTypeGene>().First();
            if (!_geneBookBodyType.TryGetValue(bodyType.BodyType, out var page))
            {
                CreateTypePage(bodyType);
            }
            _geneBookBodyType[bodyType.BodyType].AddMushroom(genome);

        }
    }
}
