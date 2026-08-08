using System.Linq;
using FarmJam2026.Assets.Scripts.Genetics.Genes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FarmJam2026
{
    public class MutadexColorPage : MonoBehaviour, IDropHandler
    {
        [Header("Main infos")]
        [SerializeField] private Image _mainImage;
        [SerializeField] private TMP_Text _mainText;
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

        private MushroomVariantData _variantData;
      
        public void SetMainInfos(MushroomVariantData variantData)
        {
            _mainImage.sprite = variantData.MutadexIllustrationSprite;
            _variantData = variantData;
            _mainText.text = _variantData.ToString();
        }
        private void SetImage(Image ImageToSet, Color colorToSet)
        {
            ImageToSet.sprite = _variantData.MutadexColoredSprite;
            ImageToSet.color = colorToSet;
        }
        public bool AddMushroom(GenomeData genome)
        {
            //TODO: Save genome when added to Mutadex

            var colorGene = genome.Genes.OfType<ColorGene>().First();
            var variantGene = genome.Genes.OfType<VariantGene>().First();
            if (variantGene.VariantData != _variantData)
                return false;
            switch (colorGene.ColorName)
            {
                case ColorName.Blue:
                    SetImage(_blueSlot, colorGene.Color);
                    break;
                case ColorName.DarkBlue:
                    SetImage(_darkerBlueSlot, colorGene.Color);
                    break;
                case ColorName.LightBlue:
                    SetImage(_lighterBlueSlot, colorGene.Color);
                    break;
                case ColorName.Red:
                    SetImage(_redSlot, colorGene.Color);
                    break;
                case ColorName.DarkRed:
                    SetImage(_darkerRedSlot, colorGene.Color);
                    break;
                case ColorName.LightRed:
                    SetImage(_lighterRedSlot, colorGene.Color);
                    break;
                case ColorName.Purple:
                    SetImage(_purpleSlot, colorGene.Color);
                    break;
                case ColorName.DarkPurple:
                    SetImage(_darkerPurpleSlot, colorGene.Color);
                    break;
                case ColorName.LightPurple:
                    SetImage(_lighterPurpleSlot, colorGene.Color);
                    break;
                case ColorName.Green:
                    SetImage(_greenSlot, colorGene.Color);
                    break;
                case ColorName.DarkGreen:
                    SetImage(_darkerGreenSlot, colorGene.Color);
                    break;
                case ColorName.LightGreen:
                    SetImage(_lighterGreenSlot, colorGene.Color);
                    break;
                case ColorName.Yellow:
                    SetImage(_yellowSlot, colorGene.Color);
                    break;
                case ColorName.DarkYellow:
                    SetImage(_darkerYellowSlot, colorGene.Color);
                    break;
                case ColorName.LightYellow:
                    SetImage(_lighterYellowSlot, colorGene.Color);
                    break;
                case ColorName.Orange:
                    SetImage(_orangeSlot, colorGene.Color);
                    break;
                case ColorName.DarkOrange:
                    SetImage(_darkerOrangeSlot, colorGene.Color);
                    break;
                case ColorName.LightOrange:
                    SetImage(_lighterOrangeSlot, colorGene.Color);
                    break;

            }
            return true;
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
