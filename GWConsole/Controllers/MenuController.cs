using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using GWConsole.Models;

namespace GWConsole.Controllers {

    public static class MenuController {

        public static MenuOption[] GetMenu(int menuId) {
            string xmlDocumentAssemblyPath = MenuController.GetMenuXmlDocumentString(menuId);
            if (String.IsNullOrWhiteSpace(xmlDocumentAssemblyPath)) {
                return null;
            }

            Assembly a = Assembly.GetExecutingAssembly();
            XmlDocument doc = new XmlDocument();

            try {
                doc.Load(a.GetManifestResourceStream(xmlDocumentAssemblyPath));
                // We will just assume that the xml document for menus are consistently the same
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/menu/menuitem");
                var menuOptions = new List<MenuOption>();

                foreach (XmlNode node in nodes) {
                    int exitCode;
                    // Try avoid nested TRY/Catch?
                    if (int.TryParse(node.SelectSingleNode("exitcode").InnerText, out exitCode)) {
                        string display = node.SelectSingleNode("display").InnerText;
                        string exitMessage = node.SelectSingleNode("exitmessage").InnerText;

                        menuOptions.Add(new MenuOption(display, exitMessage, exitCode));
                    }
                    else {
                        // Ignore the invalid nodes?
                        continue;
                    }
                }

                return menuOptions.ToArray();
            }
            catch {
                // May want to handle the catch better in the future?
                return null;
            }
        }

        private static string GetMenuXmlDocumentString(int menuId) {
            // PUT NEW MENU XML LAYOUTS HERE!
            // Ensure all menu files are roughly placed in the same folder to avoid confusion
            switch (menuId) {
                case 0:
                    return "GWConsole.Content.Menus.Menu_MainMenu.xml";
                case 1:
                    return "GWConsole.Content.Menus.Menu_GameMenu.xml";
                case 2:
                    return "GWConsole.Content.Menus.Menu_OptionsMenu.xml";
            }
            return string.Empty;
        }
    }
}
