#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace MathParser.Tokens.Attributes
{
    /// <summary>
    /// An attribute that defines the value of a "constant" enum value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
    internal class ConstantValueAttribute : Attribute
    {

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantValueAttribute"/> class.  
        /// This is the constructor to use if the constant value should have no value (i.e., null).
        /// </summary>
        public ConstantValueAttribute()
            : base()
        {
            this.Value = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantValueAttribute"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public ConstantValueAttribute(double value)
            : base()
        {
            this.Value = (decimal)value;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets the value.
        /// </summary>
        public decimal? Value { get; private set; }

        #endregion

    }
}
