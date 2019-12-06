using System;
namespace KyAApp.Models
{
    public class MenuModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string BackgroundColor { get; set; }
        public bool IsVisibleMenu { get; set; }
    }
}
