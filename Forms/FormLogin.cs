using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medusa
{
    public partial class FormLogin : Form
    {
        private readonly GlobalKeyboardHook gHook;
        private readonly CopyOps copyOps;
        private readonly PasteOps pasteOps;
        public FormLogin()
        {
            InitializeComponent();
            this.copyOps = new CopyOps();
            gHook = new GlobalKeyboardHook();
            this.SetUpKeyboardHook(gHook);
            this.pasteOps = new PasteOps();
        }
        
        private void FormLogin_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Delegate function to handle the global keyboard keyUp
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        public async void gHook_KeyUp(object sender, KeyEventArgs e)
        {
            string[] files;

            if (string.IsNullOrEmpty(this.textUserId.Text))
            {
                MessageBox.Show("Empty userId");
                return;
            }
            // more username validation if needed.

            WorkItemMessage message = new WorkItemMessage(this.textUserId.Text, WorkItemMessage.FileType.Text, null);
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.C)
            {
                IDataObject data_object = Clipboard.GetDataObject();

                // File drop
                if (data_object.GetDataPresent(DataFormats.FileDrop))
                {
                    message.fileType = WorkItemMessage.FileType.File;
                    files = (string[])data_object.GetData(DataFormats.FileDrop);
                    foreach (string fileName in files)
                    {
                        string name = fileName;
                        if (System.IO.Directory.Exists(fileName))
                        {
                            name = "[" + name + "]";
                        }

                        this.lstFiles.Items.Add(name);
                    }

                    message.content = files;
                }
                else
                // Text drop
                { 
                    string currentClipboardText = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);
                    //message.content.Add(currentClipboardText);
                }
            }
            
            await this.copyOps.copyWorkItem(message).ConfigureAwait(false);
        }
        public async void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
               // await this.pasteOps.PasteWorkItem(message).ConfigureAwait(false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(gHook!=null)
                gHook.unhook();
            this.Close();
        }

        private async void cmdLogin_Click(object sender, EventArgs e)
        {
            var validateUser = await CheckLogin.Operations(textUserId.Text, textBox2.Text); 

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (textUserId.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("User Name and Password cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //var userFound = await CheckLogin.Operations(textBox1.Text, textBox2.Text);
                var userFound = false;
                if (userFound)
                {
                    MessageBox.Show("User already exists, Please login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await CheckLogin.Registrations(textUserId.Text, textBox2.Text);
                    MessageBox.Show("Registration Successful", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }


        private void textBox3_TextChanged(object sender, KeyEventArgs e)
        {

        }
        private void textbox(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                //...
                e.SuppressKeyPress = true;
               // textBox.Text = "Triggered Copy";
                string hello = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);
               // textBox3.Text = hello;
            }
        }

        

        private void hook_click(object sender, EventArgs e)
        {
            gHook.hook();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void serializedText_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void SetUpKeyboardHook(GlobalKeyboardHook gHook)
        {
            gHook.KeyUp += new KeyEventHandler(this.gHook_KeyUp);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                gHook.HookedKeys.Add(key);
            }

            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
        }

        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
