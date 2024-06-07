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
        [LocalizedDescription("Option_Description_Enable_Extension")]
        [DefaultValue(true)]
        public bool UseTailwindCss { get; set; } = true;

        [LocalizedCategory("Option_Category_General_Title")]
        [LocalizedDisplayName("Option_Name_Tailwind_CSS_Completions_Before_All")]
        [LocalizedDescription("Option_Description_Tailwind_CSS_Completions_Before_All")]
        [DefaultValue(true)]
        public bool TailwindCompletionsComeFirst { get; set; } = true;

        [LocalizedCategory("Option_Category_General_Title")]
        [LocalizedDisplayName("Option_Name_Automatically_Apply_Lib_Updates")]
        [LocalizedDescription("Option_Description_Automatically_Apply_Lib_Updates")]
        [DefaultValue(true)]
        public bool AutomaticallyUpdateLibrary { get; set; } = true;

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Minify_Builds")]
        [LocalizedDescription("Option_Description_Minify_Builds")]
        [DefaultValue(false)]
        public bool AutomaticallyMinify { get; set; } = false;

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Default_Output_File_Name")]
        [LocalizedDescription("Option_Description_Default_Output_File_Name")]
        [DefaultValue("{0}.output.css")]
        public string TailwindOutputFileName { get; set; } = "{0}.output.css";

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Tailwind_CLI_Path")]
        [LocalizedDescription("Option_Description_Tailwind_CLI_Path")]
        public string TailwindCliPath { get; set; }

        [LocalizedCategory("Option_Category_Build_Title")]
        [LocalizedDisplayName("Option_Name_Build_Type")]
        [LocalizedDescription("Option_Description_Build_Type")]
        [TypeConverter(typeof(EnumConverter))]
        [DefaultValue(BuildProcessOptions.Default)]
        public BuildProcessOptions BuildProcessType { get; set; } = BuildProcessOptions.Default;

        [LocalizedCategory("Option_Category_Class_Sort_Title")]
        [LocalizedDisplayName("Option_Name_Class_Sort_Type")]
        [LocalizedDescription("Option_Description_Class_Sort_Type")]
        [TypeConverter(typeof(EnumConverter))]
        [DefaultValue(SortClassesOptions.OnSave)]
        public SortClassesOptions ClassSortType { get; set; } = SortClassesOptions.OnSave;

        [LocalizedCategory("Option_Category_Custom_Build_Title")]
        [LocalizedDisplayName("Option_Name_Build_Script")]
        [LocalizedDescription("Option_Description_Build_Script")]
        public string BuildScript { get; set; }

        [LocalizedCategory("Option_Category_Custom_Build_Title")]
        [LocalizedDisplayName("Option_Name_Override_Build")]
        [LocalizedDescription("Option_Description_Override_Build")]
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
