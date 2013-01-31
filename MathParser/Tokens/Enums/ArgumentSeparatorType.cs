#region - Using Statements -

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EnumHelper;

#endregion

namespace MathParser.Tokens.Enums
{
    /// <summary>
    /// An enumeration of argument separators.
    /// </summary>
    internal enum ArgumentSeparatorType
    {
        /// <summary>
        /// An unknown argument separator.
        /// </summary>
        [EnumDescription("", "")]
        Unknown,
        /// <summary>
        /// A comma.
        /// </summary>
        [EnumDescription("Comma", ",")]
        Comma
    }
}
