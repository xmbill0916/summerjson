using com.xmbill.json.api;
using com.xmbill.sample.DescDemo;
using System; 
using System.Windows.Forms;

namespace com.xmbill.sample
{
    public partial class ExampleMain : Form
    {
        public ExampleMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object s = 213212.11;
            int b = (int)s;
            object obj; 
            obj = JsonPathDescExample.RootIsArrayToObject();
            obj = JsonPathDescExample.RootIsArrayToObjectOfCustomType();
            obj = JsonPathDescExample.RootIsArrayToArrayTypeObject();
            obj = JsonPathDescExample.RootIsArrayToObjectOfCustomType1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            object obj = JsonPathDescOfObjectExample.RootIsObjectToObject();
            obj = JsonPathDescOfObjectExample.RootIsObjectToDataTable();
            obj = JsonPathDescOfDataset.ToDataSetObject();
        }
    }
}
