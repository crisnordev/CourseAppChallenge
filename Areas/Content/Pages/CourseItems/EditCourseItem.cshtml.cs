using CourseAppChallenge.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CourseAppChallenge.Models;
using CourseAppChallenge.ViewModels;
using CourseAppChallenge.ViewModels.CourseItemViewModels;
using Microsoft.AspNetCore.Authorization;

namespace CourseAppChallenge.Areas.Content.Pages.CourseItems;

[Authorize(Policy = "RequireAdministratorRole")]
public class EditCourseItemModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public EditCourseItemModel(ApplicationDbContext context)
    {
        _context = context;
    }

    [BindProperty] 
    public EditCourseItemViewModel EditCourseItemViewModel { get; set; } = default!;

    public Guid? CourseId { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid? id)
    {
        if (id == null) return NotFound(new ErrorResultViewModel("CIED X01 - Id can not be null."));
        var courseItem = await _context.CourseItems.AsNoTracking().FirstOrDefaultAsync(x => x.CourseItemId == id);
        if (courseItem == null) return NotFound(new ErrorResultViewModel("CIED X02 - Can not find this module."));
        EditCourseItemViewModel = courseItem;
        CourseId = courseItem.CourseId;
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(Guid? id)
    {
        if (!ModelState.IsValid) return Page();
        var courseItem = await _context.CourseItems.FirstOrDefaultAsync(x => x.CourseItemId == id);
        if (courseItem == null) return NotFound(new ErrorResultViewModel("CIED X03 - Can not find this module."));

        await TryUpdateModelAsync(
            courseItem!,
            "editcourseitemviewmodel", 
            c => c.CourseItemTitle, c => c.Order);
        
        try
        {
            _context.CourseItems.Update(courseItem);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./DetailsCourseItem", new {id = courseItem.CourseItemId});
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new ErrorResultViewModel("CIED X04 - Internal server error.", ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResultViewModel("CIED X05 - Something is wrong.", ex.Message));
        }
    }

    private bool CourseItemExists(Guid id)
    {
        return (_context.CourseItems?.Any(e => e.CourseItemId == id)).GetValueOrDefault();
    }
}