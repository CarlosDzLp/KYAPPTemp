//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("KyAApp.Views.Administrator.Popup.AdditionalPaymentPopup.xaml", "Views/Administrator/Popup/AdditionalPaymentPopup.xaml", typeof(global::KyAApp.Views.Administrator.Popup.AdditionalPaymentPopup))]

namespace KyAApp.Views.Administrator.Popup {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views/Administrator/Popup/AdditionalPaymentPopup.xaml")]
    public partial class AdditionalPaymentPopup : global::Rg.Plugins.Popup.Pages.PopupPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::KyAApp.Controls.EntryBorder txtdescription;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::KyAApp.Controls.EntryBorder txtprecio;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button btnsave;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(AdditionalPaymentPopup));
            txtdescription = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::KyAApp.Controls.EntryBorder>(this, "txtdescription");
            txtprecio = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::KyAApp.Controls.EntryBorder>(this, "txtprecio");
            btnsave = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "btnsave");
        }
    }
}
