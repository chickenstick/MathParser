#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace MathParser.Tokens.Attributes
{
    /// <summary>
    /// Attribute that defines the number of arguments that an operator or a function will use.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple=false)]
    internal class ArgumentCountAttribute : Attribute
    {

        #region - Constructor -

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentCountAttribute"/> class.  
        /// This is the constructor for an attribute that does not define an argument 
        /// count (i.e., null).
        /// </summary>
        public ArgumentCountAttribute()
            : base()
        {
            this.ArgumentCount = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentCountAttribute"/> class.
        /// </summary>
        /// <param name="argumentCount">The argument count.</param>
        public ArgumentCountAttribute(byte argumentCount)
            : base()
        {
            this.ArgumentCount = argumentCount;
        }

        #endregion

        #region - Properties -

        /// <summary>
        /// Gets the argument count.
        /// </summary>
        public byte? ArgumentCount { get; private set; }

        #endregion

    }
}
