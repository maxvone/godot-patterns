using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public class LabelButtonPair
    {
        public Label Label { get; private set; }
        public Button Button { get; private set; }
        
        public LabelButtonPair(Label label, Button button)
        {
            Label = label;
            Button = button;
        }
    }
}