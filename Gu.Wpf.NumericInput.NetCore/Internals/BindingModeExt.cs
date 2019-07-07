namespace Gu.Wpf.NumericInput.NetCore
{
    using System.Windows.Data;

    internal static class BindingModeExt
    {
        internal static bool IsEither(this BindingMode self, BindingMode x, BindingMode y) => self == x || self == y;
    }
}
