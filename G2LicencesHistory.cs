using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StelthXmlFilling
{
    public partial class G2LicencesHistory : Form
    {
        public const int DEFAULT_HEIGHT = 150;
        public const int ROW_HEIGHT = 15;
        public List<StelthXmlLicenceEntry> currEntries;
        private const String ERROR_TITLE = "Ошибка!";
        private const String ERROR_INCORRECT_NUMBER = "Неверное число лицензий!";

        public G2LicencesHistory()
        {
            InitializeComponent();
        }
        
        public G2LicencesHistory(List<StelthXmlLicenceEntry> entries)
        {
            currEntries = entries;
            InitializeComponent();
            int i=0;
            licencehistoryDatagrid.Rows.Clear();
            foreach (StelthXmlLicenceEntry entry in entries)
            {
                licencehistoryDatagrid.Rows.Add();
                licencehistoryDatagrid.Rows[i].Cells[0].Value = entry.PurchaseDate.ToShortDateString();
                licencehistoryDatagrid.Rows[i].Cells[1].Value = entry.Licenсes;
                if (true == entry.IsTemp)
                {
                    licencehistoryDatagrid.Rows[i].Cells[2].Value = entry.ExpirationDate.ToShortDateString();
                    licencehistoryDatagrid.Rows[i].Cells[2].ReadOnly = false;
                }
                else
                    licencehistoryDatagrid.Rows[i].Cells[2].ReadOnly = true;
                licencehistoryDatagrid.Rows[i].Cells[3].Value = entry.SupportExpirationDate.ToShortDateString();
                licencehistoryDatagrid.Rows[i].Cells[4].Value = entry.GuaranteeExpirationDate.ToShortDateString();
                i++;
            }
            Height = i * ROW_HEIGHT + DEFAULT_HEIGHT;
        }

        private void licencehistoryDatagrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //выясняем номер лицензии
            try
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        break;
                    case 1:
                        if (int.Parse(licencehistoryDatagrid[e.ColumnIndex, e.RowIndex].Value.ToString()) >= 0)
                            currEntries[e.RowIndex].Licenсes = int.Parse(licencehistoryDatagrid[e.ColumnIndex, e.RowIndex].Value.ToString());
                        else
                            throw (new FormatException(ERROR_INCORRECT_NUMBER));
                        break;
                    case 2:
                        currEntries[e.RowIndex].ExpirationDate = DateTime.Parse(licencehistoryDatagrid[e.ColumnIndex, e.RowIndex].Value.ToString());
                        break;
                    case 3:
                        currEntries[e.RowIndex].SupportExpirationDate = DateTime.Parse(licencehistoryDatagrid[e.ColumnIndex, e.RowIndex].Value.ToString());
                        break;
                    case 4:
                        currEntries[e.RowIndex].GuaranteeExpirationDate = DateTime.Parse(licencehistoryDatagrid[e.ColumnIndex, e.RowIndex].Value.ToString());
                        break;
                }

            }
            catch (FormatException exc)
            {
                MessageBox.Show(exc.Message, ERROR_TITLE, MessageBoxButtons.OK);
            }

        }
    }
}
