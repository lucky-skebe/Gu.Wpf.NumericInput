namespace Gu.Wpf.NumericInput.NetCore
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;

    /// <summary>Metadata about a validation error message.</summary>
    /// <typeparam name="T">The specific type.</typeparam>
    public abstract class FormatAndCulture<T> : IFormatAndCulture
        where T : IFormatAndCulture
    {
        private readonly Lazy<string> format;

        protected FormatAndCulture(IFormatProvider formatProvider, string resourceKey)
        {
            this.FormatProvider = formatProvider;
            this.ResourceKey = resourceKey;

            // used lazy here to avoid virtual call in ctor.
            // maybe it would work any way but playing it safe.
            this.format = new Lazy<string>(() => this.GetFormat(formatProvider));
        }

        /// <inheritdoc/>
        public string ResourceKey { get; }

        /// <inheritdoc/>
        public IFormatProvider FormatProvider { get; }

        /// <inheritdoc/>
        public string Format => this.format.Value;

        protected ConcurrentDictionary<CultureInfo, T> Cache { get; } = new ConcurrentDictionary<CultureInfo, T>();

        public abstract T GetOrCreate(IFormatProvider formatProvider);

        public string GetFormat(IFormatProvider culture)
        {
            var formatString = Properties.Resources.ResourceManager.GetString(this.ResourceKey, culture as CultureInfo);
            if (!string.IsNullOrEmpty(formatString))
            {
                this.AssertFormat(culture, formatString);
                return formatString;
            }

            return Properties.Resources.ResourceManager.GetString(this.ResourceKey, CultureInfo.InvariantCulture);
        }

        /// <summary>Assert that <paramref name="format"/> is valid.</summary>
        /// <param name="culture">The culture.</param>
        /// <param name="format">The format string.</param>
        /// <exception cref="InvalidOperationException">If the format does not match the expected.</exception>
        protected abstract void AssertFormat(IFormatProvider culture, string format);
    }
}
