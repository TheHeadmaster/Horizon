using Horizon.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Horizon.ViewModel;

public class KeyBindingVisualsViewModel : ReactiveObject
{
    public KeyBindingVisualsViewModel()
    {
        this.WhenAnyValue(x => x.KeyBindingText)
            .Select(x =>
            {
                List<Key> keys = [];
                foreach (string s in x.Split('+'))
                {
                    Key key = KeyConstants.KeyNames.FirstOrDefault(
                        kvp => string.Equals(kvp.Value, s, StringComparison.InvariantCultureIgnoreCase)
                        ).Key;

                    keys.Add(key);
                }

                return keys;
            })
            .Subscribe(x => this.KeyBindings = x);

        this.WhenAnyValue(x => x.KeyBindings)
        .Select(keys =>
        {
            List<string> keyStrings = [];
            foreach (Key key in keys)
            {
                if (KeyConstants.KeyNames.TryGetValue(key, out string? keyString))
                {
                    keyStrings.Add(keyString);
                }
                else
                {
                    keyStrings.Add(Enum.GetName(key) ?? string.Empty);
                }
            }

            return keyStrings.Aggregate((previous, next) => $"{previous}+{next}");
        })
        .Subscribe(x => this.KeyBindingText = x);
    }

    [Reactive]
    public string KeyBindingText { get; set; } = string.Empty;

    [Reactive]
    public List<Key> KeyBindings { get; set; } = [];
}