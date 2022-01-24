using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace JsonFormatter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (CheckIfOutputEmpty(OutputTextBox.Text))
                {
                    CopyOutputButton.Enabled = false;
                }
                else
                {
                    CopyOutputButton.Enabled = true;
                }

                if (CheckIfInputEmpty(InputTextBox.Text))
                {
                    OutputTextBox.Text = "";
                    CopyOutputButton.Enabled = false;
                } else
                {
                    string prettyJson = PrettyJson(InputTextBox.Text);

                    PrintOutputJson(prettyJson);

                    CopyOutputButton.Enabled = true;
                }
            } catch (Exception ex)
            {
                OutputTextBox.Text = ex.Message;
            }
        }

        private void CopyOutputButton_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(OutputTextBox.Text);
                NotifySuccess("Output copied to the clipboard.");
            } catch (Exception ex)
            {
                NotifyFail(ex.Message);
            }
        }

        private bool CheckIfInputEmpty(string text)
        {
            if (text.Length <= 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private bool CheckIfOutputEmpty(string text)
        {
            if (text.Length <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string PrettyJson(string json)
        {
            return JToken.Parse(json).ToString();
        }

        private void PrintOutputJson(string text)
        {
            if (OutputTextBox.InvokeRequired)
            {
                OutputTextBox.Invoke(new MethodInvoker(() =>
                {
                    OutputTextBox.Text = text;
                }));
            } else
            {
                OutputTextBox.Text = text;
            }
        }

        private void NotifySuccess(string message)
        {
            MessageBox.Show(message, "Hooray!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void NotifyFail(string message)
        {
            MessageBox.Show(message, "Oops...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
