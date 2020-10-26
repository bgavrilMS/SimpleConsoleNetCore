using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Authentication.Web.Core;
using Windows.UI.ApplicationSettings;

namespace SimpleConsoleNetCore
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IntPtr handle = NativeMethods.GetConsoleWindow();

            // fails with System.UnauthorizedAccessException: 'Access is denied. (0x80070005 (E_ACCESSDENIED))'
            var retaccountPane = AccountsSettingsPaneInterop.GetForWindow(handle);

        }

    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        internal static extern IntPtr GetConsoleWindow();
    }
    internal static class AccountsSettingsPaneInterop
    {
        [STAThread]
        public static AccountsSettingsPane GetForWindow(IntPtr hWnd)
        {
            IAccountsSettingsPaneInterop accountsSettingsPaneInterop = (IAccountsSettingsPaneInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(AccountsSettingsPane));
            Guid guid = typeof(AccountsSettingsPane).GetInterface("IAccountsSettingsPane").GUID;

            var result = accountsSettingsPaneInterop.GetForWindow(hWnd, ref guid);

            return result;
        }
        public static IAsyncAction ShowManagedAccountsForWindowAsync(IntPtr hWnd)
        {
            IAccountsSettingsPaneInterop accountsSettingsPaneInterop = (IAccountsSettingsPaneInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(AccountsSettingsPane));
            Guid guid = typeof(IAsyncAction).GUID;

            return accountsSettingsPaneInterop.ShowManagedAccountsForWindowAsync(hWnd, ref guid);
        }
        public static IAsyncAction ShowAddAccountForWindowAsync(IntPtr hWnd)
        {
            IAccountsSettingsPaneInterop accountsSettingsPaneInterop = (IAccountsSettingsPaneInterop)WindowsRuntimeMarshal.GetActivationFactory(typeof(AccountsSettingsPane));
            Guid guid = typeof(IAsyncAction).GUID;

            return accountsSettingsPaneInterop.ShowAddAccountForWindowAsync(hWnd, ref guid);
        }

       
    }

    //------------------------IAccountsSettingsPaneInterop----------------------------
    //MIDL_INTERFACE("D3EE12AD-3865-4362-9746-B75A682DF0E6")
    //IAccountsSettingsPaneInterop : public IInspectable
    //{
    //public:
    //    virtual HRESULT STDMETHODCALLTYPE GetForWindow(
    //        /* [in] */ __RPC__in HWND appWindow,
    //        /* [in] */ __RPC__in REFIID riid,
    //        /* [iid_is][retval][out] */ __RPC__deref_out_opt void** accountsSettingsPane) = 0;
    //    virtual HRESULT STDMETHODCALLTYPE ShowManageAccountsForWindowAsync(
    //        /* [in] */ __RPC__in HWND appWindow,
    //        /* [in] */ __RPC__in REFIID riid,
    //        /* [iid_is][retval][out] */ __RPC__deref_out_opt void** asyncAction) = 0;
    //    virtual HRESULT STDMETHODCALLTYPE ShowAddAccountForWindowAsync(
    //        /* [in] */ __RPC__in HWND appWindow,
    //        /* [in] */ __RPC__in REFIID riid,
    //        /* [iid_is][retval][out] */ __RPC__deref_out_opt void** asyncAction) = 0;
    //};
    [System.Runtime.InteropServices.Guid("D3EE12AD-3865-4362-9746-B75A682DF0E6")]
    [System.Runtime.InteropServices.InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIInspectable)]
    internal interface IAccountsSettingsPaneInterop
    {
        [STAThread]
        AccountsSettingsPane GetForWindow(IntPtr appWindow, [System.Runtime.InteropServices.In] ref Guid riid);
        IAsyncAction ShowManagedAccountsForWindowAsync(IntPtr appWindow, [System.Runtime.InteropServices.In] ref Guid riid);
        IAsyncAction ShowAddAccountForWindowAsync(IntPtr appWindow, [System.Runtime.InteropServices.In] ref Guid riid);
    }


}
