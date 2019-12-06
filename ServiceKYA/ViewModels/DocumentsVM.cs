using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class DocumentsVM
    {
        public Guid? IdOwner { get; set; }
        public Guid? IdAdmin { get; set; }
        public string Name { get; set; }
        public byte[] FileDocument { get; set; }
        public string StringFile { get; set; }
    }

    public class DocumentsUpdVM
    {
        public Guid? IdOwner { get; set; }
        public Guid? IdAdmin { get; set; }
        public string Name { get; set; }
        public byte[] FileDocument { get; set; }
        public string StringFile { get; set; }
        public Guid? IdDocument { get; set; }
    }
    public class DocumentsGetVM
    {
        public bool? Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public string Extensions { get; set; }
        public string StringFile { get; set; }
        public byte[] File { get; set; }
        public string Name { get; set; }
        public Guid? IdDocument { get; set; }
        public DateTime? DateModified { get; set; }
        public Guid? IdAdmin { get; set; }
        public Guid? IdOwner { get; set; }
    }
}
