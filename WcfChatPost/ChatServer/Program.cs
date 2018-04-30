using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel;
using System.Security.Principal;
using System.Windows.Forms;
namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Program obj = new Program();
            if (obj.IsCurrentlyRunningAsAdmin())
                obj.RunServer();
            else
                MessageBox.Show("Try, as Admin, please", "ALERT", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void RunServer()
        {
            using (ServiceHost host = new ServiceHost(typeof(WcfChatPost.ChatService)))
            {
                host.Open();
                Console.WriteLine("ServerOnline");
                Console.ReadLine();
                host.Close();
            }
        }
        private bool IsCurrentlyRunningAsAdmin()
        {
            bool isAdmin = false;
            WindowsIdentity currentIdentity = WindowsIdentity.GetCurrent();
            if (currentIdentity != null)
            {
                WindowsPrincipal pricipal = new WindowsPrincipal(currentIdentity);
                isAdmin = pricipal.IsInRole(WindowsBuiltInRole.Administrator);
                pricipal = null;
            }
            return isAdmin;
        }
    }
}
