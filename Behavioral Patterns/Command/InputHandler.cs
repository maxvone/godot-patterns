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

		private Dictionary<ICommand, uint> _commands = new Dictionary<ICommand, uint>()
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
			
			_settingsWindow.Initialize(_commands);

			KeysUpdated += _settingsWindow.UpdateButtons;
		}

		public override void _UnhandledKeyInput(InputEventKey @event)
		{
			base._UnhandledKeyInput(@event);


			if (_settingsWindow.IsBinding)
			{
				BindKey(@event.Scancode, _settingsWindow.BindingCommand);
			}
			else
			{
				foreach (var item in _commands)
				{
					if (@event.Pressed && @event.Scancode == (uint) item.Value)
					{
						item.Key.Execute(_actionLabel);
					}
				}
			}
			
		}

		public void BindKey(uint newKey, ICommand command)
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

		protected virtual void OnKeysUpdated(Dictionary<ICommand, uint> obj)
		{
			KeysUpdated?.Invoke(obj);
		}
	}
}
