using System;

namespace MRD.UserRoleSeparationHelper
{
    [Flags]
    public enum UserAction
    {
        None = 1,
        Read = 2,
        Edit = 4,
        Add = 8,
        Delete = 16
    }
}
