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
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Layouts;
using ArcGIS.Desktop.Mapping;

namespace MapLayout
{
    internal class BuildNewLayout : Button
    {
        protected async override void OnClick()
        {

            // *** First step - Create a new layout and CIM page and apply it to the layout:

            // Create a layout which will be returned from the QueuedTask
            Layout newLayout = await QueuedTask.Run<Layout>(() =>
            {
                // Create a new CIM page
                CIMPage newPage = new CIMPage();

                // Add properties
                newPage.Width = 17;
                newPage.Height = 11;
                newPage.Units = LinearUnit.Inches;

                // Add rulers
                newPage.ShowRulers = true;
                newPage.SmallestRulerDivision = 0.5;

                // Apply the CIM page to a new layout and set name
                newLayout = LayoutFactory.Instance.CreateLayout(newPage);
                newLayout.SetName("Census Data");


                // *** Next Step - Create a map with census data, add it to the layout and set the camera:

                // Create a new map with an ArcGIS Online basemap
                Map newMap = MapFactory.Instance.CreateMap("Census Map", MapType.Map, MapViewingMode.Map, Basemap.NationalGeographic);
                string url = @"http://sampleserver1.arcgisonline.com/ArcGIS/rest/services/Demographics/ESRI_Census_USA/MapServer";
                Uri uri = new Uri(url);
                LayerFactory.Instance.CreateLayer(uri, newMap);

                // Build a map frame geometry / envelope
                Coordinate2D ll = new Coordinate2D(1, 0.5);
                Coordinate2D ur = new Coordinate2D(13, 9);
                Envelope mapEnv = EnvelopeBuilderEx.CreateEnvelope(ll, ur);

                // Create a map frame and add it to the layout
                MapFrame newMapframe = ElementFactory.Instance.CreateMapFrameElement(newLayout, mapEnv, newMap);
                newMapframe.SetName("Map Frame");

                // Create and set the camera
                Camera camera = newMapframe.Camera;
                camera.X = -118.465;
                camera.Y = 33.988;
                camera.Scale = 30000;
                newMapframe.SetCamera(camera);


                // *** Next Step - Create title and north arrow:

                // Add text for title
                Coordinate2D titleTxt_ll = new Coordinate2D(4.5, 9.5);
                CIMTextSymbol arial36bold = SymbolFactory.Instance.ConstructTextSymbol(ColorFactory.Instance.BlueRGB, 80, "Arial", "Bold");
                GraphicElement titleTxtElm = ElementFactory.Instance.CreateTextGraphicElement
                    (newLayout, TextType.PointText, titleTxt_ll.ToMapPoint(), arial36bold, "Census Data");
                titleTxtElm.SetName("Title");


                // Add north arrow
                // Reference a North Arrow in a style
                StyleProjectItem stylePrjItm = Project.Current.GetItems<StyleProjectItem>().FirstOrDefault(item => item.Name == "ArcGIS 2D");
                NorthArrowStyleItem naStyleItm = stylePrjItm.SearchNorthArrows("ArcGIS North 8")[0];
                // Set the center coordinate and create the arrow on the layout
                Coordinate2D center = new Coordinate2D(15, 7);
                var naInfo = new NorthArrowInfo() { MapFrameName = newMapframe.Name, NorthArrowStyleItem = naStyleItm };
                var arrowElm = ElementFactory.Instance.CreateMapSurroundElement(newLayout, center.ToMapPoint(), naInfo);
                arrowElm.SetName("New North Arrow");
                arrowElm.SetHeight(2);


                // *** Final Step - Create legend and open layout pane:

                // Add legend - first build 2D envelope geometry
                Coordinate2D leg_ll = new Coordinate2D(13.5, 0.5);
                Coordinate2D leg_ur = new Coordinate2D(16.5, 4.0);
                Envelope leg_env = EnvelopeBuilderEx.CreateEnvelope(leg_ll, leg_ur);
                // Create legend
                var legInfo = new LegendInfo() { MapFrameName = newMapframe.Name };
                var legendElm = ElementFactory.Instance.CreateMapSurroundElement(newLayout, leg_env, legInfo);
                legendElm.SetVisible(true);
                legendElm.SetName("New Legend");



                return newLayout;

            });

            var layoutPane = await ProApp.Panes.CreateLayoutPaneAsync(newLayout);


        }
    }
}
