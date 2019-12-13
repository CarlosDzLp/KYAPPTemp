using System;
using KyAApp.Controls.Templates;
using KyAApp.Models.Message;
using Xamarin.Forms;

namespace KyAApp.Controls
{
    public class DataTemplateMessageOwner : DataTemplateSelector
    {
        DataTemplate incomingTextDataTemplate;
        DataTemplate outgoingTextDataTemplate;
        DataTemplate incomingImageDataTemplate;
        DataTemplate outgoingImageDataTemplate;
        public DataTemplateMessageOwner()
        {
            this.incomingTextDataTemplate = new DataTemplate(typeof(IncomingViewCell));
            this.outgoingTextDataTemplate = new DataTemplate(typeof(OutgoingViewCell));
            this.incomingImageDataTemplate = new DataTemplate(typeof(IncomingImageViewCell));
            this.outgoingImageDataTemplate = new DataTemplate(typeof(OutgoingImageViewCell));
        }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var messageVm = item as MessageOwnerToAdmin;
            if (messageVm == null)
                return null;
            if (messageVm.TypeSendUser == 1)
            {
                //admin
                if (messageVm.TypeMessage == 1)
                {
                    //mensaje imagen
                    return outgoingImageDataTemplate;
                }
                else
                {
                    //texto
                    return outgoingTextDataTemplate;
                }
            }
            else
            {
                //usuario
                if (messageVm.TypeMessage == 1)
                {
                    //mensaje imagen
                    return incomingImageDataTemplate;
                }
                else
                {
                    //texto
                    return incomingTextDataTemplate;
                }
            }
            return null;
        }
    }
}
