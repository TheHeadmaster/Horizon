namespace Horizon.ObjectModel;

public class ProjectTemplate
{
    public string Name { get; set; }

    public string Description { get; set; }

    public List<string> Tags { get; set; } = [];
}