using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Core.Geoprocessing;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;

namespace RunGPTool
{
    internal class RunGP : Button
    {
        protected override async void OnClick()
        {
            await QueuedTask.Run(async () =>
            {
                // Check for an active mapview, if not, then prompt and exit.
                if (MapView.Active == null)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("No MapView currently active. Exiting...", "Info");
                    return;
                }
                // Get the layer(s) selected in the Contents pane, if there is not just one, then prompt then exit.
                if (MapView.Active.GetSelectedLayers().Count != 1)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("One feature layer must be selected in the Contents pane. Exiting...", "Info");
                    return;
                }
                // Check to see if the selected layer is a feature layer, if not, then prompt and exit.
                var featLayer = MapView.Active.GetSelectedLayers().First() as FeatureLayer;
                if (featLayer == null)
                {
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("A feature layer must be selected in the Contents pane. Exiting...", "Info");
                    return;
                }

                try
                {
                    // Get the path to the layer's feature class and path to a new 200-foot buffer feature class
                    string FLPath = featLayer.GetFeatureClass().GetDatastore().GetPath().AbsolutePath;
                    var FLPathCombine = System.IO.Path.GetFullPath(FLPath);
                    string infc = System.IO.Path.Combine(FLPathCombine, featLayer.Name);
                    string outfc = System.IO.Path.Combine(FLPathCombine, featLayer.Name + "_GP_Buffer_200ft");
                    // Place parameters into an array
                    var parameters = Geoprocessing.MakeValueArray(infc, outfc, "200 Feet");
                    // Place environment settings in an array, in this case, OK to over-write
                    var environments = Geoprocessing.MakeEnvironmentArray(overwriteoutput: true);
                    // Execute the GP tool with parameters
                    var gpResult = await Geoprocessing.ExecuteToolAsync("Buffer_analysis", parameters, environments);
                    // Show a messagebox with the results
                    Geoprocessing.ShowMessageBox(gpResult.Messages, "GP Messages", gpResult.IsFailed ? GPMessageBoxStyle.Error : GPMessageBoxStyle.Default);

                }
                catch (Exception exc)
                {
                    // Catch any exception found and display in a message box
                    ArcGIS.Desktop.Framework.Dialogs.MessageBox.Show("Exception caught while trying to run GP tool: " + exc.Message);
                    return;
                }
            });
        }
    }
}
