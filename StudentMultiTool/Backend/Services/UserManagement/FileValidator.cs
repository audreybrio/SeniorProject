namespace StudentMultiTool.Backend.Services.UserManagement
{
    public class FileValidator
    {
        public string TXT { get; } = ".txt";
        public FileValidator()
        {

        }
        public bool IsBulkOperationFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                string path = Path.GetFullPath(filePath);
                return false;
            }
            FileAttributes attribs = File.GetAttributes(filePath);
            if (attribs == FileAttributes.Directory)
            {
                return false;
            }
            FileInfo info = new FileInfo(filePath);
            if (!TXT.Equals(info.Extension) || !info.Exists || info.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
