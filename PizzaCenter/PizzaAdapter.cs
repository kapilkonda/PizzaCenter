﻿using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using PizzaCenter;

namespace PizzaCenter
{
	public class PizzaAdapter : BaseAdapter<Pizza>, ISectionIndexer
	{
		List<Pizza>     pizzas;
		Java.Lang.Object[]   sectionHeaders;
		Dictionary<int, int> positionForSectionMap;
		Dictionary<int, int> sectionForPositionMap;

		public PizzaAdapter(List<Pizza> instructors)
		{
			this.pizzas = instructors;

			sectionHeaders        = SectionIndexerBuilder.BuildSectionHeaders       (instructors);
			positionForSectionMap = SectionIndexerBuilder.BuildPositionForSectionMap(instructors);
			sectionForPositionMap = SectionIndexerBuilder.BuildSectionForPositionMap(instructors);
		}

		public override Pizza this[int position]
		{
			get
			{
				return pizzas[position];
			}
		}

		public override int Count
		{
			get
			{
				return pizzas.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;

			if (view == null)
			{
				view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.InstructorRow, parent, false);

				var p = view.FindViewById<ImageView>(Resource.Id.photoImageView);
				var n = view.FindViewById<TextView >(Resource.Id.nameTextView);
				var s = view.FindViewById<TextView >(Resource.Id.specialtyTextView);

				view.Tag = new ViewHolder() { Photo = p, Name = n, Specialty = s };
			}

			var holder = (ViewHolder)view.Tag;

			holder.Name     .Text = pizzas[position].Topping;
			holder.Specialty.Text = pizzas[position].Count;

			return view;
		}

		public Java.Lang.Object[] GetSections()
		{
			return sectionHeaders;
		}

		public int GetPositionForSection(int section)
		{
			return positionForSectionMap[section];
		}

		public int GetSectionForPosition(int position)
		{
			return sectionForPositionMap[position];
		}
	}
}