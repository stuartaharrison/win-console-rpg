using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using GWConsole.Models;

namespace GWConsole.Controllers {

    public static class MapController {

        public static readonly int MapPaddingTop = 2;
        public static readonly int MapPaddingLeft = 0;
        public static readonly int MapWallPaddingX = 2;
        public static readonly int MapWallPaddingY = 2;

        public static Map GetMap(int mapId) {
            string xmlDocumentAssemblyPath = MapController.GetMapXmlDocumentString(mapId);
            if (String.IsNullOrWhiteSpace(xmlDocumentAssemblyPath)) {
                return null;
            }

            Assembly a = Assembly.GetExecutingAssembly();
            XmlDocument doc = new XmlDocument();

            try {
                doc.Load(a.GetManifestResourceStream(xmlDocumentAssemblyPath));
                XmlNode baseNode = doc.DocumentElement.SelectSingleNode("/map/base");
                // Use the base node to generate the base object
                int id = int.Parse(baseNode.SelectSingleNode("mapid").InnerText);
                string title = baseNode.SelectSingleNode("title").InnerText;
                int width = int.Parse(baseNode.SelectSingleNode("mapwidth").InnerText);
                int height = int.Parse(baseNode.SelectSingleNode("mapheight").InnerText);

                var theMap = new Map(id, title, width, height);
                // TODO: extend to add doors, monsters, player location, keys, items, etc
                var x = baseNode.SelectSingleNode("playstartx");
                var y = baseNode.SelectSingleNode("playstarty");
                if (x != null && y != null) {
                    theMap.PlayerStartX = int.Parse(x.InnerText);
                    theMap.PlayerStartY = int.Parse(y.InnerText);
                }

                return theMap;
            }
            catch {
                // TODO: handle the exception better in the future
                return null;
            }
        }

        public static MapBoundary GetMapBoundary(Map theMap) {
            int startX = MapPaddingLeft; // Where we begin to draw the map (inc. walls)
            int startY = MapPaddingTop; // Where we begin to draw the map (inc. walls)
            // Get the Map Width + Height totals including Walls
            int endX = theMap.Width + (MapWallPaddingX + MapPaddingLeft);
            int endY = theMap.Height + (MapWallPaddingY + MapPaddingTop);
            int trueMapWidth = theMap.Width + MapPaddingLeft;
            int trueMapHeight = theMap.Height + MapPaddingTop;
            // Build the boundary object!
            return new MapBoundary(startX, startY, endX, endY, theMap.Width, theMap.Height, trueMapWidth, trueMapHeight);
        }

        private static string GetMapXmlDocumentString(int mapId) {
            // PUT NEW MAP XML LAYOUTS HERE!
            // Ensure all the Map files are roughly in the same place
            switch (mapId) {
                case 0:
                    return "GWConsole.Content.Maps.Map_TestAreaA.xml";
            }
            return string.Empty;
        }
    }
}
