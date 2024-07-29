using DynamicData;
using Newtonsoft.Json;
using ReactiveUI.Fody.Helpers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.ObjectModel;

/// <summary>
/// Contains project metadata and serves as a root for related project files in the directory it is contained in.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public sealed class ProjectFile : JsonFile
{
    /// <inheritdoc />
    public ProjectFile() : base()
    {
    }

    /// <summary>
    /// The name of the <see cref="ProjectFile" />.
    /// </summary>
    [JsonProperty, Reactive]
    public string Name { get; set; } = string.Empty;

    /// <inheritdoc />
    public override Task Load()
    {
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public override Task Unload()
    {
        return Task.CompletedTask;
    }
}