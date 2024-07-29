using FluentValidation;
using System.IO;

namespace Horizon.ViewModel.Validators;

/// <summary>
/// Provides a set of helper functions for validating specific values in the view.
/// </summary>
public static class Validator
{
    private static readonly List<string> ReservedFolderNames =
   [
       "CON",
        "PRN",
        "AUX",
        "NUL",
        "COM1",
        "COM2",
        "COM3",
        "COM4",
        "COM5",
        "COM6",
        "COM7",
        "COM8",
        "COM9",
        "COM0",
        "LPT1",
        "LPT2",
        "LPT3",
        "LPT4",
        "LPT5",
        "LPT6",
        "LPT7",
        "LPT8",
        "LPT9",
        "LPT0"
   ];

    private static readonly List<char> InvalidFolderChars =
    [
        '<',
        '>',
        ':',
        '"',
        '/',
        '\\',
        '|',
        '?',
        '*',
        (char)0,
        (char)1,
        (char)2,
        (char)3,
        (char)4,
        (char)5,
        (char)6,
        (char)7,
        (char)8,
        (char)9,
        (char)10,
        (char)11,
        (char)12,
        (char)13,
        (char)14,
        (char)15,
        (char)16,
        (char)17,
        (char)18,
        (char)19,
        (char)20,
        (char)21,
        (char)22,
        (char)23,
        (char)24,
        (char)25,
        (char)26,
        (char)27,
        (char)28,
        (char)29,
        (char)30,
        (char)31
    ];

    private static readonly List<char> InvalidPathChars =
    [
        '<',
        '>',
        '"',
        '|',
        '?',
        '*',
        (char)0,
        (char)1,
        (char)2,
        (char)3,
        (char)4,
        (char)5,
        (char)6,
        (char)7,
        (char)8,
        (char)9,
        (char)10,
        (char)11,
        (char)12,
        (char)13,
        (char)14,
        (char)15,
        (char)16,
        (char)17,
        (char)18,
        (char)19,
        (char)20,
        (char)21,
        (char)22,
        (char)23,
        (char)24,
        (char)25,
        (char)26,
        (char)27,
        (char)28,
        (char)29,
        (char)30,
        (char)31
    ];

    public static IRuleBuilderOptions<T, string?> IsValidFolderName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        => ruleBuilder.Must(IsValidFolderName);

    public static IRuleBuilderOptions<T, string?> IsValidPathName<T>(this IRuleBuilder<T, string?> ruleBuilder)
        => ruleBuilder.Must(IsValidPathName);

    public static IRuleBuilderOptions<T, string?> IsEmptyPath<T>(this IRuleBuilder<T, string?> ruleBuilder)
        => ruleBuilder.Must(IsEmptyPath);

    /// <summary>
    /// Returns whether the supplied path is empty (no files or folders inside). Defaults to true if the directory does not exist.
    /// </summary>
    /// <param name="path">The path to check.</param>
    /// <returns>True if the directory is empty or doesn't exist. Otherwise False.</returns>
    public static bool IsEmptyPath(string? path) => !Directory.Exists(path) || !Directory.EnumerateFileSystemEntries(path).Any();

    /// <summary>
    /// Returns whether the supplied folder name is valid.
    /// </summary>
    /// <param name="name">The name of the folder.</param>
    /// <returns>True if the name is valid, false otherwise.</returns>
    public static bool IsValidFolderName(string? name)

    {
        return !string.IsNullOrWhiteSpace(name)
               && Path.GetInvalidPathChars()
                   .Concat(InvalidFolderChars)
                   .All(c => !name!.Contains(c))
               && !ReservedFolderNames.Contains(name);
    }

    /// <summary>
    /// Returns whether the supplied path is valid.
    /// </summary>
    /// <param name="path">The file path.</param>
    /// <returns>True if the path is valid, false otherwise.</returns>
    public static bool IsValidPathName(string? path)
    {
        return !string.IsNullOrWhiteSpace(path)
               && Path.GetInvalidPathChars()
                   .Concat(InvalidPathChars)
                   .All(c => !path!.Contains(c))
               && !ReservedFolderNames.Contains(Path.GetFileName(path));
    }
}