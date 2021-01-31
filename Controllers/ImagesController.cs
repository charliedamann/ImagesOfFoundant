using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ImagesOfFoundant.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ImagesOfFoundant.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ImageSharingContext _context;

        public ImagesController(ImageSharingContext context)
        {
            _context = context;
        }

        [DisableRequestSizeLimit]
        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile()
        {
            try
            {
                var file = Request.Form.Files[0];
                string fileName = file.FileName;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Unable to upload the file.");
            }

            return Ok();
        }    

        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            image.ImageUrl = "images/" + image.ImageUrl;

            // Check for existing tags
            for(int i = 0; i < image.Tags.Count; i++)
            {
                var newTag = _context.Tags.FirstOrDefault(t => t.Name == image.Tags[i].Name);
                if(null != newTag)
                {
                    image.Tags[i] = newTag;
                }
            }

            _context.Images.Add(image); 
            await _context.SaveChangesAsync();

            return image;
        }              

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetImages()
        {
            return await _context.Images.Include(i=>i.Tags).ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Image>> DeleteImage(long id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\", image.ImageUrl);
            FileInfo imageFile = new FileInfo(filePath);
            imageFile.Delete();
            return image;
        }
    }
}
