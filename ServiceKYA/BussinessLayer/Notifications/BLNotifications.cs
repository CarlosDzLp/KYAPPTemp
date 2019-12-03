using BussinessEntities.DataBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Response;

namespace BussinessLayer.Notifications
{
    public static class BLNotifications
    {
        public static async Task InsertNotification(Guid? Id,string playerId,string pushToken,int typeUser)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                using (var dc = new RentAppEntities())
                {
                    var query = dc.spInsNotifications(Id, playerId, pushToken, typeUser);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
