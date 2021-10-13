using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public class LabelButtonPair
    {
        public Label Label { get; }
        public Button Button { get; }
        
        public LabelButtonPair(Label label, Button button)
        {
            Label = label;
            Button = button;
        }
    }
}