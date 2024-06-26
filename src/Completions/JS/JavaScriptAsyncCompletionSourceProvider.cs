﻿using Microsoft.VisualStudio.Language.Intellisense.AsyncCompletion;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TailwindCSSIntellisense.Settings;

namespace TailwindCSSIntellisense.Completions.JS;

[Export(typeof(IAsyncCompletionSourceProvider))]
[Order(Before = "High")]
[Name("JavaScriptAsyncCompletionSourceProvider")]
[ContentType("JavaScript")]
[ContentType("TypeScript")]
[ContentType("jsx")]
internal class JavaScriptAsyncCompletionSourceProvider : IAsyncCompletionSourceProvider
{
    private readonly IDictionary<ITextView, IAsyncCompletionSource> _cache = new Dictionary<ITextView, IAsyncCompletionSource>();

    [Import]
    public CompletionUtilities CompletionUtilities { get; set; }

    [Import]
    public SettingsProvider SettingsProvider { get; set; }

    public IAsyncCompletionSource GetOrCreate(ITextView textView)
    {
        if (_cache.TryGetValue(textView, out var itemSource))
            return itemSource;

        var source = new JavaScriptAsyncCompletionSource(CompletionUtilities, SettingsProvider);
        textView.Closed += (o, e) => _cache.Remove(textView);
        _cache.Add(textView, source);
        return source;
    }
}
