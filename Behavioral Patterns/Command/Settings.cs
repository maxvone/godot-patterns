using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public class Settings : GridContainer
    {

        [Export()] private PackedScene labelScene;
        [Export()] private PackedScene buttonScene;
        
        private Dictionary<ICommand, LabelButtonPair> _commandsForLabelButtonPairs = new Dictionary<ICommand, LabelButtonPair>();

        public bool IsInBindingState { get; private set; }
        public ICommand BindingCommand { get; private set; }
        
        public void Initialize(Dictionary<ICommand, uint> commandsForKeyLists)
        {

            foreach (var commandForKeyList in commandsForKeyLists)
            {
                if (labelScene.Instance() is Label label)
                {
                    label.Text = commandForKeyList.Key.Name;
                    AddChild(label);
                }
                else
                {
                    return;
                }

                if (buttonScene.Instance() is Button button)
                {
                    button.Text = OS.GetScancodeString(commandForKeyList.Value);
                    button.Connect("pressed", this, nameof(OnButtonPressed));
                    AddChild(button);
                }
                else
                {
                    return;
                }
                
                
                LabelButtonPair labelButtonPair = new LabelButtonPair(label, button);
                _commandsForLabelButtonPairs.Add(commandForKeyList.Key, labelButtonPair);
            }

        }

        public void UpdateButtons(Dictionary<ICommand, uint> commands)
        {
            for (int i = 0; i < _commandsForLabelButtonPairs.Count; i++)
            {
                var labelButtonPair = _commandsForLabelButtonPairs.ElementAt(i);
            
                for (int j = 0; j < commands.Count; j++)
                {
                    var commandItem = commands.ElementAt(j);

                    if (labelButtonPair.Key.GetType() == commandItem.Key.GetType())
                    {
                        _commandsForLabelButtonPairs[labelButtonPair.Key].Label.Text = commandItem.Key.Name;
                        _commandsForLabelButtonPairs[labelButtonPair.Key].Button.Text = OS.GetScancodeString(commandItem.Value);
                    }
                }
                
            }

            IsInBindingState = false;
            BindingCommand = null;
        }

        private void OnButtonPressed()
        {
            for (int i = 0; i < _commandsForLabelButtonPairs.Count; i++)
            {
                var labelButtonPair = _commandsForLabelButtonPairs.ElementAt(i);

                if (labelButtonPair.Value.Button.Pressed)
                {
                    labelButtonPair.Value.Button.Text = "Press a key";
                    
                    IsInBindingState = true;
                    BindingCommand = labelButtonPair.Key;
                    
                    return;
                }
            }
        }
        
    
    }
}
