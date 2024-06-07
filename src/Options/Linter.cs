using Community.VisualStudio.Toolkit;
using System.ComponentModel;
using TailwindCSSIntellisense.Attributes;
using TailwindCSSIntellisense.Linting;

namespace TailwindCSSIntellisense.Options;

public class Linter : BaseOptionModel<Linter>
{
    [LocalizedCategory("Option_Category_General_Title")]
    [LocalizedDisplayName("Option_Name_Enable_Linter")]
    [LocalizedDescription("Option_Description_Enable_Linter")]
    [DefaultValue(true)]
    public bool Enabled { get; set; } = true;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_Invalid_Screen")]
    [LocalizedDescription("Option_Description_Invalid_Screen")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Error)]
    public ErrorSeverity InvalidScreen { get; set; } = ErrorSeverity.Error;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_Invalid_Tailwind_Directive")]
    [LocalizedDescription("Option_Description_Invalid_Tailwind_Directive")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Error)]
    public ErrorSeverity InvalidTailwindDirective { get; set; } = ErrorSeverity.Error;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_Invalid_Config_Path")]
    [LocalizedDescription("Option_Description_Invalid_Config_Path")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Error)]
    public ErrorSeverity InvalidConfigPath { get; set; } = ErrorSeverity.Error;

    [LocalizedCategory("Option_Category_Validation_Title")]
    [LocalizedDisplayName("Option_Name_CSS_Conflict")]
    [LocalizedDescription("Option_Description_CSS_Conflict")]
    [TypeConverter(typeof(EnumConverter))]
    [DefaultValue(ErrorSeverity.Warning)]
    public ErrorSeverity CssConflict { get; set; } = ErrorSeverity.Warning;
}
