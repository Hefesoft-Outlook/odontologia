
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MSBuild.ExtensionPack.NuGet
{
    public class Packager : BaseTask
    {
        private const string PackTaskAction = "pack";

        public string Authors
        {
            get;
            set;
        }

        public ITaskItem[] ContentFiles
        {
            get;
            set;
        }

        public string CopyrightText
        {
            get;
            set;
        }

        public ITaskItem[] Dependencies
        {
            get;
            set;
        }

        [Required]
        public string Description
        {
            get;
            set;
        }

        public ITaskItem[] FrameworkAssemblies
        {
            get;
            set;
        }

        public string IconUrl
        {
            get;
            set;
        }

        [Required]
        public string Id
        {
            get;
            set;
        }

        public ITaskItem[] LibraryFiles
        {
            get;
            set;
        }

        public ITaskItem[] PrincipalDlls
        {
            get;
            set;
        }

        [Required]
        public string LicenseUrl
        {
            get;
            set;
        }

        [Required]
        public string OutputFile
        {
            get;
            set;
        }

        public string Owners
        {
            get;
            set;
        }

        [Required]
        public string ProjectUrl
        {
            get;
            set;
        }

        public ITaskItem[] References
        {
            get;
            set;
        }

        public string ReleaseNotes
        {
            get;
            set;
        }

        public bool RequiresExplicitLicensing
        {
            get;
            set;
        }

        public string Tags
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public ITaskItem[] ToolsFiles
        {
            get;
            set;
        }

        [Required]
        public string Version
        {
            get;
            set;
        }

        public Packager()
        {
            this.RequiresExplicitLicensing = false;
        }

        private static XElement GenerateDependencyElement(XNamespace defaultNamespace, ITaskItem taskItem)
        {
            XElement dependency = new XElement(defaultNamespace + "dependency");
            dependency.Add(new XAttribute("id", taskItem.ItemSpec));
            string version = taskItem.GetMetadata("version");
            if (!string.IsNullOrWhiteSpace(version))
            {
                if (!Packager.IsValidVersionNumber(version))
                {
                    CultureInfo currentCulture = CultureInfo.CurrentCulture;
                    object[] itemSpec = new object[] { version, taskItem.ItemSpec };
                    throw new Exception(string.Format(currentCulture, "Invalid version {0} specified for dependency {1}", itemSpec));
                }
                dependency.Add(new XAttribute("version", version));
            }
            return dependency;
        }

        private static XElement GenerateFrameworkAssemblyXElement(XNamespace defaultNamespace, ITaskItem taskItem)
        {
            XElement frameworkAssembly = new XElement(defaultNamespace + "frameworkAssembly ");
            frameworkAssembly.Add(new XAttribute("assemblyName", taskItem.ItemSpec));
            if (!string.IsNullOrWhiteSpace(taskItem.GetMetadata("version")))
            {
                frameworkAssembly.Add(new XAttribute("version", taskItem.GetMetadata("version")));
            }
            return frameworkAssembly;
        }

        private string GenerateSpecification(string directoryPath)
        {
            string specFileName = Path.Combine(directoryPath, string.Concat(this.Id, ".nuspec"));
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            object[] objArray = new object[] { specFileName };
            base.LogTaskMessage(MessageImportance.Low, string.Format(currentCulture, "Generating NuGet specification file {0}", objArray));
            XNamespace xNamespace = "http://schemas.microsoft.com/packaging/2010/07/nuspec.xsd";
            XElement specXml = new XElement(xNamespace + "package");
            XName xName = xNamespace + "metadata";
            object[] xElement = new object[] { new XElement(xNamespace + "id", this.Id), new XElement(xNamespace + "version", this.Version), new XElement(xNamespace + "authors", this.Authors), new XElement(xNamespace + "owners", this.Owners), new XElement(xNamespace + "licenseUrl", this.LicenseUrl), new XElement(xNamespace + "projectUrl", this.ProjectUrl), null, null, null };
            XName xName1 = xNamespace + "requireLicenseAcceptance";
            bool requiresExplicitLicensing = this.RequiresExplicitLicensing;
            xElement[6] = new XElement(xName1, requiresExplicitLicensing.ToString().ToLower(CultureInfo.CurrentCulture));
            xElement[7] = new XElement(xNamespace + "description", this.Description);
            xElement[8] = new XElement(xNamespace + "tags", this.Tags);
            specXml.Add(new XElement(xName, xElement));
            if (this.Dependencies != null)
            {
                specXml.Element(xNamespace + "metadata").Add(new XElement(xNamespace + "dependencies",
                    from e in (IEnumerable<ITaskItem>)this.Dependencies
                    select Packager.GenerateDependencyElement(xNamespace, e) into dependency
                    select dependency));
            }
            if (this.References != null)
            {
                specXml.Element(xNamespace + "metadata").Add(new XElement(xNamespace + "references", ((IEnumerable<ITaskItem>)this.References).Select<ITaskItem, XElement>((ITaskItem e) =>
                {
                    XElement xElement1 = new XElement(xNamespace + "reference");
                    xElement1.Add(new XAttribute("file", e.ItemSpec));
                    return xElement1;
                }).Select<XElement, XElement>((XElement reference) => reference)));
            }
            if (this.FrameworkAssemblies != null)
            {
                specXml.Element(xNamespace + "metadata").Add(new XElement(xNamespace + "frameworkAssemblies",
                    from e in (IEnumerable<ITaskItem>)this.FrameworkAssemblies
                    select Packager.GenerateFrameworkAssemblyXElement(xNamespace, e) into frameworkAssemblies
                    select frameworkAssemblies));
            }
            specXml.Save(specFileName);
            return specFileName;
        }

        protected override void InternalExecute()
        {
            string lower = this.TaskAction.ToLower(CultureInfo.CurrentCulture);
            if (lower != null && lower == "pack")
            {
                this.Pack();
                return;
            }
            TaskLoggingHelper log = base.Log;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            object[] taskAction = new object[] { this.TaskAction };
            log.LogError(string.Format(currentCulture, "Invalid Task Action passed: {0}", taskAction), new object[0]);
        }

        private static bool IsValidVersionNumber(string version)
        {
            return (new Regex("\\d+(?:\\.\\d+)+")).Match(version).Success;
        }

        private void Pack()
        {
            if (!Packager.IsValidVersionNumber(this.Version))
            {
                TaskLoggingHelper log = base.Log;
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                object[] version = new object[] { this.Version };
                log.LogError(string.Format(currentCulture, "Invalid version number {0}. Examples of valid version numbers are 1.0, 1.2.3. 2.3.1909.7", version), new object[0]);
                return;
            }
            
            string nugetDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(nugetDirectory);
            try
            {
                string nugetspecification = this.GenerateSpecification(nugetDirectory);
                Packager.PopulateFolder("lib", nugetDirectory, this.PrincipalDlls);
                Packager.PopulateFolder("lib\\" + Id, nugetDirectory, this.LibraryFiles);
                Packager.PopulateFolder("content", nugetDirectory, this.ContentFiles);
                Packager.PopulateFolder("tools", nugetDirectory, this.LibraryFiles);                
                this.PreparePackage(nugetspecification);
            }
            finally
            {
                Directory.Delete(nugetDirectory, true);
            }
        }

        private static void PopulateFolder(string folderName, string packageDirectoryPath, IEnumerable<ITaskItem> items)
        {            
            if (items == null)
            {
                return;
            }
            DirectoryInfo libDirectory = Directory.CreateDirectory(Path.Combine(packageDirectoryPath, folderName));
            foreach (ITaskItem item in items)
            {
                string folder = item.GetMetadata("RelativeDir").ToLower().Replace("bin\\debug\\", "").Replace("bin\\release\\", "");
                
                if (string.IsNullOrWhiteSpace(folder))
                {
                    File.Copy(item.ItemSpec, Path.Combine(libDirectory.FullName, Path.GetFileName(item.ItemSpec)));
                }
                else
                {
                    folder = Path.Combine(libDirectory.FullName, folder);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    File.Copy(item.ItemSpec, Path.Combine(folder, Path.GetFileName(item.ItemSpec)));
                }
            }
        }
        

        private void PreparePackage(string nugetSpecificationFile)
        {
            string executionDirectory = Path.GetDirectoryName(nugetSpecificationFile);
            string nugetFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\Nuget.exe");
            ProcessStartInfo processStartInfo = new ProcessStartInfo()
            {
                Arguments = string.Concat("pack ", nugetSpecificationFile),
                WorkingDirectory = executionDirectory,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                FileName = nugetFilePath
            };
            using (Process process = Process.Start(processStartInfo))
            {
                process.WaitForExit(20000);
                if (process.ExitCode == 0)
                {
                    string[] files = Directory.GetFiles(executionDirectory, string.Concat(this.Id, ".", this.Version, "*.nupkg"));
                    if (files.Any<string>())
                    {
                        File.Copy(files[0], this.OutputFile, true);
                        CultureInfo currentCulture = CultureInfo.CurrentCulture;
                        object[] outputFile = new object[] { this.OutputFile };
                        base.LogTaskMessage(MessageImportance.Normal, string.Format(currentCulture, "NuGet Package {0} created successfully.", outputFile));
                    }
                }
                else
                {
                    TaskLoggingHelper log = base.Log;
                    CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                    object[] exitCode = new object[] { process.ExitCode };
                    log.LogError(string.Format(cultureInfo, "Nuget Package could not be created. Exit Code: {0}.", exitCode), new object[0]);
                }
            }
        }
    }
}