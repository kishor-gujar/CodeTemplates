   private readonly IWebHostEnvironment _hostEnvironment;
   public ImagesController(IWebHostEnvironment hostEnvironment)
   {
       _hostEnvironment = hostEnvironment;
   }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sliderImage = await _context.SliderImages.FindAsync(id);
        var imageName = sliderImage.Image;
        var mobileImage = sliderImage.MobileImage;

        _context.SliderImages.Remove(sliderImage);
        await _context.SaveChangesAsync();

        // Path to the directory where images are stored
        var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads/slider", imageName); 
        // Remove the file from the server
        if (System.IO.File.Exists(imagePath))
        {
            System.IO.File.Delete(imagePath);
        }

        // Path to the directory where images are stored
        var mobileImagePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads/slider", mobileImage);
        // Remove the file from the server
        if (System.IO.File.Exists(mobileImagePath))
        {
            System.IO.File.Delete(mobileImagePath);
        }

        // Remove the database record
        _context.SliderImages.Remove(sliderImage);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index)).WithSuccess("Slider image has been deleted.", null);
    }


     [HttpPost]
    public async Task<IActionResult> DeleteSelected(List<int> leadIds)
    {

        var leadsToDelete = await _context.SliderImages
        .Where(lead => leadIds.Contains(lead.Id))
        .ToListAsync();

        foreach (var lead in leadsToDelete)
        {
            // File paths for images
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads/slider", lead.Image);
            var mobileImagePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads/slider", lead.MobileImage);

            // Check and delete image file
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            // Check and delete mobile image file
            if (System.IO.File.Exists(mobileImagePath))
            {
                System.IO.File.Delete(mobileImagePath);
            }
        }

        _context.SliderImages.RemoveRange(leadsToDelete);
        await _context.SaveChangesAsync();

        return Ok("Success");
    }



