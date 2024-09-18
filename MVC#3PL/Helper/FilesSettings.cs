namespace MVC_3PL.Helper
{
    public class FilesSettings
    {
        //1.Upload
        public static string UploadFile(IFormFile file,string folderName)
        {
            //1.Get file Path
            //string folderPath = $"F:\\Route C# Course\\00C# Assignments Route\\MVC#3PL\\MVC#3PL\\wwwroot\\files\\{folderName}";
            //string folderPath = Directory.GetCurrentDirectory()+@"wwwroot\files"+folderName;
            string folderPath =Path.Combine(Directory.GetCurrentDirectory() , @"wwwroot\files", folderName);

            //2.Get unique FileName
            string fileName=$"{Guid.NewGuid()}{file.FileName}";

            //3.Get File Path --> FolderPath + FileName
            string filePath= Path.Combine(folderPath, fileName);

            //4.Save File as Stream: Data per Time
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;

        }

        //2.Delete

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath =Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\files",folderName,fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }


    }
}
