using System.Collections.Generic;
using System.Linq;
using FarmJam2026.Assets.Scripts.Genetics.Genes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FarmJam2026
{
    public class MutadexColorPage : MonoBehaviour, IDropHandler
    {
        [Header("Main infos")]
        [SerializeField] private Image _mainImage;
        [SerializeField] private Image _mainTextImage;
        [Header("Slots")]
        [SerializeField] private Image _redSlot;
        [SerializeField] private Image _darkerRedSlot;
        [SerializeField] private Image _lighterRedSlot;
        [SerializeField] private Image _purpleSlot;
        [SerializeField] private Image _darkerPurpleSlot;
        [SerializeField] private Image _lighterPurpleSlot;
        [SerializeField] private Image _blueSlot;
        [SerializeField] private Image _darkerBlueSlot;
        [SerializeField] private Image _lighterBlueSlot;
        [SerializeField] private Image _greenSlot;
        [SerializeField] private Image _darkerGreenSlot;
        [SerializeField] private Image _lighterGreenSlot;
        [SerializeField] private Image _yellowSlot;
        [SerializeField] private Image _darkerYellowSlot;
        [SerializeField] private Image _lighterYellowSlot;
        [SerializeField] private Image _orangeSlot;
        [SerializeField] private Image _darkerOrangeSlot;
        [SerializeField] private Image _lighterOrangeSlot;

        private Dictionary<ColorName, GenomeData> _genomeArchive = new Dictionary<ColorName, GenomeData>();

        private EBodyType _primaryType;
        private EBodyType _secondaryType;
        private MushroomVariantData _variantData;

        public void SetMainInfos(EBodyType primary, EBodyType secondary)
        {
            _primaryType = primary;
            _secondaryType = secondary;
            _variantData = MushroomDefinitions.Instance.GetVariationData(primary, secondary);
            _mainImage.sprite = _variantData.MutadexIllustrationSprite;
            _mainTextImage.sprite = _variantData.MutadexTitleSprite;
        }
        private void SetSlotInfo(Image ImageToSet, Color colorToSet, GenomeData genome)
        {
            ImageToSet.sprite = _variantData.MutadexColoredSprite;
            ImageToSet.color = colorToSet;
            ImageToSet.enabled = true;
            if (ImageToSet.gameObject.TryGetComponent<DragElementInCanvas>(out var drag))
                drag.enabled = true;
            var element = ImageToSet.AddComponent<MutadexElement>();
            element.Genome = genome;
            element.Page = this;
        }
        public void RemoveMushroom(Image slotToRemove, GenomeData genome)
        {
            slotToRemove.enabled = false;
            var colorGene = genome.Genes.OfType<ColorGene>().First();
            _genomeArchive.Remove(colorGene.ColorName);
            
        }
        public bool AddMushroom(GenomeData genome)
        {

            var colorGene = genome.Genes.OfType<ColorGene>().First();
            var variantGene = genome.Genes.OfType<VariantGene>().First();
            if (variantGene.PrimaryVariation != _primaryType || variantGene.SecondaryVariation != _secondaryType)
                return false;
            switch (colorGene.ColorName)
            {
                case ColorName.Blue:
                    SetSlotInfo(_blueSlot, colorGene.Color, genome);
                    break;
                case ColorName.DarkBlue:
                    SetSlotInfo(_darkerBlueSlot, colorGene.Color, genome);
                    break;
                case ColorName.LightBlue:
                    SetSlotInfo(_lighterBlueSlot, colorGene.Color, genome);
                    break;
                case ColorName.Red:
                    SetSlotInfo(_redSlot, colorGene.Color, genome);
                    break;
                case ColorName.DarkRed:
                    SetSlotInfo(_darkerRedSlot, colorGene.Color, genome);
                    break;
                case ColorName.LightRed:
                    SetSlotInfo(_lighterRedSlot, colorGene.Color, genome);
                    break;
                case ColorName.Purple:
                    SetSlotInfo(_purpleSlot, colorGene.Color, genome);
                    break;
                case ColorName.DarkPurple:
                    SetSlotInfo(_darkerPurpleSlot, colorGene.Color, genome);
                    break;
                case ColorName.LightPurple:
                    SetSlotInfo(_lighterPurpleSlot, colorGene.Color, genome);
                    break;
                case ColorName.Green:
                    SetSlotInfo(_greenSlot, colorGene.Color, genome);
                    break;
                case ColorName.DarkGreen:
                    SetSlotInfo(_darkerGreenSlot, colorGene.Color, genome);
                    break;
                case ColorName.LightGreen:
                    SetSlotInfo(_lighterGreenSlot, colorGene.Color, genome);
                    break;
                case ColorName.Yellow:
                    SetSlotInfo(_yellowSlot, colorGene.Color, genome);
                    break;
                case ColorName.DarkYellow:
                    SetSlotInfo(_darkerYellowSlot, colorGene.Color, genome);
                    break;
                case ColorName.LightYellow:
                    SetSlotInfo(_lighterYellowSlot, colorGene.Color, genome);
                    break;
                case ColorName.Orange:
                    SetSlotInfo(_orangeSlot, colorGene.Color, genome);
                    break;
                case ColorName.DarkOrange:
                    SetSlotInfo(_darkerOrangeSlot, colorGene.Color, genome);
                    break;
                case ColorName.LightOrange:
                    SetSlotInfo(_lighterOrangeSlot, colorGene.Color, genome);
                    break;

            }
            if (!_genomeArchive.TryAdd(colorGene.ColorName, genome))
            {
                _genomeArchive[colorGene.ColorName] = genome;
            }
            return true;
        }
        public List<GenomeData> GetGenomeList()
        {
            return _genomeArchive.Values.ToList();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var mush = DragAndDropHolderFSM.Instance.DraggedElement.GetComponent<Mushroom>();
            if (mush!= null)
            {
                if (AddMushroom(mush.Genome.GenomeData))
                {
                    DragAndDropHolderFSM.Instance.Drop();
                }
                else
                    DragAndDropHolderFSM.Instance.Release();
            }
            else
                DragAndDropHolderFSM.Instance.Release();
        }
    }
}
