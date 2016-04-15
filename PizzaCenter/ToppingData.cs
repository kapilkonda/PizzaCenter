using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using Android.Content; 

namespace PizzaCenter
{
	public class ToppingData
	{
		public static List<Pizza> Instructors { get; private set; }


		public ToppingData(Context context)
		{
			var temp = new List<Pizza>();

			AddInstructors(temp, context); 

			//Instructors = temp.OrderBy(i => i.Topping).ToList();
			Instructors = temp.ToList();
		}

		static void AddInstructors(List<Pizza> instructors, Context context)
		{ 
			string content; 
			using (StreamReader sr = new StreamReader (context.Assets.Open ("pizzas.json")))
			{
				content = sr.ReadToEnd ();
			}
			List<string> toppings = new List<string>();
			List<Pizza> products = JsonConvert.DeserializeObject<List<Pizza>>(content);


			foreach (var product in products)
			{
				product.Toppings.Sort();
				if (product.Toppings.Count > 1)
				{
					int c = 0;
					string top = string.Empty;
					foreach (var topping in product.Toppings)
					{
						int count = product.Toppings.Count;
						c++;

						if (c == product.Toppings.Count)
						{
							top = top + "-" + topping;
							toppings.Add(top);
						}
						else
						{
							if (top == string.Empty)
							{
								top = topping;
							}
							else
							{ top = top + "-" + topping;  }

						} 
					}

				}
				else
				{
					foreach (var topping in product.Toppings)
					{
						toppings.Add(topping);
					}
				}

			}

			var temp = (from x in toppings
				group x by x into g
				let count = g.Count()
				orderby count descending 
				select new { topping = g.Key, Count = count }).Take(20);

			foreach (var item in temp) {
				Pizza instructor = new Pizza ();
				instructor.Topping = item.topping;
				instructor.Count = item.Count.ToString();
				instructors.Add (instructor);
			}
		}
	}
}
