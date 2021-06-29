namespace Medusa
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Work item message to store data and data type.
    /// </summary>
    public class WorkItemMessage
    {
        public WorkItemMessage(string userId, FileType fileType, string[] content)
        {
            this.userId = userId ?? throw new ArgumentNullException(nameof(userId));
            this.fileType = fileType;
        }
        public WorkItemMessage()
        {
        }

        public enum FileType
        {
            none = 0,
            Text = 1,
            File = 2
        }

        public string userId { get; set; }
        public FileType fileType { get; set; }
        public string[] content { get; set; }
    }
}
