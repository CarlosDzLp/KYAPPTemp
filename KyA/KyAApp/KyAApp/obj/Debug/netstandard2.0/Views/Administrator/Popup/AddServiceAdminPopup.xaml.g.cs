//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("KyAApp.Views.Administrator.Popup.AddServiceAdminPopup.xaml", "Views/Administrator/Popup/AddServiceAdminPopup.xaml", typeof(global::KyAApp.Views.Administrator.Popup.AddServiceAdminPopup))]

namespace KyAApp.Views.Administrator.Popup {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views/Administrator/Popup/AddServiceAdminPopup.xaml")]
    public partial class AddServiceAdminPopup : global::Rg.Plugins.Popup.Pages.PopupPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::ImageCircle.Forms.Plugin.Abstractions.CircleImage imgservice;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::KyAApp.Controls.CustomPicker combopicker;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry txtname;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Entry txtprice;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button btnsaveservice;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(AddServiceAdminPopup));
            imgservice = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::ImageCircle.Forms.Plugin.Abstractions.CircleImage>(this, "imgservice");
            combopicker = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::KyAApp.Controls.CustomPicker>(this, "combopicker");
            txtname = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "txtname");
            txtprice = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Entry>(this, "txtprice");
            btnsaveservice = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "btnsaveservice");
        }
    }
}
