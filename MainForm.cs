/*
 * Created by SharpDevelop.
 * User: hadie
 * Date: 29/01/2022
 * Time: 08:50 a. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Proyect1
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		Bitmap bmpImage;
		List<Circle> circleList;
		
		int circleHeight = 0;
		int circleWidth = 0;
		
		int circleCenterX = 0;
		int circleCenterY = 0;
		
		int circleRadius;
		
		int v = 0;
		double menorDistancia = 999999;
		double distancia = 0;
		
		int[] x = new int[100];
		int[] y = new int[100];
		
		int[] xminus = new int[2];
		int[] yminus = new int[2];
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			circleList = new List<Circle>();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void ButtonSelectImageClick(object sender, EventArgs e)
		{
			openFileDialog1.ShowDialog();
			bmpImage = new Bitmap(openFileDialog1.FileName);
			pictureBoxImage.Image = bmpImage;
		}
		
		//BUSCAR CIRCULO
		void ButtonSearchClick(object sender, EventArgs e)
		{
			xminus[0]=0;
			xminus[1]=0;
			yminus[0]=0;
			yminus[1]=0;
			labelCirlce1.Text = " ";
			labelCircle2.Text = " ";
			menorDistancia = 999999;
			v=0;
			circleList.Clear();
			listBox1.Items.Clear();
			Color c_i;
			for(int y_i = 0 ; y_i < bmpImage.Height ; y_i++)
			{
				for(int x_i = 0 ; x_i < bmpImage.Width ; x_i++)
				{
					c_i = bmpImage.GetPixel(x_i, y_i);
					if (isBlack(c_i))
					{
						getCircle(x_i, y_i, bmpImage);
						x[v] = circleCenterX;
						y[v] = circleCenterY;
						v++;
						Circle circle = new Circle(circleCenterX, circleCenterY, v, circleRadius);
						circleList.Add(circle);
						//EVITAR CIRCULOS REPETIDOS
						drawCircule(circleCenterX-(circleWidth/2+2), circleCenterY-(circleHeight/2+2), circleWidth+4, circleHeight+4, bmpImage);
						drawPixel(circleCenterX, circleCenterY, bmpImage);
						drawString(v, circleCenterX, circleCenterY, bmpImage);
						pictureBoxImage.Refresh();
					}
				}
			}
			
			for(int i=0 ; i < circleList.Count ; i++)
			{
				listBox1.Items.Add(circleList[i]);
			}
			
			//ALGORITMO DE FUERZA BRUTA
			for(int i=0 ; i < circleList.Count ; i++)
			{
				for(int j= i+1 ; j < circleList.Count ; j++)
				{
					distancia = Math.Sqrt(((x[j] - x[i])*(x[j] - x[i])) + ((y[j] - y[i])*(y[j] - y[i])));
					
					if(distancia < menorDistancia)
					{
						menorDistancia = distancia;
						xminus[0] = x[i];
						xminus[1] = x[j];
						
						yminus[0] = y[i];
						yminus[1] = y[j];
					}
				}
			}
			
			for(int i=1 ; i < circleList.Count ; i++)
			{
				if(x[i-1] == xminus[0] && y[i-1] == yminus[0])
				{
					
					labelCirlce1.Text = i.ToString();
				}
				
				if(x[i-1] == xminus[1] && y[i-1] == yminus[1])
				{
					labelCircle2.Text = i.ToString();
				}
			}
			
			labelX1.Text = xminus[0].ToString();
			labelY1.Text = yminus[0].ToString();
			labelX2.Text = xminus[1].ToString();
			labelY2.Text = yminus[1].ToString();
		}
		
		bool isBlack(Color color)
		{
			if(color.R == 0)
			{
				if(color.G == 0)
				{
					if(color.B == 0)
					{
						return true;
					}
				}
			}
			
			return false;
		}
		
		void drawCircule(int x, int y, int w, int h, Bitmap bmpLocal)
		{
			Graphics g = Graphics.FromImage(bmpLocal);
			Brush brocha = new SolidBrush(Color.Gold);
			g.FillEllipse(brocha, x, y, w, h);
		}
		
		void drawPixel(int x_i, int y_i, Bitmap bmpLocal)
		{
			Graphics g = Graphics.FromImage(bmpLocal);
			Brush brocha = new SolidBrush(Color.Green);
			for(int i=x_i; i<=x_i+20 ; i++)
			{
				g.FillRectangle(brocha, i, y_i, 1, 1);
			}
			for(int i=x_i; i>=x_i-20 ; i--)
			{
				g.FillRectangle(brocha, i, y_i, 1, 1);
			}
			
			for(int j=y_i;j<=y_i+20; j++)
			{
				g.FillRectangle(brocha, x_i, j, 1, 1);
			}
			
			for(int j=y_i;j>=y_i-20; j--)
			{
				g.FillRectangle(brocha, x_i, j, 1, 1);
			}
			
		}
		
		void drawString(int num, int x, int y, Bitmap bmpLocal)
		{
			Graphics g = Graphics.FromImage(bmpLocal);
			String drawstring = num.ToString();
		             
		    Font drawFont = new Font("Arial", 20);
		    SolidBrush drawBrush = new SolidBrush(Color.Red);
		            
		    StringFormat drawFormat = new StringFormat();
		    drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
		             
		    g.DrawString(drawstring, drawFont, drawBrush, x, y, drawFormat);
		}
		
		//CALCULAR CENTRO
		void getCircle(int c_x, int c_y, Bitmap bmpLocal)
		{
			Color c_i;
			
			int y_i = c_y;
			int x_i = c_x;
			
			do
			{
				c_i = bmpImage.GetPixel(x_i, y_i);
				y_i++;
			}while (isBlack(c_i));
			
			circleCenterY = ((((y_i-1)-c_y)/2) + c_y);
			
			circleHeight = (y_i-1)-c_y;
			
			do
			{
				c_i = bmpImage.GetPixel(x_i, circleCenterY);
				x_i++;
			}while (isBlack(c_i));
			
			
			int x_i2 = c_x;
			
			do
			{
				c_i = bmpImage.GetPixel(x_i2, circleCenterY);
				x_i2--;
			}while (isBlack(c_i));
			
			circleCenterX = ((((x_i-1)-x_i2+1)/2) + x_i2+1);
			
			circleWidth = (x_i-1)-(x_i2+1);
			
			circleRadius = circleWidth/2;
		}
	}
}
