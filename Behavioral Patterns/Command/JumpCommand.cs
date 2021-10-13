using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public class JumpCommand : ICommand
    {
        public string Name { get; } = "Jump";

        public void Execute(Label label)
        {
            label.Text = "Jump";

        }
    }
}