//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("KyAApp.Views.Administrator.Popup.AddAssignmentRoomUserPopup.xaml", "Views/Administrator/Popup/AddAssignmentRoomUserPopup.xaml", typeof(global::KyAApp.Views.Administrator.Popup.AddAssignmentRoomUserPopup))]

namespace KyAApp.Views.Administrator.Popup {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views/Administrator/Popup/AddAssignmentRoomUserPopup.xaml")]
    public partial class AddAssignmentRoomUserPopup : global::Rg.Plugins.Popup.Pages.PopupPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::KyAApp.Controls.CustomPicker pickerowner;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::KyAApp.Controls.CustomPicker pickerroom;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::KyAApp.Controls.CustomPicker pickeruser;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Button btnsave;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(AddAssignmentRoomUserPopup));
            pickerowner = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::KyAApp.Controls.CustomPicker>(this, "pickerowner");
            pickerroom = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::KyAApp.Controls.CustomPicker>(this, "pickerroom");
            pickeruser = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::KyAApp.Controls.CustomPicker>(this, "pickeruser");
            btnsave = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Button>(this, "btnsave");
        }
    }
}
