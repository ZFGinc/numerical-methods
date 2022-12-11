using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChislennyeMetody
{
    public partial class IntGrafic : Form
    {
        ChartArea area = new ChartArea("y=kx");
        Series series = new Series();
        Chart chart = new Chart();

        public IntGrafic()
        {
            InitializeComponent();

            area.AxisX.Crossing = 0;
            area.AxisY.Crossing = 0;

            series.ChartType = SeriesChartType.Line;
            series.ChartArea = "y=kx";
        }

        public void Draw(int id)
        {
            chart.Parent = this;
            chart.Dock = DockStyle.Fill;

            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(area);

            chart.Series.Clear();
            chart.Series.Add(series);

            series.Points.Clear();

            if (id != 2)
            {
                for (double x = -10; x <= 10; x += 0.5)
                {
                    double y;
                    switch (id)
                    {
                        case 0:
                            y = IntNewton.GetSolve(x);
                            series.Points.AddXY(x, y);
                            break;
                        case 1:
                            y = IntLagrange.GetSolve(x);
                            series.Points.AddXY(x, y);
                            break;
                    }
                }
            }
            else
            {
                for (double y = 0; y <= 49; y += 1)
                {
                    double x = IntLagrange.GetSolve(y);
                    series.Points.AddXY(x, y);
                }
            }
        }
    }
}
