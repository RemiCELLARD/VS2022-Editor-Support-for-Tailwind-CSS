﻿using Community.VisualStudio.Toolkit;
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
        [LocalizedCategory("Category_General_Title")]
        [DisplayName("Enable extension")]
        [Description("Enables or disables the extension features")]
        [DefaultValue(true)]
        public bool UseTailwindCss { get; set; } = true;

        [LocalizedCategory("Category_General_Title")]
        [DisplayName("Tailwind CSS completions before all")]
        [Description("True if Tailwind CSS completions come before all others; false if after")]
        [DefaultValue(true)]
        public bool TailwindCompletionsComeFirst { get; set; } = true;

        [LocalizedCategory("Category_General_Title")]
        [DisplayName("Automatically apply library updates")]
        [Description("True if the Tailwind CSS should update on project load; false if not")]
        [DefaultValue(true)]
        public bool AutomaticallyUpdateLibrary { get; set; } = true;

        [LocalizedCategory("Category_Build_Title")]
        [DisplayName("Minify builds")]
        [Description("Determines whether or not the Tailwind build process minifies by default")]
        [DefaultValue(false)]
        public bool AutomaticallyMinify { get; set; } = false;

        [LocalizedCategory("Category_Build_Title")]
        [DisplayName("Default output file name")]
        [Description("Sets the default name of the built Tailwind CSS file; use {0} if you want to reference the content file name")]
        [DefaultValue("{0}.output.css")]
        public string TailwindOutputFileName { get; set; } = "{0}.output.css";

        [LocalizedCategory("Category_Build_Title")]
        [DisplayName("Tailwind CLI path")]
        [Description("The absolute path to the Tailwind CLI executable for building: if empty, the default npx tailwindcss build command will run; if not, the specified Tailwind CLI will be called")]
        public string TailwindCliPath { get; set; }

        [LocalizedCategory("Category_Build_Title")]
        [DisplayName("Build type")]
        [Description("Files can be built in four ways: Default (Tailwind JIT), OnSave (on file save), OnBuild (on project build), and None (no building)")]
        [TypeConverter(typeof(EnumConverter))]
        [DefaultValue(BuildProcessOptions.Default)]
        public BuildProcessOptions BuildProcessType { get; set; } = BuildProcessOptions.Default;

        [LocalizedCategory("Category_Class_Sort_Title")]
        [DisplayName("Class sort type")]
        [Description("Classes can be sorted manually (with 'Tools' options), on file save (only sorts open file), on build (entire solution), or never.")]
        [TypeConverter(typeof(EnumConverter))]
        [DefaultValue(SortClassesOptions.OnSave)]
        public SortClassesOptions ClassSortType { get; set; } = SortClassesOptions.OnSave;

        [LocalizedCategory("Category_Custom_Build_Title")]
        [DisplayName("Build script")]
        [Description("The name of the script to execute on build (defined in package.json); leave blank to use the default Tailwind CSS build")]
        public string BuildScript { get; set; }

        [LocalizedCategory("Category_Custom_Build_Title")]
        [DisplayName("Override build")]
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
