using SustainTheStrain.Abilities;
using Zenject;

namespace SustainTheStrain.Buildings
{
    public class BuildingOutline : Outline
    {
        [Inject] private IObservable<SelectionType> _selection;

        private void Start() => _selection.Changed += UpdateSelection;

        protected override void OnDestroy() { base.OnDestroy(); _selection.Changed -= UpdateSelection; }

        private void UpdateSelection(SelectionType selectionType) => enabled = selectionType is SelectionType.Pointer;
    }
}