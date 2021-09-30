using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public class ReloadCommand : ICommand
    {
        public string Name { get; } = "Reload";

        public void Execute(Label label)
        {
            label.Text = "Reloading";
        }
    }
}