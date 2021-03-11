using System;

namespace MRD.UserRoleSeparationHelper
{
    [Flags]
    public enum UserType
    {
        Guest = UserAction.None,
        User = UserAction.Read,
        Manager = User | UserAction.Edit,
        Admin = Manager | UserAction.Add | UserAction.Delete
    }
}
