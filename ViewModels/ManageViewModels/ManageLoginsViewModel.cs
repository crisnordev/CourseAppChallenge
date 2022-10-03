// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace courseappchallenge.ViewModels.ManageViewModels;

public class ManageLoginsViewModel
{
    public IList<UserLoginInfo> CurrentLogins { get; set; }
}
