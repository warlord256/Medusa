using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Medusa
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gHook;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            PasteOps.ReceiveWorkItem("vignesh***filethis is");
            gHook = new GlobalKeyboardHook(); // Create a new GlobalKeyboardHook
                                              // Declare a KeyDown Event
            gHook.KeyUp += new KeyEventHandler(gHook_KeyUp);
            // Add the keys you want to hook to the HookedKeys list
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
        }
        public void gHook_KeyUp(object sender, KeyEventArgs e)
        {
            // textBox1.Text += ((char)e.KeyValue).ToString();
            // if(e.Control == true && e.KeyCode==Keys.C)
            string hello="";
            string finalContent = "";
            string fileType = "Text";
            string[] files= { };

            if (Control.ModifierKeys==Keys.Control && e.KeyCode == Keys.C)
            {
                

                IDataObject data_object = Clipboard.GetDataObject();
                
                // Look for a file drop.
                if (data_object.GetDataPresent(DataFormats.FileDrop))
                {
                    fileType = "File";
                     files = (string[])data_object.GetData(DataFormats.FileDrop);                    
                    foreach (string file_name in files)
                    {
                        string name = file_name;
                        if (System.IO.Directory.Exists(file_name))
                        {
                            name = "[" + name + "]";
                        }                            
                        lstFiles.Items.Add(name);
                        finalContent += name;
                    }
                }
                else
                {
                    hello = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);
                    textBox3.Text = hello;                    
                }
            }
            WorkItemMessage message = new WorkItemMessage();
            message.userId = textBox1.Text;
            message.fileType = fileType;
            message.content = (files.ToArray());               
            CopyOps.copyWorkItem( message);
        }
        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.V)
            {
                //string hello = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);
                string hello = "Paste is working fine";
                textBox1.Text = hello;

                //lstFiles.Items.Clear();

                //// Get the DataObject.
                //IDataObject data_object = Clipboard.GetDataObject();

                //// Look for a file drop.
                //if (data_object.GetDataPresent(DataFormats.FileDrop))
                //{
                //    string[] files = (string[])
                //        data_object.GetData(DataFormats.FileDrop);
                //    foreach (string file_name in files)
                //    {
                //        string name = file_name;
                //        if (System.IO.Directory.Exists(file_name))
                //            name = "[" + name + "]";
                //        lstFiles.Items.Add(name);
                //    }
                //}
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gHook.unhook();
            this.Close();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var validateUser = await CheckLogin.Operations(textBox1.Text, textBox2.Text); 

        }

        private async void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals(""))
            {
                MessageBox.Show("User Name and Password cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var userFound = await CheckLogin.Operations(textBox1.Text, textBox2.Text);
                if (userFound)
                {
                    MessageBox.Show("User already exists, Please login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await CheckLogin.Registrations(textBox1.Text, textBox2.Text);
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
                textBox3.Text = "Triggered Copy";
                string hello = Clipboard.GetText(System.Windows.Forms.TextDataFormat.Text);
                textBox3.Text = hello;
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            gHook.hook();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void serializedText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
