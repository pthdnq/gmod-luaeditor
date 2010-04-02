using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GMod_Lua_Editor
{
    public partial class LuaEditor : UserControl
    {

        public LuaEditor()
        {
            InitializeComponent();

            numberLabel.Font = new Font(richTextBox.Font.FontFamily, richTextBox.Font.Size + 1.019f);
        }


        private void updateNumberLabel()
        {
            Point pos = new Point(0, 0);
            int firstIndex = richTextBox.GetCharIndexFromPosition(pos);
            int firstLine = richTextBox.GetLineFromCharIndex(firstIndex);

            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = richTextBox.GetCharIndexFromPosition(pos);
            int lastLine = richTextBox.GetLineFromCharIndex(lastIndex);

            pos = richTextBox.GetPositionFromCharIndex(lastIndex);

           
            numberLabel.Text = "";
            for (int i = firstLine; i <= lastLine + 1; i++)
            {
                numberLabel.Text += i + 1 + "\n";
            }

        }


        private void richTextBox_TextChanged(object sender, EventArgs e)
        {
            updateNumberLabel();

            int selPos = richTextBox.SelectionStart;
            Regex LuaWords = new Regex("function|end|if|then|else");
            //Regex LuaBrackets = new Regex("(|)|<|>|[|]|{|}");
            //Regex LuaOperators = new Regex("+|-|*|/|=|==|!=");

            foreach (Match keyWordMatch in LuaWords.Matches(richTextBox.Text))
            {

                richTextBox.Select(keyWordMatch.Index, keyWordMatch.Length);
                richTextBox.SelectionColor = Color.Blue;
                richTextBox.SelectionStart = selPos;
                richTextBox.SelectionColor = Color.Black;
            }
        }

        private void richTextBox_VScroll(object sender, EventArgs e)
        {
            //move location of numberLabel for amount of pixels caused by scrollbar
            int d = richTextBox.GetPositionFromCharIndex(0).Y % (richTextBox.Font.Height + 1);
            numberLabel.Location = new Point(0, d);

            updateNumberLabel();
        }

        private void richTextBox_Resize(object sender, EventArgs e)
        {
            richTextBox_VScroll(null, null);
        }

        private void richTextBox_FontChanged(object sender, EventArgs e)
        {
            updateNumberLabel();
            richTextBox_VScroll(null, null);
        }

             


    }
}
