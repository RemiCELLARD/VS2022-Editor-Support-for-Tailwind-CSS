using System;
using System.ComponentModel;
using System.Threading;
using TailwindCSSIntellisense.Properties;

namespace TailwindCSSIntellisense.Attributes;

[AttributeUsage(
    AttributeTargets.Assembly |
    AttributeTargets.Module |
    AttributeTargets.Class |
    AttributeTargets.Struct |
    AttributeTargets.Enum |
    AttributeTargets.Constructor |
    AttributeTargets.Method |
    AttributeTargets.Property |
    AttributeTargets.Field |
    AttributeTargets.Event |
    AttributeTargets.Interface |
    AttributeTargets.Parameter |
    AttributeTargets.Delegate |
    AttributeTargets.ReturnValue |
    AttributeTargets.GenericParameter)]
public class LocalizedCategory : CategoryAttribute
{
    /// <summary>
    /// Initializes a new instance of the LocalizedCategory class
    /// </summary>
    /// <param name="key">Resource manager key</param>
    public LocalizedCategory(string key) : base(Resources.ResourceManager.GetString(key, Thread.CurrentThread.CurrentUICulture))
    {
    }
}
