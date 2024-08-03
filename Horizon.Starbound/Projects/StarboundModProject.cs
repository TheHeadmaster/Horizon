using Horizon.ObjectModel;

namespace Horizon.Starbound.Templates;

public class StarboundModProject : ProjectFile
{
    public override List<string> Tags { get; } = ["Starbound", "Mod"];
    public override string TemplateName { get; } = "Starbound Mod Project";
    public override string TemplateDescription { get; } = "A mod project for the game Starbound.";
}