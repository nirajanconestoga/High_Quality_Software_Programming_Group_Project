using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private Chart chartExpenses; // Declare the Chart

        public Form1()
        {
            InitializeComponent();
            AddChart();  // Call method to create the Chart
        }

        private void AddChart()
        {
            // Create a new Chart object
            chartExpenses = new Chart();
            chartExpenses.Size = new System.Drawing.Size(400, 300);
            chartExpenses.Location = new System.Drawing.Point(50, 200); // Adjust position

            // Create Chart Area
            ChartArea chartArea = new ChartArea();
            chartExpenses.ChartAreas.Add(chartArea);

            // Create a new Series for the Pie Chart
            Series series = new Series("Expenses");
            series.ChartType = SeriesChartType.Pie;

            // Add some dummy data (you will replace this with real data)
            series.Points.AddXY("Food", 200);
            series.Points.AddXY("Transport", 150);
            series.Points.AddXY("Shopping", 300);
            series.Points.AddXY("Bills", 100);

            // Add series to Chart
            chartExpenses.Series.Add(series);

            // Add Chart to the Form
            this.Controls.Add(chartExpenses);
        }

        // Existing event handlers (unchanged)
        private void label2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void Form1_Load(object sender, EventArgs e) { }
    }
}

