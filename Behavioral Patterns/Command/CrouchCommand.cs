using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public class CrouchCommand : ICommand
    {
        
        public string Name { get; } = "Crouch";

        public void Execute(Label label)
        {
            label.Text = "Crouching";
        }
    }
}