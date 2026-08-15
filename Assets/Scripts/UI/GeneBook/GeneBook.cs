using FarmJam2026.Assets.Scripts.Genetics.Genes;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


namespace FarmJam2026
{
    public class GeneBook : MonoBehaviour, ISaveable
    {
        [SerializeField]
        private GameObject _pageHolder;
        [SerializeField]
        private GameObject _geneBookGO;
        [SerializeField]
        private TMP_Text _pageText;
        [SerializeField]
        private GameObject _helpButton;

        private int _currentPage = 0;

        /// <summary>
        /// This is a double entry array!
        /// Variant xy is at index x*ENUM_COUNT+y
        /// </summary>
        private Dictionary<int, MutadexColorPage> _geneBookVariant = new();
        /// <summary>
        /// Dictionnary variant ID <-> page number
        /// </summary>
        private Dictionary<int, int> _pageNumberDictionary = new();
        [SerializeField]
        private MessageBox _messageBox;

        public string Name => "Gene Book";
        private bool HasAlreadyCompltePage = false;
        private void OnEnable()
        {
            EventManager.StartListening<GenomeData>(EventManager.Events.OnScienceCollected, ProcessGenome);
            EventManager.StartListening<GenomeData>(EventManager.Events.OnMushroomAdult, CreateTypePage);

            if (SaveGame.Instance!= null)
                SaveGame.Instance.RegisterSaveable(this);
        }
        private void OnDisable()
        {
            EventManager.StopListening<GenomeData>(EventManager.Events.OnScienceCollected, ProcessGenome);
            EventManager.StopListening<GenomeData>(EventManager.Events.OnMushroomAdult, CreateTypePage);

            if (SaveGame.Instance != null)
                SaveGame.Instance.UnregisterSaveable(this);
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _geneBookGO.SetActive(false);
            _pageText.text = "";
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

        private void PerformOpen(int pageindex)
        {

            _geneBookGO.SetActive(true);
            _helpButton.SetActive(false);
            EventManager.TriggerEvent(EventManager.Events.OnUIMenuOpen);
            _currentPage = pageindex;
            ShowPage(_currentPage);
            SoundManager.Instance.PlaySFX(ESoundSFX.BookOpen);
        }
        public void OpenMenu()
        {
            PerformOpen(0);
        }
        public void OpenMenuOnSpecificPage(GenomeData genome)
        {
            var variant = genome.Genes.OfType<VariantGene>().First();
            var variantID = (int)variant.PrimaryVariation * (int)EBodyType.ENUM_COUNT + (int)variant.SecondaryVariation;
            PerformOpen(_pageNumberDictionary[variantID]);
        }

        public void CloseMenu()
        {
            _geneBookGO.SetActive(false);
            _helpButton.SetActive(true);
            SoundManager.Instance.PlaySFX(ESoundSFX.BookClose);
            EventManager.TriggerEvent(EventManager.Events.OnUIMenuClose);
        }

        public void NextPage()
        {
            _currentPage = (_currentPage + 1 >= _pageHolder.transform.childCount)? 0 : _currentPage + 1;
            ShowPage(_currentPage);
            SoundManager.Instance.PlaySFX(ESoundSFX.BookFlip);
        }
        public void PreviousPage()
        {
            _currentPage = (_currentPage - 1 < 0) ? _pageHolder.transform.childCount - 1 : _currentPage - 1;
            ShowPage(_currentPage);
            SoundManager.Instance.PlaySFX(ESoundSFX.BookFlip);
        }
        private void CreateTypePage(GenomeData genome) => CreateTypePage(genome.Genes.OfType<VariantGene>().First());
        private void CreateTypePage(VariantGene variant)
        {
            var variantIdx = (int)variant.PrimaryVariation * (int)EBodyType.ENUM_COUNT + (int)variant.SecondaryVariation;
            if (!_geneBookVariant.TryGetValue(variantIdx, out var page))
            {
                int pageNumber = _pageHolder.transform.childCount;
                var newPage = Instantiate(PrefabLibrary.Instance.MutadexColorPagePrefab, _pageHolder.transform);
                var mutadexPage = newPage.GetComponent<MutadexColorPage>();
                mutadexPage.OnCompletePage = CompletePage;
                mutadexPage.SetMainInfos(variant.PrimaryVariation, variant.SecondaryVariation);
                _geneBookVariant.Add(variantIdx, mutadexPage);
                _pageNumberDictionary.Add(variantIdx, pageNumber);
            }
        }
        public void ProcessGenome(GenomeData genome)
        {
            var variant = genome.Genes.OfType<VariantGene>().First();
            var variantIdx = (int)variant.PrimaryVariation * (int)EBodyType.ENUM_COUNT + (int)variant.SecondaryVariation;
            if (!_geneBookVariant.TryGetValue(variantIdx, out var page))
            {
                CreateTypePage(variant);
            }
            _geneBookVariant[variantIdx].AddMushroom(genome);
            
        }
        public void CompletePage()
        {
            if (!HasAlreadyCompltePage)
            {
                HasAlreadyCompltePage = true;
                if (_messageBox!= null)
                    _messageBox.DisplayMessageBox("Congratulations!!\nYou completed one Mutadex Page! You can keep playing to complete all the pages!\nThanks for playing our game!");
                else
                {
                    Debug.LogWarning("Missing reference for message box in Gene Book", this.gameObject);
                }
            }
        }

        public void Save(ref SaveData data)
        {
            foreach (var item in _geneBookVariant)
            {
                data.ListMutadexPages.Add(new MutadexPage { ListMutadexPages = new(item.Value.GetGenomeList()) });
            }
            Debug.Log("[SAVE] MUTADEX SAVED !");
        }

        public void Load(SaveData data)
        {
            foreach (var item in data.ListMutadexPages)
            {
                if (item.ListMutadexPages.Count > 0)
                {
                    CreateTypePage(item.ListMutadexPages[0]);
                    foreach (var genome in item.ListMutadexPages)
                    {
                        ProcessGenome(genome);
                    }
                }
            }
            Debug.Log("[LOAD] MUTADEX LOADED !");
        }
    }
}
