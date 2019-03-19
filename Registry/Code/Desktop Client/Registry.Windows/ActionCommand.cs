using System;
using System.Windows.Input;

namespace Registry.Windows
{
    public class ActionCommand : ICommand
    {
        private readonly Action<Object> action;
        private readonly Predicate<Object> predicate;

        #region Constructors
        /// <summary>
        /// predicate in super is null for convenience, 
        /// when we do not want any condition for the action to excecute
        /// </summary>
        public ActionCommand(Action<Object> action) : this(action, null)
        {
            
        }

        public ActionCommand(Action<Object> action, Predicate<Object> predicate)
        {
            if (action == null)
                throw new ArgumentNullException("action", "action cannot be null");

            this.action = action;
            this.predicate = predicate;
        }

        #endregion

        #region Execute functions

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.
        /// If the command does not require data to be passed in, this object can be set to null
        /// </param>
        /// <returns>
        /// true if this command can be executed; otherwise false
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (predicate == null)
            {
                return true;
            }

            return predicate(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.
        /// If the command does not require data to be passed in, this object can be set to null
        /// </param>
        public void Execute(object parameter)
        {
            action(parameter);
        }

        /// <summary>
        /// convenience method manually created for execute method to be called with null parameter
        /// </summary>
        public void Execute()
        {
            Execute(null);
        }

        #endregion
    }
}
