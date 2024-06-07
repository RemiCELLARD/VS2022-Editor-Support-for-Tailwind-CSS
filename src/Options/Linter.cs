using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using TailwindCSSIntellisense.Attributes;
using TailwindCSSIntellisense.Linting;

namespace TailwindCSSIntellisense.Options;

public class Linter : BaseOptionModel<Linter>
{
    [LocalizedCategory("Option_Category_General_Title")]
    [LocalizedDisplayName("Option_Name_Enable_Linter")]
    [Description("Enables or disables the entire linter.")]
    [DefaultValue(true)]
    public bool Enabled { get; set; } = true;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_Invalid_Screen")]
    [Description("Unknown screen name used with the @screen directive.")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Error)]
    public ErrorSeverity InvalidScreen { get; set; } = ErrorSeverity.Error;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_Invalid_Tailwind_Directive")]
    [Description("Unknown value used with the @tailwind directive.")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Error)]
    public ErrorSeverity InvalidTailwindDirective { get; set; } = ErrorSeverity.Error;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_Invalid_Config_Path")]
    [Description("Unknown or invalid path used with the theme helper.")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Error)]
    public ErrorSeverity InvalidConfigPath { get; set; } = ErrorSeverity.Error;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_CSS_Conflict")]
    [Description("Class names on the same HTML element / CSS class which apply the same CSS property or properties.")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Warning)]
    public ErrorSeverity CssConflict { get; set; } = ErrorSeverity.Warning;
}
