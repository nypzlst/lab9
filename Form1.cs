using System;
using System.Drawing;
using System.Windows.Forms;

namespace lab9
{


    public partial class Form1 : Form
    {
        private Panel drawingPanel;
        private TextBox rInput;
        private Button drawButton;
        private float R;

        public Form1()
        {

            this.Size = new Size(800, 600);

            drawingPanel = new Panel();
            drawingPanel.Location = new Point(50, 50);
            drawingPanel.Size = new Size(600, 400);
            drawingPanel.BorderStyle = BorderStyle.FixedSingle;
            drawingPanel.Paint += new PaintEventHandler(DrawingPanel_Paint);
            this.Controls.Add(drawingPanel);

        
            rInput = new TextBox();
            rInput.Location = new Point(50, 470);
            rInput.Size = new Size(100, 30);
            rInput.Text = "1"; 
            this.Controls.Add(rInput);

        
            drawButton = new Button();
            drawButton.Text = "Построить график";
            drawButton.Location = new Point(170, 470);
            drawButton.Click += new EventHandler(DrawButton_Click);
            this.Controls.Add(drawButton);
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {

            if (float.TryParse(rInput.Text, out float result))
            {
                R = result;
                drawingPanel.Invalidate(); 
            }
            else
            {
                MessageBox.Show("Введите корректное значение для R.");
            }
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            int centerX = drawingPanel.Width / 2;
            int centerY = drawingPanel.Height / 2;

            float scale = 50;

            g.DrawLine(Pens.Black, centerX, 0, centerX, drawingPanel.Height);
            g.DrawLine(Pens.Black, 0, centerY, drawingPanel.Width, centerY);

         
            float tMin = 0;
            float tMax = 2 * (float) Math.PI;
            float step = 0.01f;

            PointF? prevPoint = null;
            for (float t = tMin; t <= tMax; t += step)
            {
                float x = R * (float) Math.Pow(Math.Cos(t), 3);
                float y = R * (float) Math.Pow(Math.Sin(t), 3);

                PointF point = new PointF(centerX + x * scale, centerY - y * scale);

                if (prevPoint != null)
                {
                    g.DrawLine(Pens.Blue, prevPoint.Value, point);
                }

                prevPoint = point;
            }
        }

        
    }
}