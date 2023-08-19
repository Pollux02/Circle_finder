/*
 * Created by SharpDevelop.
 * User: hadie
 * Date: 28/01/2022
 * Time: 07:56 p. m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Proyect1
{
	/// <summary>
	/// Description of Circle.
	/// </summary>
	public class Circle
	{
		int x_c;
		int y_c;
		int i;
		int r;
		
		public Circle()
		{

		}
		
		public Circle(int x_c, int y_c, int i, int r)
		{
			this.x_c = x_c;
			this.y_c = y_c;
			this.i = i;
			this.r = r;
		}
		
		public override string ToString()
		{
			return string.Format("[Centro Círculo num: {0} | X= {1}, Y= {2}] | Radio: {3}", i,  x_c, y_c, r);
		}

	}
}
