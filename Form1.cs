﻿using System;
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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
