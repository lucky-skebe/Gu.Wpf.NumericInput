namespace Gu.Wpf.NumericInput.NetCore
{
    using System.Windows.Input;

    public interface ISpinnerBox
    {
        ICommand IncreaseCommand { get; }

        ICommand DecreaseCommand { get; }

        bool AllowSpinners { get; }
    }
}
