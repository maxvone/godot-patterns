using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(Label label);
    }
}