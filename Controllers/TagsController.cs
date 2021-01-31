using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ImagesOfFoundant.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ImagesOfFoundant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly ImageSharingContext _context;

        public TagsController(ImageSharingContext context)
        {
            _context = context;
        }                     

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTags()
        {
            return await _context.Tags.Include(i=>i.Images).ToListAsync();
        }        
    }
}
