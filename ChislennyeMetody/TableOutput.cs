using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChislennyeMetody
{
    public class TableOutput
    {
        public DataGridView table;

        public void ResetTable()
        {
            table.Rows.Clear();
            table.Columns.Clear();
            table.RowCount = 1;
            table.ColumnCount = 1;
        }
        public void SetTitles(string[] titles)
        {
            table.ColumnCount = titles.Length;
            AddLine(titles);
        }
        public void AddLine()
        {
            table.RowCount++;
        }
        public void AddLine(string[] data)
        {
            table.Rows.Add(data);
        }
    }

}
