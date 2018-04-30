using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using WcfChatPost;
namespace ChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            lblStatus.Text = "Disconnected";
        }

        private ChannelFactory<IChatService> remoteFactory;
        private IChatService remoteProxy;
        private ChatUser clientUser;
        private bool isConnected = false;
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                lblStatus.Text = "Connecting...";
                LoginForm loginDialog = new LoginForm();
                loginDialog.ShowDialog(this);
                if (!string.IsNullOrEmpty(loginDialog.UserName))
                {
                    remoteFactory = new ChannelFactory<IChatService>("ChatConfig");
                    remoteProxy = remoteFactory.CreateChannel();
                    clientUser = remoteProxy.ClientConnect(loginDialog.UserName);
                    if (clientUser != null)
                    {
                        usersTimer.Enabled = true;
                        messagesTimer.Enabled = true;
                        loginToolStripMenuItem.Enabled = false;
                        btnSend.Enabled = true;
                        txtMessage.Enabled = true;
                        isConnected = true;
                        lblStatus.Text = "Connected as: " + clientUser.UserName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usersTimer_Tick(object sender, EventArgs e)
        {
            List<ChatUser> listUsers = remoteProxy.GetAllUsers();
            lstUsers.DataSource = listUsers;
        }

        private void messagesTimer_Tick(object sender, EventArgs e)
        {
            List<ChatMessage> messages = remoteProxy.GetNewMessages(clientUser);
            if (messages != null)
                foreach (var message in messages)
                    InsertMessage(message);
        }

        private void InsertMessage(ChatMessage message)
        {
            txtChat.AppendText("\n" + message.Date + " :" + message.User.UserName + " " + message.Message + "\n");
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessage.Text))
            {
                ChatMessage newMessage = new ChatMessage()
                {
                    Date = DateTime.Now,
                    Message = txtMessage.Text,
                    User = clientUser
                };
                remoteProxy.SendNewMessage(newMessage);
                InsertMessage(newMessage);
                txtMessage.Text = String.Empty;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isConnected)
            {
                remoteProxy.SendNewMessage(new ChatMessage()
                {
                    Date = DateTime.Now,
                    Message = "Log off",
                    User = clientUser
                });
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
