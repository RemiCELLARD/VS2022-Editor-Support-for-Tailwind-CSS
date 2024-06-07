using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using System.Runtime.InteropServices;
using TailwindCSSIntellisense.Attributes;

namespace TailwindCSSIntellisense.Options
{
    internal partial class OptionsProvider
    {
        [ComVisible(true)]
        public class GeneralOptions : BaseOptionPage<General> { }

        [ComVisible(true)]
        public class LinterOptions : BaseOptionPage<Linter> { }
    }

    public class General : BaseOptionModel<General>
    {
        [LocalizedCategory("Option_Category_General_Title")]
        [LocalizedDisplayName("Option_Name_Enable_Extension")]
        [Description("Enables or disables the extension features")]
        [DefaultValue(true)]
        public bool UseTailwindCss { get; set; } = true;

        [LocalizedCategory("Option_Category_General_Title")]
        [LocalizedDisplayName("Option_Name_Tailwind_CSS_Completions_Before_All")]
        [Description("True if Tailwind CSS completions come before all others; false if after")]
        [DefaultValue(true)]
        public bool TailwindCompletionsComeFirst { get; set; } = true;

        [LocalizedCategory("Option_Category_General_Title")]
        [LocalizedDisplayName("Option_Name_Automatically_Apply_Lib_Updates")]
        [Description("True if the Tailwind CSS should update on project load; false if not")]
        [DefaultValue(true)]
        public bool AutomaticallyUpdateLibrary { get; set; } = true;

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Minify_Builds")]
        [Description("Determines whether or not the Tailwind build process minifies by default")]
        [DefaultValue(false)]
        public bool AutomaticallyMinify { get; set; } = false;

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Default_Output_File_Name")]
        [Description("Sets the default name of the built Tailwind CSS file; use {0} if you want to reference the content file name")]
        [DefaultValue("{0}.output.css")]
        public string TailwindOutputFileName { get; set; } = "{0}.output.css";

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Tailwind_CLI_Path")]
        [Description("The absolute path to the Tailwind CLI executable for building: if empty, the default npx tailwindcss build command will run; if not, the specified Tailwind CLI will be called")]
        public string TailwindCliPath { get; set; }

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Build_Type")]
        [Description("Files can be built in four ways: Default (Tailwind JIT), OnSave (on file save), OnBuild (on project build), and None (no building)")]
        [TypeConverter(typeof(EnumConverter))]
        [DefaultValue(BuildProcessOptions.Default)]
        public BuildProcessOptions BuildProcessType { get; set; } = BuildProcessOptions.Default;

        [LocalizedCategory("Option_Category_Class_Sort_Title")]
        [LocalizedDisplayName("Option_Name_Class_Sort_Type")]
        [Description("Classes can be sorted manually (with 'Tools' options), on file save (only sorts open file), on build (entire solution), or never.")]
        [TypeConverter(typeof(EnumConverter))]
        [DefaultValue(SortClassesOptions.OnSave)]
        public SortClassesOptions ClassSortType { get; set; } = SortClassesOptions.OnSave;

        [LocalizedCategory("Option_Category_Custom_Build_Title")]
        [LocalizedDisplayName("Option_Name_Build_Script")]
        [Description("The name of the script to execute on build (defined in package.json); leave blank to use the default Tailwind CSS build")]
        public string BuildScript { get; set; }

        [LocalizedCategory("Option_Category_Custom_Build_Title")]
        [LocalizedDisplayName("Option_Name_Override_Build")]
        [Description("Only runs the script defined in \"Build script\" when set to true; both run simultaneously when set to false; only the default Tailwind build will run if the package.json script is not found")]
        [DefaultValue(false)]
        public bool OverrideBuild { get; set; } = false;
    }

    public enum BuildProcessOptions
    {
        Default,
        OnSave,
        OnBuild,
        None
    }

    public enum SortClassesOptions
    {
        OnSave,
        OnBuild,
        Manual,
        None
    }
}
