using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PriconneSizer
{
	public class MainViewModel : INotifyPropertyChanged
	{
		//Standard INotifyPropertyChanged implementation, pick your favorite
		public MainViewModel()
		{
			InitializeCmbBox();
		}
		public ObservableCollection<ComboBoxItem> cbItems { get; set; }
		public ComboBoxItem SelectedcbItem { get; set; }
		private String _widthValue;
		public String WidthValue
		{
			get { return _widthValue; }
			set
			{
				_widthValue = value;
				OnPropertyChanged("WidthValue");
			}
		}

		private String _heigtValue;
		public String HeightValue
		{
			get { return _heigtValue; }
			set
			{
				_heigtValue = value;
				OnPropertyChanged("HeightValue");
			}
		}

		private void OnPropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void InitializeCmbBox()
		{
			cbItems = new ObservableCollection<ComboBoxItem>();
			var tmpItems = new List<ComboBoxItem>();
			tmpItems.Add(new ComboBoxItem { Content = "1280x720" });
			SelectedcbItem = tmpItems[0];
			tmpItems.Add(new ComboBoxItem { Content = "1600x900" });
			tmpItems.Add(new ComboBoxItem { Content = "1920x1080" });
			tmpItems.Add(new ComboBoxItem { Content = "2560x1440" });
			tmpItems.Add(new ComboBoxItem { Content = "3840x2160" });
			foreach (var item in tmpItems)
			{
				cbItems.Add(item);
			}
		}

	}

}
