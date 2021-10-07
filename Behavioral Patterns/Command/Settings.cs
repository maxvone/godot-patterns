using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Patterns.Behavioral_Patterns.Command
{
    public class Settings : GridContainer
    {

        [Export()] private PackedScene labelScene;
        [Export()] private PackedScene buttonScene;
        
        private Dictionary<ICommand, LabelButtonPair> _labelButtonPairs = new Dictionary<ICommand, LabelButtonPair>();
        public void Initialize(Dictionary<ICommand, KeyList> keyLists)
        {

            foreach (var item in keyLists)
            {
                if (labelScene.Instance() is Label label)
                {
                    label.Text = item.Key.Name;
                    AddChild(label);
                }
                else
                {
                    return;
                }

                if (buttonScene.Instance() is Button button)
                {
                    button.Text = item.Value.ToString();
                    AddChild(button);
                }
                else
                {
                    return;
                }
                
                
                LabelButtonPair labelButtonPair = new LabelButtonPair(label, button);
                _labelButtonPairs.Add(item.Key, labelButtonPair);
            }

        }

        public void UpdateButtons(Dictionary<ICommand, KeyList> commands)
        {
            for (int i = 0; i < _labelButtonPairs.Count; i++)
            {
                var labelButtonPair = _labelButtonPairs.ElementAt(i);
            
                for (int j = 0; j < commands.Count; j++)
                {
                    var commandItem = _labelButtonPairs.ElementAt(i);
            
                    
                    if (labelButtonPair.Key.GetType() == commandItem.Key.GetType())
                    {
                        _labelButtonPairs[labelButtonPair.Key].Label.Text = commandItem.Key.Name;
                    }
                }
                
                
            }
        }
    
    }
}
