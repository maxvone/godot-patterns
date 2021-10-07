using System;
using System.Collections.Generic;
using Godot;

using System.Linq;

namespace Patterns.Behavioral_Patterns.Command
{
	public class InputHandler : Control
	{
		
		private Label _actionLabel;
		private Settings _settingsWindow;

		private Dictionary<ICommand, KeyList> _commands = new Dictionary<ICommand, KeyList>()
		{
			{new JumpCommand(), KeyList.Space},
			{new CrouchCommand(), KeyList.Control},
			{new ReloadCommand(), KeyList.R},
			{new ShootCommand(), KeyList.F},
		};
		
		public event Action<Dictionary<ICommand, KeyList>> KeysUpdated;
		
		public override void _Ready()
		{
			_actionLabel = GetNode<Label>("Container/ActionLabel");
			_settingsWindow = GetNode<Settings>("Container/Settings");
			
			_settingsWindow.Initialize(_commands);

			KeysUpdated += _settingsWindow.UpdateButtons;
		}

		public override void _UnhandledKeyInput(InputEventKey @event)
		{
			base._UnhandledKeyInput(@event);

			foreach (var item in _commands)
			{
				if (@event.Pressed && @event.Scancode == (ulong) item.Value)
				{
					item.Key.Execute(_actionLabel);
				}
			}
		}

		public void BindKey(KeyList newKey, ICommand command)
		{
			for (int i = 0; i < _commands.Count; i++)
			{
				var item = _commands.ElementAt(i);
				if (item.Key.GetType() == command.GetType())
				{
					_commands[item.Key] = newKey;
					OnKeysUpdated(_commands);
				}
			}
		}

		protected virtual void OnKeysUpdated(Dictionary<ICommand, KeyList> obj)
		{
			KeysUpdated?.Invoke(obj);
		}
	}
}
