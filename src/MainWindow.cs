using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System;

namespace PriconneSizer
{

	public partial class MainWindow : MetroWindow
	{
		MainViewModel vm = new MainViewModel();

		private static readonly Regex integerRegex = new Regex("[^0-9]+");
		private static bool IsTextAllowed(string text)
		{
			return integerRegex.IsMatch(text);
		}

		public MainWindow()
		{
			DataContext = vm;
			InitializeComponent();
		}

		// 이벤트 핸들러
		private void txtBox_PreviewLimitIntegerInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = IsTextAllowed(e.Text);
		}

		private void cmbBox_LayoutChanged(object sender, SelectionChangedEventArgs e)
		{
			Debug.WriteLine("BEFORE: " + vm.WidthValue + " " + vm.HeightValue);
			foreach (ComboBoxItem elem in e.AddedItems)
			{
				var parsedResult = parseWidthHeight((string)elem.Content);
				vm.WidthValue = parsedResult["width"];
				vm.HeightValue = parsedResult["height"];
				Debug.WriteLine(vm.WidthValue + " " + vm.HeightValue);
			}
			Debug.WriteLine("AFTER: " + vm.WidthValue + " " + vm.HeightValue);
		}

		private void btn_ClickResizePriconne(object sender, RoutedEventArgs e)
		{
			int conneWidth = int.Parse(vm.WidthValue);
			int conneHeight = int.Parse(vm.HeightValue);
			DialogResult result = System.Windows.Forms.MessageBox.Show($"{vm.WidthValue}x{vm.HeightValue}로 해상도를 변경합니까?", "프리코네 해상도 변경", MessageBoxButtons.YesNo);
			if (result == System.Windows.Forms.DialogResult.Yes)
			{
				string subkey = @"Software\Cygames\PrincessConnectReDive";
				RegistryKey key = Registry.CurrentUser.OpenSubKey(subkey, true);
				key.SetValue("Screenmanager Resolution Width_h182942802", conneWidth);
				key.SetValue("Screenmanager Resolution Height_h2627697771", conneHeight);
				MessageBox.Show("설정이 완료됐습니다.", "프리코네 해상도 변경");
			}

		}

		private Dictionary<string, string> parseWidthHeight(string cmbItem)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			string[] parsedWh = cmbItem.Split('x');
			result.Add("width", parsedWh[0]);
			result.Add("height", parsedWh[1]);
			return result;
		}
	}
}