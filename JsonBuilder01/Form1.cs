using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace JsonBuilder01
{
    public partial class Form1 : Form
    {

        private JArray main_array = new JArray();
        private ComboBox[] cb_types = new ComboBox[8];
        private TextBox[] tb_keys = new TextBox[8];
        private TextBox[] tb_values = new TextBox[8];


        public Form1()
        {
            InitializeComponent();

            cb_types[0] = cb_type0;
            cb_types[1] = cb_type1;
            cb_types[2] = cb_type2;
            cb_types[3] = cb_type3;
            cb_types[4] = cb_type4;
            cb_types[5] = cb_type5;
            cb_types[6] = cb_type6;
            cb_types[7] = cb_type7;

            tb_keys[0] = tb_key0;
            tb_keys[1] = tb_key1;
            tb_keys[2] = tb_key2;
            tb_keys[3] = tb_key3;
            tb_keys[4] = tb_key4;
            tb_keys[5] = tb_key5;
            tb_keys[6] = tb_key6;
            tb_keys[7] = tb_key7;

            tb_values[0] = tb_value0;
            tb_values[1] = tb_value1;
            tb_values[2] = tb_value2;
            tb_values[3] = tb_value3;
            tb_values[4] = tb_value4;
            tb_values[5] = tb_value5;
            tb_values[6] = tb_value6;
            tb_values[7] = tb_value7;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {

                JObject item = new JObject();
                for (int i = 0; i < 8; ++i)
                {
                    if (tb_keys[i].Text.Length > 0)
                    {
                        switch (cb_types[i].Text)
                        {
                            case "int": item[tb_keys[i].Text] = int.Parse(tb_values[i].Text); break;
                            case "float": item[tb_keys[i].Text] = double.Parse(tb_values[i].Text); break;
                            case "bool": item[tb_keys[i].Text] = Boolean.Parse(tb_values[i].Text); break;
                            default:
                                if (ck_replace_newline.Checked)
                                {
                                    item[tb_keys[i].Text] = tb_values[i].Text.Replace("\r\n","<br>"); 
                                }
                                else { item[tb_keys[i].Text] = tb_values[i].Text; }
                                
                                break;
                        }

                    }
                    tb_values[i].Text = "";
                }
                
                if (item.Count > 0)
                {
                    main_array.Add(item);
                }
                else
                {
                    MessageBox.Show("No item is added to Array"); 
                }

                tb_result.Text = main_array.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_clear_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; ++i)
            {
                cb_types[i].Text = "string";
                tb_values[i].Text = "";
                tb_keys[i].Text = "";
            }
            main_array.Clear();
            tb_result.Text = main_array.ToString();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDia = new SaveFileDialog();

            // set filters - this can be done in properties as well
            saveDia.Filter = "Text files (*.txt)|*.txt|JS files (*.js)|*.js|All files (*.*)|*.*";

            if (saveDia.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(saveDia.FileName, false, System.Text.Encoding.UTF8))
                    sw.WriteLine(main_array.ToString());
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDia = new OpenFileDialog();
            openDia.Title = "Open File";
            openDia.Filter = "All file|*.*";

            if (openDia.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openDia.FileName);
                    main_array = JArray.Parse(sr.ReadToEnd());
                    tb_result.Text = main_array.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

    }
}
