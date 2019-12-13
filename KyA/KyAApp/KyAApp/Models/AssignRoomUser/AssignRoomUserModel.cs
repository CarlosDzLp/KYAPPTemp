using System;
namespace KyAApp.Models.AssignRoomUser
{
    public class AssignRoomUserModel
    {
        public Guid? IdRoom { get; set; }
        public Guid? UserId { get; set; }
        public Guid? IdOwner { get; set; }
        public Guid? IdAdmin { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? StatusAssign { get; set; }
        public Guid? IdAssign { get; set; }
    }

}
