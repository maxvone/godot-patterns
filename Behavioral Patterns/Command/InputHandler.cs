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

		private Dictionary<ICommand, uint> _commandsForScancodes = new Dictionary<ICommand, uint>()
		{
			{new JumpCommand(), (uint)KeyList.Space},
			{new CrouchCommand(), (uint)KeyList.Control},
			{new ReloadCommand(), (uint)KeyList.R},
			{new ShootCommand(), (uint)KeyList.F},
		};
		
		public event Action<Dictionary<ICommand, uint>> KeysUpdated;
		
		public override void _Ready()
		{
			_actionLabel = GetNode<Label>("Container/ActionLabel");
			
			_settingsWindow = GetNode<Settings>("Container/Settings");
			_settingsWindow.Initialize(_commandsForScancodes);

			KeysUpdated += _settingsWindow.UpdateButtons;
		}

		public override void _UnhandledKeyInput(InputEventKey @event)
		{
			if (_settingsWindow.IsInBindingState)
			{
				BindKey(@event.Scancode, _settingsWindow.BindingCommand);
			}
			else
			{
				ExecuteOnInput(@event);
			}
		}

		private void ExecuteOnInput(InputEventKey @event)
		{
			foreach (var item in _commandsForScancodes)
			{
				if (@event.Pressed && @event.Scancode == item.Value)
				{
					item.Key.Execute(_actionLabel);
				}
			}
		}

		private void BindKey(uint newKey, ICommand command)
		{
			for (int i = 0; i < _commandsForScancodes.Count; i++)
			{
				var item = _commandsForScancodes.ElementAt(i);
				if (item.Key.GetType() == command.GetType())
				{
					_commandsForScancodes[item.Key] = newKey;
					OnKeysUpdated(_commandsForScancodes);
				}
			}
		}

		protected virtual void OnKeysUpdated(Dictionary<ICommand, uint> obj)
		{
			KeysUpdated?.Invoke(obj);
		}
	}
}
