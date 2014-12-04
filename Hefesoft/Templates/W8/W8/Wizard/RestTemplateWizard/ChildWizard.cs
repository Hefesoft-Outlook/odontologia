using System;
using System.Linq;
using System.Collections.Generic;

using EnvDTE;
using Microsoft.VisualStudio.TemplateWizard;

namespace RestTemplateWizard
{
    // Child wizard is used by child project vstemplates

    public class ChildWizard : IWizard
    {
        // Retrieve global replacement parameters
        public void RunStarted(object automationObject, 
            Dictionary<string, string> replacementsDictionary, 
            WizardRunKind runKind, object[] customParams)
        {

            nombreProyecto(replacementsDictionary);
            nombreSinHefesoft(replacementsDictionary);

            // Add custom parameters.
            replacementsDictionary.Add("$saferootprojectname$",
                RootWizard.GlobalDictionary["$saferootprojectname$"]);

        }

        private static void nombreProyecto(Dictionary<string, string> replacementsDictionary)
        {
            try
            {
                var nombreProyecto = RootWizard.GlobalDictionary["$saferootprojectname$"];
                nombreProyecto = nombreProyecto.Replace(".Locator", "").Replace(".Test", "");
                //Declaramos una variable en la que solo van estar el nombre del proyecto para luego hacer los reemplazos
                replacementsDictionary.Add("$nombreProyecto$", nombreProyecto);
            }
            catch 
            { }
        }

        private static void nombreSinHefesoft(Dictionary<string, string> replacementsDictionary)
        {
            try
            {
                var nombreProyectoSinHefesoft = RootWizard.GlobalDictionary["$saferootprojectname$"];
                nombreProyectoSinHefesoft = nombreProyectoSinHefesoft.Replace(".Locator", "").Replace(".Test", "").Replace(".Hefesoft", "");
                //Declaramos una variable en la que solo van estar el nombre del proyecto para luego hacer los reemplazos
                replacementsDictionary.Add("$nombreProyectoSinHefesoft$", nombreProyectoSinHefesoft);
            }
            catch
            { }
        }

        public void RunFinished()
        {
        }

        public void BeforeOpeningFile(ProjectItem projectItem)
        {
        }

        public void ProjectFinishedGenerating(Project project)
        {
        }

        public bool ShouldAddProjectItem(string filePath)
        {
            return true;
        }

        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }
    }
}
