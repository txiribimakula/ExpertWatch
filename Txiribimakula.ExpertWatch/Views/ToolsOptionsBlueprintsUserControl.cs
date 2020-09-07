using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using Txiribimakula.ExpertWatch.Loading;

namespace Txiribimakula.ExpertWatch.Views
{
    public partial class ToolsOptionsBlueprintsUserControl : UserControl
    {
        public ToolsOptionsBlueprintsUserControl() {
            InitializeComponent();
        }

        internal BlueprintsOptionPage BlueprintsOptionPage;

        public void Initialize() {
            if(BlueprintsOptionPage.Blueprints != null) {
                Blueprint[] blueprints = JsonConvert.DeserializeObject<Blueprint[]>(BlueprintsOptionPage.Blueprints);
                foreach (Blueprint blueprint in blueprints) {
                    foreach (string key in blueprint.Keys) {
                        dataGridView.Rows.Add(key, "");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Expert Debug Template (*.*)|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string text = System.IO.File.ReadAllText(openFileDialog1.FileName);
                BlueprintsOptionPage.Blueprints = text;
            }

            Initialize();
        }
    }
}
