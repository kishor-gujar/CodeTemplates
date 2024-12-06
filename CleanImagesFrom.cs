 public async Task<IActionResult> CleanImages()
 {
     var list = await _context.SliderImages.ToListAsync();
     var imagePaths = new List<string>();
     foreach (var record in list) {

         if (record.Image != null) {

             var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", record.Image.TrimStart('/'));

             if (System.IO.File.Exists(imagePath))
             {
                 imagePaths.Add(Path.GetFileName(imagePath));
             }
         }
         if (record.MobileImage != null)
         {
             var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", record.Image.TrimStart('/'));
             if (System.IO.File.Exists(imagePath))
             {
                 imagePaths.Add(Path.GetFileName(imagePath));
             }
         }
     }

     var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/slider");
     
     var filesInFolder = Directory.GetFiles(folder).Select(x => Path.GetFileName(x));

     var filesToDelete = filesInFolder.Where(file => !imagePaths.Contains(Path.GetFileName(file))).ToList();

     foreach (var file in filesToDelete)
     {
         var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/slider", file);

         if (System.IO.File.Exists(filePath))
         {
             System.IO.File.Delete(filePath);
         }
     }

     return Ok(new
     {
         DeletedFiles = filesToDelete.Select(Path.GetFileName),
         Message = "Unreferenced files have been deleted successfully."
     });
 }