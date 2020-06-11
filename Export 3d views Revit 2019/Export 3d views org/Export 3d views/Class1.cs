﻿using System;
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
            var f = new Export_3d_views.Select_Coordinates();
            f.ShowDialog();

            //sets coordinates based on buttons in form
            if (f.rdoChecked == "shared")
            {
                opt.Coordinates = NavisworksCoordinates.Shared;
                TaskDialog.Show("Coordinates", "Using" + " " + opt.Coordinates.ToString() + " " + "Coordinates");
            }
            else
            {
                TaskDialog.Show("Coordinates", "Using" + " " + opt.Coordinates.ToString() + " " + "Coordinates");
            }


            //string filePath = @"C:\Users\Derek.Becher\Documents\navis_dynamo_default\test\";
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }

            //list of view to export
            string[] viewList = {"MEP-Fire", "MEP-Plumbing", "MEP-HVAC Mechanical", "MEP-Piping Mechanincal", "MEP-Piping Process",
                "MEP-Future", "MEP-Electrical", "MEP-ARCH_Export", "MEP-Equipment", "MEP-HVAC Process"};

            //collect #d views in revit
            FilteredElementCollector viewCollector = new FilteredElementCollector(doc);
            viewCollector.OfClass(typeof(View3D));

            //list of view exported
            List<string> threeDlist = new List<string>();

            //check if revit view is in list and export to NWC if it is
            foreach (View3D view in viewCollector)
            {
                if (viewList.Contains(view.Name))
                {
                    opt.ViewId = view.Id;
                    threeDlist.Add(view.Name);
                    doc.Export(folderPath, view.Name + ".nwc", opt);
                    //TaskDialog.Show("Exported views", view.Name);
                    //TaskDialog.Show("List has", Convert.ToString(view.Id));
                }
            }

            //Combine all view into string to show user what was exported
            string fullList = "";
            for (int i = 0; i < threeDlist.Count; i++)
            {
                if (i < threeDlist.Count)
                    fullList = fullList + threeDlist[i] + ", ";
                else
                    fullList += threeDlist[i];
            }
            //Display list of exported views
            TaskDialog.Show("Exported views", fullList);

            return Result.Succeeded;
        }
    }
}