using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Drawing;

namespace export3dviews
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Class1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            //Get application and documnet objects
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;
                        
            //Navisworks export settings
            NavisworksExportOptions opt = new NavisworksExportOptions
            {
                ExportScope = NavisworksExportScope.View,
                Coordinates = NavisworksCoordinates.Internal,
                DivideFileIntoLevels = true,
                ExportUrls = false,
                ExportRoomGeometry = false,
                ConvertElementProperties = true,
                ExportElementIds = true,
                ExportLinks = false
            };

            //Initiates Radio buttons form for choosing export coordinates
            var f = new Export_3d_views.Select_Settings();
            f.ShowDialog();

            //sets coordinates setting based on buttons in form
            if (f.rdoChecked == "shared")
            {
                opt.Coordinates = NavisworksCoordinates.Shared;
                TaskDialog.Show("Coordinates", "Using" + " " + opt.Coordinates.ToString() + " " + "Coordinates");
            }
            else
            {
                TaskDialog.Show("Coordinates", "Using" + " " + opt.Coordinates.ToString() + " " + "Coordinates");
            }

            //sets Export linked files setting based on buttons in form
            if (f.LrdoChecked == "yes")
            {
                opt.ExportLinks = true;
                TaskDialog.Show("Linked Files", "Linked files will be exported");
            }
            else
            {
                TaskDialog.Show("Linked Files", "Linked files will NOT be exported");
            }

            //collect #d views in revit
            FilteredElementCollector viewCollector = new FilteredElementCollector(doc);
            viewCollector.OfClass(typeof(View3D));

            //list of views to send to the from
            List<string> listViewNames = new List<string>();
            foreach (View3D view in viewCollector)
            {
                listViewNames.Add(view.Name);
            }

            //initate list of views form
            var l = new Export_3d_views.ViewList(listViewNames);
            l.ShowDialog();


            //select save folder
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }

            //list of views exported
            List<string> threeDlist = new List<string>();

            //check if revit view is in list and export to NWC if it is
            if (l.ExportViewList.Count > 0)
            {

                foreach (View3D view in viewCollector)
                {
                    foreach (var item in l.ExportViewList)
                        if (view.Name == item)
                        {
                            opt.ViewId = view.Id;
                            threeDlist.Add(view.Name);
                            doc.Export(folderPath, view.Name + ".nwc", opt);
                        }
                }
            }
            else
            {
                TaskDialog.Show("view error", "Must select one view for export");
                l.ShowDialog();
            }

            //Display form listing views that were exported
            var d = new Export_3d_views.ExportedList(threeDlist);
            d.ShowDialog();

            return Result.Succeeded;
        }
    }
}