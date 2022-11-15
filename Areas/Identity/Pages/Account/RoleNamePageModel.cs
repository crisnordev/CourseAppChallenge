﻿using courseappchallenge.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace courseappchallenge.Areas.Identity.Pages.Account;

public class RoleNamePageModel : PageModel
{
    public SelectList RoleNameSelectList { get; set; } = default!;

    public void PopulateRolesDropDownList(RoleManager<AppRole> roleManager, object? selectedRole = null)
    {
        var rolesQuery = from item in roleManager.Roles
            orderby item.Name
            select item;

        RoleNameSelectList = new SelectList(rolesQuery.AsNoTracking(),
            "Id", "Name", selectedRole);
    }
}