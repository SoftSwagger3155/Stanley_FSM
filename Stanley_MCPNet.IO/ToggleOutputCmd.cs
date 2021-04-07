using System;
using System.Windows;
using System.Windows.Input;

namespace Stanley_MCPNet.IO
{
    public class ToggleOutputCmd : ICommand
    {
        public ToggleOutputCmd(OpObj io)
        {
            this.digitalOutput = io;
        }

        public bool CanExecute(object parameter)
        {
            return this.digitalOutput != null && this.digitalOutput.enable;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            try
            {
                if (this.digitalOutput != null)
                {
                    this.digitalOutput.Toggle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private OpObj digitalOutput = null;
    }
}
