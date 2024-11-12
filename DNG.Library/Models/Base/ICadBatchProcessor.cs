using System.Collections.Generic;

namespace SolidWorksAutomation
{
    /// <summary>
    /// Interface to define batch operations for optimizing CAD efficiency and structuring files.
    /// Prioritization is based on impact, from quick organizational wins to more complex operations.
    /// </summary>
    public interface ICadBatchProcessor
    {
        /// <summary>
        /// Cleans up file names, removes duplicates, and organizes CAD files in a standard directory structure.
        /// Impact: High - Establishes a structured environment, reduces errors in locating resources.
        /// </summary>
        /// <param name="directoryPath">Root path of CAD library to organize.</param>
        void OrganizeCadLibrary(string directoryPath);

        /// <summary>
        /// Adds standard properties to all SolidWorks files in a directory, ensuring files contain metadata such as project, revision, and author.
        /// Impact: High - Improves searchability, traceability, and compliance with company standards.
        /// </summary>
        /// <param name="filePaths">List of file paths to update.</param>
        void BatchAddStandardProperties(List<string> filePaths);

        /// <summary>
        /// Converts DWG and AutoCAD block files to SolidWorks drawings, preserving layers and annotations where possible.
        /// Impact: High - Integrates legacy data into SolidWorks environment for consistency and reusability.
        /// </summary>
        /// <param name="dwgFilePaths">List of DWG files to convert.</param>
        /// <param name="outputDirectory">Directory to save converted SolidWorks files.</param>
        void ConvertDwgToSolidWorks(List<string> dwgFilePaths, string outputDirectory);

        /// <summary>
        /// Updates all drawing templates in a directory, replacing old templates with updated versions.
        /// Impact: Medium - Maintains consistency across drawings, aligns old files with current standards.
        /// </summary>
        /// <param name="templatePath">Path to the new template file.</param>
        /// <param name="directoryPath">Directory of drawings to update.</param>
        void BatchUpdateDrawingTemplates(string templatePath, string directoryPath);

        /// <summary>
        /// Generates PDFs for all SolidWorks drawing files in a directory.
        /// Impact: Medium - Quickly creates customer deliverables, aligns digital and physical formats.
        /// </summary>
        /// <param name="directoryPath">Directory containing drawing files.</param>
        /// <param name="outputDirectory">Directory to save generated PDFs.</param>
        void BatchGeneratePdfDrawings(string directoryPath, string outputDirectory);

        /// <summary>
        /// Extracts and reports bill of materials (BOM) data for all assemblies in a directory, saving BOM data to a CSV or JSON format.
        /// Impact: Medium - Provides quick access to part requirements, facilitates procurement and manufacturing.
        /// </summary>
        /// <param name="assemblyPaths">List of assembly files to process.</param>
        /// <param name="outputFile">File path to save BOM data.</param>
        void ExtractBomData(List<string> assemblyPaths, string outputFile);

        /// <summary>
        /// Checks for broken references across all assemblies and parts in a directory, identifying missing files and listing them for correction.
        /// Impact: Medium - Ensures drawings and assemblies load correctly, reduces manual troubleshooting.
        /// </summary>
        /// <param name="directoryPath">Directory of files to check for broken references.</param>
        void BatchCheckBrokenReferences(string directoryPath);

        /// <summary>
        /// Applies standard part configurations, such as suppressing unnecessary features or simplifying parts, to optimize performance.
        /// Impact: Medium - Reduces loading times for complex assemblies, standardizes configurations.
        /// </summary>
        /// <param name="partFilePaths">List of part files to configure.</param>
        void BatchApplyPartConfigurations(List<string> partFilePaths);

        /// <summary>
        /// Renames files according to a naming convention, ensuring standardization for future automation and searchability.
        /// Impact: Low - Improves consistency in file naming for batch operations and automation.
        /// </summary>
        /// <param name="directoryPath">Directory containing files to rename.</param>
        void BatchRenameFiles(string directoryPath);
    }
}