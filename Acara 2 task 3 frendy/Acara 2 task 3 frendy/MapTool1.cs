﻿using System;
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

namespace Acara_2_task_3_frendy
{
    internal class MapTool1 : MapTool
    {
        public MapTool1()
        {
            IsSketchTool = true;
            SketchType = SketchGeometryType.Circle;
            SketchOutputMode = SketchOutputMode.Screen;
        }

        protected override Task OnToolActivateAsync(bool active)
        {
            return base.OnToolActivateAsync(active);
        }

        protected override async Task<bool> OnSketchCompleteAsync(Geometry geometry)
        {
            var mv = MapView.Active;
            var identifyResult = await QueuedTask.Run(() =>
            {
                var sb = new StringBuilder();

                // Get the features that intersect the sketch geometry.
                var features = mv.GetFeatures(geometry);

                // Get all layer definitions.
                var lyrs = mv.Map.GetLayersAsFlattenedList().OfType<FeatureLayer>();
                foreach (var lyr in lyrs)
                {
                    var fCnt = features.ContainsKey(lyr) ? features[lyr].Count : 0;
                    sb.AppendLine($@"{fCnt} {(fCnt == 1 ? "record" : "records")} for {lyr.Name}");
                }
                return sb.ToString();
            });
            MessageBox.Show(identifyResult);
            return true;
        }
    }
}
