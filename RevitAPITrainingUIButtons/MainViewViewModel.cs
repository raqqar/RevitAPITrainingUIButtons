using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CreatingButtons;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingUIButtons
{
    
        public class MainViewViewModel
        {
            private ExternalCommandData _commandData;

            public DelegateCommand SelectCommandPipes { get; }
            public DelegateCommand SelectCommandDoors { get; }
            public DelegateCommand SelectCommandVolumeWall { get; }

            public MainViewViewModel(ExternalCommandData commandData)
            {
                _commandData = commandData;
                SelectCommandPipes = new DelegateCommand(OnSelectCommandPipes);
                SelectCommandDoors = new DelegateCommand(OnSelectCommandDoors);
                SelectCommandVolumeWall = new DelegateCommand(OnSelectCommandVolumeWall);
            }

            public event EventHandler HideRequest;

            private void RaiseHideRequest()
            {
                HideRequest?.Invoke(this, EventArgs.Empty);
            }
            public event EventHandler ShowRequest;

            private void RaiseShowRequest()
            {
                ShowRequest?.Invoke(this, EventArgs.Empty);
            }
            private void OnSelectCommandPipes()
            {
                RaiseHideRequest();
                List<Element> el = ElementCollector.GetElement(_commandData, BuiltInCategory.OST_PipeCurves);
                TaskDialog.Show("Количество труб", $"Труб: {el.Count}");
                RaiseShowRequest();
            }
            private void OnSelectCommandDoors()
            {
                RaiseHideRequest();
                List<Element> el = ElementCollector.GetElement(_commandData, BuiltInCategory.OST_Doors);
                TaskDialog.Show("Количество дверей", $"Дверей: {el.Count}");
                RaiseShowRequest();
            }
            private void OnSelectCommandVolumeWall()
            {
                RaiseHideRequest();
                double volume = VolumeWall.Get(_commandData);
                TaskDialog.Show("Объем стен", $"Объем стен: {volume} ");
                RaiseShowRequest();
            }
        }
    
}
