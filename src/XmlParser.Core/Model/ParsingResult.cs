﻿namespace XmlParser.Core.Model;

/// <summary>
/// A record encapsulating a result of a parsing operation.
/// </summary>
public sealed record ParsingResult<T>(
    ICollection<T> Data,
    string? Error = null
);