// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Performance", "CA1806:Do not ignore method results", 
    Justification = "Xlib rarely actually returns an error code, often just returning 1 or 0, seemingly at random.", 
    Scope = "namespaceanddescendants", Target = "N:Squish.Interop.X11")]
