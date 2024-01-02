﻿using Microsoft.VisualStudio.Core.Imaging;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailwindCSSIntellisense.Completions;
using TailwindCSSIntellisense.Configuration;
using TailwindCSSIntellisense.Options;

namespace TailwindCSSIntellisense.Linting;

[Export]
internal sealed class LinterUtilities : IDisposable
{
    private readonly CompletionUtilities _completionUtilities;

    private readonly Dictionary<string, string> _cacheCssAttributes = [];

    private Linter _linterOptions;
    private General _generalOptions;

    [ImportingConstructor]
    public LinterUtilities(CompletionUtilities completionUtilities)
    {
        _completionUtilities = completionUtilities;
        Linter.Saved += LinterSettingsChanged;
        General.Saved += GeneralSettingsChanged;
        _completionUtilities.Configuration.ConfigurationUpdated += ConfigurationUpdated;
    }

    /// <summary>
    /// Generates errors for a list of classes.
    /// </summary>
    /// <param name="classes">The list of classes to check</param>
    /// <returns>A list of Tuples containing the class name and error message</returns>
    public IEnumerable<Tuple<string, string>> CheckForClassDuplicates(IEnumerable<string> classes)
    {
        var cssAttributes = new Dictionary<string, string>();

        foreach (var c in classes)
        {
            var classTrimmed = c.Split(':').Last().Trim();
            
            if (_cacheCssAttributes.ContainsKey(classTrimmed) == false)
            {
                var desc = _completionUtilities.GetDescriptionFromClass(classTrimmed, shouldFormat: false);

                if (string.IsNullOrWhiteSpace(desc))
                {
                    continue;
                }

                _cacheCssAttributes[classTrimmed] = string.Join(",", desc.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(a => a.Split(':')[0].Trim()).OrderBy(x => x));
            }

            cssAttributes[c] = _cacheCssAttributes[classTrimmed];
        }

        foreach (var group in cssAttributes.GroupBy(x => x.Value, x => x.Key))
        {
            var erroneous = classes.Where(group.Contains);
            var count = erroneous.Count();
            if (count > 1)
            {
                int i = 0;
                foreach (var className in erroneous)
                {
                    var others = erroneous.Take(i)
                        .Concat(erroneous.Skip(i + 1).Take(count - i - 1))
                        .Select(c => $"'{c}'");

                    var errorMessage =
                        $"'{className}' applies the same CSS properties as " +
                        $"{string.Join(", ", others.Take(count - 2))}" +
                        $"{(count > 2 ? " and " : "")}" +
                        $"{others.Last()}.";

                    yield return new(className, errorMessage);
                    i++;
                }
            }
        }
    }

    public bool LinterEnabled()
    {
        _linterOptions ??= ThreadHelper.JoinableTaskFactory.Run(Linter.GetLiveInstanceAsync);
        _generalOptions ??= ThreadHelper.JoinableTaskFactory.Run(General.GetLiveInstanceAsync);

        return _linterOptions.Enabled && _generalOptions.UseTailwindCss;
    }

    private void LinterSettingsChanged(Linter linter)
    {
        _linterOptions = linter;
    }
    
    private void GeneralSettingsChanged(General general)
    {
        _generalOptions = general;
    }

    private void ConfigurationUpdated(TailwindConfiguration config)
    {
        _cacheCssAttributes.Clear();
    }

    public ErrorSeverity GetErrorSeverity(ErrorType type)
    {
        _linterOptions ??= ThreadHelper.JoinableTaskFactory.Run(Linter.GetLiveInstanceAsync);

        return type switch
        {
            ErrorType.InvalidScreen => _linterOptions.InvalidScreen,
            ErrorType.InvalidTailwindDirective => _linterOptions.InvalidTailwindDirective,
            ErrorType.InvalidConfigPath => _linterOptions.InvalidConfigPath,
            ErrorType.CssConflict => _linterOptions.CssConflict,
            _ => ErrorSeverity.Warning
        };
    }

    public ITagSpan<IErrorTag> CreateTagSpan(SnapshotSpan span, string error, ErrorType type)
    {
        var severity = GetErrorSeverity(type);

        if (severity == ErrorSeverity.None)
        {
            return null;
        }

        return new TagSpan<IErrorTag>(span, new ErrorTag(GetErrorTagFromSeverity(severity),
            new ContainerElement(ContainerElementStyle.Wrapped,
                new ImageElement(KnownMonikers.StatusWarning.ToImageId()),
                new ClassifiedTextElement(
                    new ClassifiedTextRun(PredefinedClassificationTypeNames.FormalLanguage, error + " "),
                    new ClassifiedTextRun(PredefinedClassificationTypeNames.ExcludedCode, $"({type.ToString().ToLower()[0]}{type.ToString().Substring(1)})")
                )
            )
        ));
    }

    public string GetErrorTagFromSeverity(ErrorSeverity severity)
    {
        return severity switch
        {
            ErrorSeverity.Suggestion => PredefinedErrorTypeNames.HintedSuggestion,
            ErrorSeverity.Warning => PredefinedErrorTypeNames.Warning,
            ErrorSeverity.Error => PredefinedErrorTypeNames.SyntaxError,
            _ => PredefinedErrorTypeNames.OtherError,
        };
    }

    public void Dispose()
    {
        Linter.Saved -= LinterSettingsChanged;
        _completionUtilities.Configuration.ConfigurationUpdated -= ConfigurationUpdated;
    }
}